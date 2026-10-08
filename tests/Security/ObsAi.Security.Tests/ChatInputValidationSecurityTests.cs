using ObsAi.Application.Contracts.Chat;
using ObsAi.Application.Contracts.Common;
using ObsAi.Application.Validation;
using Xunit;

namespace ObsAi.Security.Tests;

public sealed class ChatInputValidationSecurityTests
{
    private static InputValidationLimits DefaultLimits { get; } = InputValidationLimits.Create(128, 64);

    private static ChatMessage CreateMessage(string text, string? channel = "channel-1", string? message = "message-1", string? sender = "sender-1") =>
        new(
            ProviderId.Create("youtube-live"),
            channel!,
            message!,
            sender!,
            text,
            new DateTimeOffset(2026, 10, 8, 12, 0, 0, TimeSpan.Zero));

    [Theory]
    [InlineData("malformed\u007fpayload")]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("a\u0000b")]
    public void RejectedInput_NeverYieldsMessageForAnySink(string text)
    {
        var result = ChatInputValidator.Validate(CreateMessage(text), DefaultLimits);

        Assert.True(result.IsRejected);
        Assert.Null(result.Message);
        Assert.NotNull(result.Rejection);
    }

    [Fact]
    public void RejectedInput_UnpairedSurrogate_NeverYieldsMessage()
    {
        var result = ChatInputValidator.Validate(CreateMessage("unpaired\ud800high"), DefaultLimits);

        Assert.True(result.IsRejected);
        Assert.Equal(InputRejectionReason.MalformedEncoding, result.Rejection!.Reason);
        Assert.Null(result.Message);
    }

    [Fact]
    public void MissingTimestamp_IsRejectedWithoutReachingAnySink()
    {
        var result = ChatInputValidator.Validate(CreateMessage("hello") with { ReceivedAtUtc = default }, DefaultLimits);

        Assert.True(result.IsRejected);
        Assert.Equal(InputRejectionReason.MalformedTimestamp, result.Rejection!.Reason);
        Assert.Null(result.Message);
    }

    [Fact]
    public void OversizeFlood_IsAlwaysRejectedWithNullMessage()
    {
        var limits = InputValidationLimits.Create(maximumTextLength: 10, maximumReferenceLength: 16);
        var flood = Enumerable.Repeat(new string('x', 100), 5000);

        foreach (var payload in flood)
        {
            var result = ChatInputValidator.Validate(CreateMessage(payload), limits);
            Assert.True(result.IsRejected);
            Assert.Equal(InputRejectionReason.OversizeText, result.Rejection!.Reason);
            Assert.Null(result.Message);
        }
    }

    [Fact]
    public void RejectionDiagnostic_NeverLeaksPayloadOrOperationalIdentity()
    {
        const string secretMarker = "SECRET-IDENTITY-42";
        var result = ChatInputValidator.Validate(
            CreateMessage($"please DO NOT leak {secretMarker}\u0007", sender: "ok", channel: "ok", message: "ok"),
            DefaultLimits);

        Assert.True(result.IsRejected);
        var diagnostic = result.Rejection!.ToString() + result.Rejection.Reason;
        Assert.DoesNotContain(secretMarker, diagnostic, StringComparison.Ordinal);
    }

    [Fact]
    public void EquivalentInputs_ProduceSingleCanonicalVariant_NoStorageMultiplication()
    {
        var decomposed = "cafe\u0301 e\u0301 claro";
        var composed = "café é claro";

        var decomposedResult = ChatInputValidator.Validate(CreateMessage(decomposed), DefaultLimits);
        var composedResult = ChatInputValidator.Validate(CreateMessage(composed), DefaultLimits);

        Assert.True(decomposedResult.IsAccepted);
        Assert.True(composedResult.IsAccepted);
        Assert.Equal(decomposedResult.Message!.Text, composedResult.Message!.Text);
        Assert.Equal("café é claro", decomposedResult.Message.Text);
    }

    [Fact]
    public void AcceptedMessage_PreservesOnlyRequiredAbuseControlIdentity()
    {
        var result = ChatInputValidator.Validate(
            CreateMessage("hello", channel: "live-channel-9", message: "chat-msg-1", sender: "viewer-77"),
            DefaultLimits);

        Assert.True(result.IsAccepted);
        Assert.Equal("youtube-live", result.Message!.ProviderId.ToString());
        Assert.Equal("live-channel-9", result.Message.ChannelReference);
        Assert.Equal("chat-msg-1", result.Message.MessageReference);
        Assert.Equal("viewer-77", result.Message.SenderReference);
    }

    [Fact]
    public void UnknownRejectionReason_IsRejectedAtCreation()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => InputRejection.Create((InputRejectionReason)int.MaxValue));
    }

    [Fact]
    public void AcceptedText_AlwaysRespectsConfiguredLimits()
    {
        var limits = InputValidationLimits.Create(maximumTextLength: 32, maximumReferenceLength: 8);
        for (var length = 1; length <= 32; length++)
        {
            var result = ChatInputValidator.Validate(CreateMessage(new string('m', length), channel: "c1", message: "m1", sender: "s1"), limits);
            Assert.True(result.IsAccepted, $"Expected acceptance for length {length}.");
            Assert.True(result.Message!.Text.Length <= limits.MaximumTextLength);
            Assert.DoesNotContain(result.Message.Text, char.IsControl);
        }
    }
}

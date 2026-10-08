using ObsAi.Application.Contracts.Chat;
using ObsAi.Application.Contracts.Common;
using ObsAi.Application.Validation;
using Xunit;

namespace ObsAi.Unit.Tests;

public sealed class ChatInputValidationTests
{
    private static InputValidationLimits DefaultLimits { get; } = InputValidationLimits.Create(128, 64);

    private static ChatMessage CreateMessage(
        string text,
        string? channel = "channel-1",
        string? message = "message-1",
        string? sender = "sender-1") =>
        new(
            ProviderId.Create("youtube-live"),
            channel!,
            message!,
            sender!,
            text,
            new DateTimeOffset(2026, 10, 8, 12, 0, 0, TimeSpan.Zero));

    [Fact]
    public void Validate_Accepts_AndTrimsValidChatMessage()
    {
        var result = ChatInputValidator.Validate(CreateMessage("  Olá mundo  "), DefaultLimits);

        Assert.True(result.IsAccepted);
        Assert.Null(result.Rejection);
        Assert.Equal("Olá mundo", result.Message!.Text);
    }

    [Fact]
    public void Validate_CollapsesInternalWhitespaceRuns()
    {
        var result = ChatInputValidator.Validate(CreateMessage("a   b  c"), DefaultLimits);

        Assert.True(result.IsAccepted);
        Assert.Equal("a b c", result.Message!.Text);
    }

    [Theory]
    [InlineData("Olá   mundo")]
    [InlineData("Olá  mundo")]
    [InlineData("Olá mundo")]
    public void Validate_EquivalentInputs_ProduceEqualNormalizedText(string text)
    {
        var accepted = ChatInputValidator.Validate(CreateMessage(text), DefaultLimits);

        Assert.True(accepted.IsAccepted);
        Assert.Equal("Olá mundo", accepted.Message!.Text);
    }

    [Fact]
    public void Validate_NfcAndNfdInputs_ProduceSameCommonField()
    {
        var precomposed = ChatInputValidator.Validate(CreateMessage("café"), DefaultLimits);
        var decomposed = ChatInputValidator.Validate(CreateMessage("cafe\u0301"), DefaultLimits);

        Assert.True(precomposed.IsAccepted);
        Assert.True(decomposed.IsAccepted);
        Assert.Equal(precomposed.Message, decomposed.Message);
    }

    [Fact]
    public void Validate_PreservesOnlyMinimalOperationalIdentityAndOrigin()
    {
        var result = ChatInputValidator.Validate(CreateMessage("hello"), DefaultLimits);

        Assert.True(result.IsAccepted);
        var normalized = result.Message!;
        Assert.Equal("youtube-live", normalized.ProviderId.ToString());
        Assert.Equal("channel-1", normalized.ChannelReference);
        Assert.Equal("message-1", normalized.MessageReference);
        Assert.Equal("sender-1", normalized.SenderReference);
        Assert.Equal("hello", normalized.Text);
        Assert.Equal(new DateTimeOffset(2026, 10, 8, 12, 0, 0, TimeSpan.Zero), normalized.ReceivedAtUtc);

        var expectedMembers = new[]
        {
            "ProviderId", "ChannelReference", "SenderReference", "MessageReference", "Text", "ReceivedAtUtc",
        };
        var actualMembers = typeof(NormalizedChatMessage)
            .GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
            .Select(property => property.Name);
        Assert.Equal(expectedMembers, actualMembers);
    }

    [Fact]
    public void Validate_TrimsOperationalIdentityReferences()
    {
        var result = ChatInputValidator.Validate(CreateMessage("hello", channel: "  c1  ", message: "  m1  ", sender: "  s1  "), DefaultLimits);

        Assert.True(result.IsAccepted);
        Assert.Equal("c1", result.Message!.ChannelReference);
        Assert.Equal("m1", result.Message!.MessageReference);
        Assert.Equal("s1", result.Message!.SenderReference);
    }

    [Theory]
    [InlineData("   ")]
    [InlineData("\t\r\n")]
    [InlineData("\u00A0")]
    public void Validate_WhitespaceOnlyText_IsRejectedAsMissingRequiredField(string text)
    {
        var result = ChatInputValidator.Validate(CreateMessage(text), DefaultLimits);

        Assert.True(result.IsRejected);
        Assert.Null(result.Message);
        Assert.Equal(InputRejectionReason.MissingRequiredField, result.Rejection!.Reason);
    }

    [Fact]
    public void Validate_NullText_IsRejectedWithoutThrowing()
    {
        var result = ChatInputValidator.Validate(CreateMessage(null!), DefaultLimits);

        Assert.True(result.IsRejected);
        Assert.Equal(InputRejectionReason.MissingRequiredField, result.Rejection!.Reason);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Validate_MissingChannelReference_IsRejected(string channel)
    {
        var result = ChatInputValidator.Validate(CreateMessage("hello", channel: channel), DefaultLimits);

        Assert.True(result.IsRejected);
        Assert.Equal(InputRejectionReason.MissingRequiredField, result.Rejection!.Reason);
    }

    [Fact]
    public void Validate_MissingMessageReference_IsRejected()
    {
        var result = ChatInputValidator.Validate(CreateMessage("hello", message: null), DefaultLimits);

        Assert.True(result.IsRejected);
        Assert.Equal(InputRejectionReason.MissingRequiredField, result.Rejection!.Reason);
    }

    [Fact]
    public void Validate_MissingSenderReference_IsRejected()
    {
        var result = ChatInputValidator.Validate(CreateMessage("hello", sender: null), DefaultLimits);

        Assert.True(result.IsRejected);
        Assert.Equal(InputRejectionReason.MissingRequiredField, result.Rejection!.Reason);
    }

    [Fact]
    public void Validate_ControlCharacterInReference_IsRejectedAsMalformedReference()
    {
        var result = ChatInputValidator.Validate(CreateMessage("hello", channel: "c\u0007h"), DefaultLimits);

        Assert.True(result.IsRejected);
        Assert.Equal(InputRejectionReason.MalformedReference, result.Rejection!.Reason);
    }

    [Fact]
    public void Validate_ControlCharacterInText_IsRejectedAsMalformedText()
    {
        var result = ChatInputValidator.Validate(CreateMessage("hello\u0007world"), DefaultLimits);

        Assert.True(result.IsRejected);
        Assert.Equal(InputRejectionReason.MalformedText, result.Rejection!.Reason);
    }

    [Fact]
    public void Validate_UnpairedHighSurrogate_IsRejectedAsMalformedEncoding()
    {
        var result = ChatInputValidator.Validate(CreateMessage("bad\ud800text"), DefaultLimits);

        Assert.True(result.IsRejected);
        Assert.Equal(InputRejectionReason.MalformedEncoding, result.Rejection!.Reason);
    }

    [Fact]
    public void Validate_UnpairedLowSurrogate_IsRejectedAsMalformedEncoding()
    {
        var result = ChatInputValidator.Validate(CreateMessage("bad\udc00text"), DefaultLimits);

        Assert.True(result.IsRejected);
        Assert.Equal(InputRejectionReason.MalformedEncoding, result.Rejection!.Reason);
    }

    [Fact]
    public void Validate_WellFormedSurrogatePair_IsAccepted()
    {
        var result = ChatInputValidator.Validate(CreateMessage("boa \ud83d\ude00 mensagem"), DefaultLimits);

        Assert.True(result.IsAccepted);
        Assert.Equal("boa \ud83d\ude00 mensagem", result.Message!.Text);
    }

    [Fact]
    public void Validate_OversizeText_IsRejectedAsOversizeText()
    {
        var limits = InputValidationLimits.Create(maximumTextLength: 4, maximumReferenceLength: 64);
        var result = ChatInputValidator.Validate(CreateMessage("hello"), limits);

        Assert.True(result.IsRejected);
        Assert.Equal(InputRejectionReason.OversizeText, result.Rejection!.Reason);
    }

    [Fact]
    public void Validate_RawOversizeWhitespaceText_IsRejectedBeforeScanningContent()
    {
        var limits = InputValidationLimits.Create(maximumTextLength: 4, maximumReferenceLength: 64);
        var result = ChatInputValidator.Validate(CreateMessage("     "), limits);

        Assert.True(result.IsRejected);
        Assert.Equal(InputRejectionReason.OversizeText, result.Rejection!.Reason);
    }

    [Fact]
    public void Validate_OversizeReference_IsRejectedAsOversizeReference()
    {
        var limits = InputValidationLimits.Create(maximumTextLength: 128, maximumReferenceLength: 4);
        var result = ChatInputValidator.Validate(CreateMessage("hello", channel: "toolongreference"), limits);

        Assert.True(result.IsRejected);
        Assert.Equal(InputRejectionReason.OversizeReference, result.Rejection!.Reason);
    }

    [Fact]
    public void Validate_RawOversizeReference_IsRejectedBeforeTrimming()
    {
        var limits = InputValidationLimits.Create(maximumTextLength: 128, maximumReferenceLength: 4);
        var result = ChatInputValidator.Validate(
            CreateMessage("hello", channel: "  c1  ", message: "m1", sender: "s1"),
            limits);

        Assert.True(result.IsRejected);
        Assert.Equal(InputRejectionReason.OversizeReference, result.Rejection!.Reason);
    }

    [Fact]
    public void Validate_UnpairedSurrogateInReference_IsRejectedAsMalformedEncoding()
    {
        var result = ChatInputValidator.Validate(CreateMessage("hello", channel: "bad\ud800reference"), DefaultLimits);

        Assert.True(result.IsRejected);
        Assert.Equal(InputRejectionReason.MalformedEncoding, result.Rejection!.Reason);
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 0)]
    [InlineData(-1, 1)]
    [InlineData(1, -1)]
    public void InputValidationLimits_Create_RejectsNonPositiveBounds(int textLength, int referenceLength)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => InputValidationLimits.Create(textLength, referenceLength));
    }

    [Fact]
    public void InputValidationLimits_ExposeNoWriteableState()
    {
        var limits = InputValidationLimits.Create(10, 20);

        Assert.Equal(10, limits.MaximumTextLength);
        Assert.Equal(20, limits.MaximumReferenceLength);
        Assert.All(limits.GetType().GetProperties(), property => Assert.False(property.CanWrite));
    }

    [Fact]
    public void Validate_NullInput_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => ChatInputValidator.Validate(null!, DefaultLimits));
    }

    [Fact]
    public void Validate_NullLimits_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => ChatInputValidator.Validate(CreateMessage("hello"), null!));
    }

    [Fact]
    public void Validate_DefaultReceivedAtUtc_IsRejectedAsMalformedTimestamp()
    {
        var message = CreateMessage("hello") with { ReceivedAtUtc = default };

        var result = ChatInputValidator.Validate(message, DefaultLimits);

        Assert.True(result.IsRejected);
        Assert.Null(result.Message);
        Assert.Equal(InputRejectionReason.MalformedTimestamp, result.Rejection!.Reason);
    }

    [Fact]
    public void Validate_ValidReceivedAtUtc_IsPreserved()
    {
        var result = ChatInputValidator.Validate(CreateMessage("hello"), DefaultLimits);

        Assert.True(result.IsAccepted);
        Assert.Equal(new DateTimeOffset(2026, 10, 8, 12, 0, 0, TimeSpan.Zero), result.Message!.ReceivedAtUtc);
    }

    [Fact]
    public void ValidationResult_Factories_EnforceNonNullOutcomes()
    {
        Assert.Throws<ArgumentNullException>(() => ChatInputValidationResult.Accepted(null!));
        Assert.Throws<ArgumentNullException>(() => ChatInputValidationResult.Rejected(null!));
    }
}

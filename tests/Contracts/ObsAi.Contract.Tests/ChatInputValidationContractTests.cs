using System.Reflection;
using ObsAi.Application.Contracts.Chat;
using ObsAi.Application.Contracts.Common;
using ObsAi.Application.Validation;
using Xunit;

namespace ObsAi.Contract.Tests;

public sealed class ChatInputValidationContractTests
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
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    [InlineData("x")]
    [InlineData("\u0007x")]
    public void ValidationOutcome_IsAlwaysExactlyOneFailClosedState(string? text)
    {
        var result = ChatInputValidator.Validate(CreateMessage(text!), DefaultLimits);

        if (result.IsAccepted)
        {
            Assert.NotNull(result.Message);
            Assert.Null(result.Rejection);
            Assert.False(result.IsRejected);
        }
        else
        {
            Assert.True(result.IsRejected);
            Assert.Null(result.Message);
            Assert.NotNull(result.Rejection);
            Assert.False(result.IsAccepted);
        }
    }

    [Fact]
    public void ProviderOrigin_IsPreservedIntoNormalizedMessage()
    {
        var provider = ProviderId.Create("youtube-live");

        var result = ChatInputValidator.Validate(
            new ChatMessage(provider, "channel-1", "message-1", "sender-1", "hello", DateTimeOffset.UtcNow),
            DefaultLimits);

        Assert.True(result.IsAccepted);
        Assert.Equal(provider, result.Message!.ProviderId);
    }

    [Fact]
    public void InputRejectionSurface_ExposesOnlySafeDiagnostic()
    {
        var publicMembers = typeof(InputRejection).GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        var stringAccessors = publicMembers
            .Where(member => member.MemberType is MemberTypes.Property or MemberTypes.Field or MemberTypes.Method)
            .Where(member => member.Name is not nameof(InputRejection.ToString))
            .Where(member => member switch
            {
                PropertyInfo property => property.PropertyType == typeof(string),
                FieldInfo field => field.FieldType == typeof(string),
                MethodInfo method => method.ReturnType == typeof(string),
                _ => false,
            })
            .ToList();

        Assert.DoesNotContain(stringAccessors, member => member.Name.Contains("Text", StringComparison.Ordinal)
            || member.Name.Contains("Payload", StringComparison.Ordinal)
            || member.Name.Contains("Channel", StringComparison.Ordinal)
            || member.Name.Contains("Sender", StringComparison.Ordinal)
            || member.Name.Contains("Message", StringComparison.Ordinal));
    }

    [Fact]
    public void NormalizedChatMessage_ExposesOnlyMinimalOperationalFields()
    {
        var expected = new[]
        {
            "ProviderId", "ChannelReference", "SenderReference", "MessageReference", "Text", "ReceivedAtUtc",
        };

        var actual = typeof(NormalizedChatMessage)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(property => property.Name)
            .ToArray();

        Assert.Equal(expected, actual);
    }

    private static readonly string[] SecretLikeFragments =
    {
        "Secret", "Credential", "Password", "Token", "ApiKey",
    };

    [Fact]
    public void ValidationSurface_HasNoSecretLikeAccessors()
    {
        var exported = new[]
        {
            typeof(ChatInputValidator), typeof(InputValidationLimits), typeof(InputRejection),
            typeof(InputRejectionReason), typeof(ChatInputValidationResult), typeof(NormalizedChatMessage),
        };

        var exposed = exported
            .SelectMany(type => new[] { type.Name }.Concat(type.GetProperties().Select(property => property.Name)))
            .ToList();

        Assert.DoesNotContain(exposed, name =>
            SecretLikeFragments.Any(fragment => name.Contains(fragment, StringComparison.OrdinalIgnoreCase)));
    }
}

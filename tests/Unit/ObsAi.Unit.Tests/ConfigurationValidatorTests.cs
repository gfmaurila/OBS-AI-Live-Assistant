using ObsAi.Application.Configuration;
using ObsAi.Application.Queues;
using ObsAi.Domain.Profiles;
using Xunit;

namespace ObsAi.Unit.Tests;

public sealed class ConfigurationValidatorTests
{
    private static readonly IReadOnlyCollection<string> DuplicateTriggers = new[] { "!IA", "!ia" };
    private static readonly IReadOnlyCollection<string> TooManyTriggers = new[] { "!first", "!second" };

    [Fact]
    public void Validate_ValidConfigurationProducesImmutableRuntimeSnapshot()
    {
        var sourceTriggers = new List<string> { "  !ia  ", "@assistente" };
        var draft = ConfigurationTestData.CreateDraft(
            triggers: sourceTriggers,
            aiProvider: ConfigurationTestData.CreateProvider(),
            rateLimits: new RateLimitDraft(20, 15, 4),
            inputLimits: new InputLimitsDraft(600, 80),
            queues: new[]
            {
                new QueueSettingsDraft(QueueKind.Request, 20, 3, SaturationPolicy.DiscardOldest),
            });

        var result = ConfigurationValidator.Validate(draft, ConfigurationTestData.CreatePolicy());
        sourceTriggers.Add("later");

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
        Assert.Collection(
            result.Configuration!.EnabledTriggers,
            trigger => Assert.Equal("!ia", trigger),
            trigger => Assert.Equal("@assistente", trigger));
        Assert.Equal(20, result.Configuration.RateLimits.UserCooldownSeconds);
        Assert.Equal(600, result.Configuration.InputLimits.MaximumTextLength);
        Assert.Equal(20, result.Configuration.Queues[QueueKind.Request].Capacity);
        Assert.Equal(10, result.Configuration.Queues[QueueKind.Response].Capacity);
        Assert.Equal("approved-ai", result.Configuration.AiProvider!.ProviderId.Value);
    }

    [Fact]
    public void Validate_OmittedOptionalValuesUseExplicitPolicyDefaults()
    {
        var profile = ConfigurationTestData.CreateProfile();
        var result = ConfigurationValidator.Validate(
            new ConfigurationDraft(
                new[] { profile },
                profile.Id,
                null,
                null,
                null,
                null,
                null,
                null,
                null),
            ConfigurationTestData.CreatePolicy());

        Assert.True(result.IsValid);
        Assert.False(result.Configuration!.TtsEnabled);
        Assert.Equal(10, result.Configuration.RateLimits.UserCooldownSeconds);
        Assert.Equal(5, result.Configuration.RateLimits.GlobalCooldownSeconds);
        Assert.Equal(3, result.Configuration.RateLimits.RequestsPerUser);
        Assert.Equal(500, result.Configuration.InputLimits.MaximumTextLength);
        Assert.All(result.Configuration.Queues.Values, queue => Assert.Equal(10, queue.Capacity));
    }

    [Fact]
    public void Validate_NullConfigurationIsRejectedSafely()
    {
        var result = ConfigurationValidator.Validate(null, ConfigurationTestData.CreatePolicy());

        Assert.False(result.IsValid);
        Assert.Equal(ConfigurationErrorCode.MissingRequiredValue, Assert.Single(result.Errors).Code);
    }

    [Fact]
    public void Validate_MissingProfilesIsRejected()
    {
        var profileId = AssistantProfileId.Create(ConfigurationTestData.PrimaryProfileValue);
        var draft = new ConfigurationDraft(null, profileId, null, false, null, null, null, null, null);

        var result = ConfigurationValidator.Validate(draft, ConfigurationTestData.CreatePolicy());

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.Path == "profiles");
    }

    [Fact]
    public void Validate_SelectedProfileMustBelongToConfiguration()
    {
        var draft = ConfigurationTestData.CreateDraft(
            selectedProfileId: AssistantProfileId.Create(Guid.Parse("22222222-2222-2222-2222-222222222222")));

        var result = ConfigurationValidator.Validate(draft, ConfigurationTestData.CreatePolicy());

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.Path == "selectedProfileId");
    }

    [Fact]
    public void Validate_DuplicateProfileIdentifiersAreRejected()
    {
        var profiles = new[]
        {
            ConfigurationTestData.CreateProfile(name: "Um"),
            ConfigurationTestData.CreateProfile(name: "Dois"),
        };

        var result = ConfigurationValidator.Validate(
            ConfigurationTestData.CreateDraft(profiles: profiles),
            ConfigurationTestData.CreatePolicy());

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.Code == ConfigurationErrorCode.DuplicateValue);
    }

    [Fact]
    public void Validate_ProfileCountAbovePolicyLimitIsRejected()
    {
        var profiles = new[]
        {
            ConfigurationTestData.CreateProfile(),
            ConfigurationTestData.CreateProfile(Guid.Parse("44444444-4444-4444-4444-444444444444"), "Secundário"),
        };

        var result = ConfigurationValidator.Validate(
            ConfigurationTestData.CreateDraft(profiles: profiles),
            ConfigurationTestData.CreatePolicy(maximumProfiles: 1));

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.Code == ConfigurationErrorCode.TooManyItems);
    }

    [Fact]
    public void Validate_ProfileCreatedWithLooserLimitsIsRevalidatedAgainstConfigurationPolicy()
    {
        var profile = ConfigurationTestData.CreateProfile(name: "long-name");

        var result = ConfigurationValidator.Validate(
            ConfigurationTestData.CreateDraft(profiles: new[] { profile }),
            ConfigurationTestData.CreatePolicy(maximumProfileNameLength: 4));

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.Path == "profiles" && error.Code == ConfigurationErrorCode.OutOfRange);
    }

    [Theory]
    [InlineData(-1, 5, 3)]
    [InlineData(301, 5, 3)]
    [InlineData(10, -1, 3)]
    [InlineData(10, 5, 51)]
    public void Validate_RateLimitsOutsideApprovedRangesAreRejected(int user, int global, int requests)
    {
        var result = ConfigurationValidator.Validate(
            ConfigurationTestData.CreateDraft(rateLimits: new RateLimitDraft(user, global, requests)),
            ConfigurationTestData.CreatePolicy());

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.Code == ConfigurationErrorCode.OutOfRange);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(2000, 256)]
    public void Validate_InputLimitBoundariesAreAccepted(int textLength, int referenceLength)
    {
        var result = ConfigurationValidator.Validate(
            ConfigurationTestData.CreateDraft(inputLimits: new InputLimitsDraft(textLength, referenceLength)),
            ConfigurationTestData.CreatePolicy());

        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData(0, 64)]
    [InlineData(2001, 64)]
    [InlineData(500, 0)]
    [InlineData(500, 257)]
    public void Validate_InputLimitsOutsideApprovedRangesAreRejected(int textLength, int referenceLength)
    {
        var result = ConfigurationValidator.Validate(
            ConfigurationTestData.CreateDraft(inputLimits: new InputLimitsDraft(textLength, referenceLength)),
            ConfigurationTestData.CreatePolicy());

        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(101, 1)]
    [InlineData(10, 0)]
    [InlineData(10, 9)]
    public void Validate_QueueLimitsOutsideApprovedRangesAreRejected(int capacity, int concurrency)
    {
        var result = ConfigurationValidator.Validate(
            ConfigurationTestData.CreateDraft(
                queues: new[] { new QueueSettingsDraft(QueueKind.Request, capacity, concurrency, SaturationPolicy.Reject) }),
            ConfigurationTestData.CreatePolicy());

        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validate_UnknownQueueEnumIsRejected()
    {
        var result = ConfigurationValidator.Validate(
            ConfigurationTestData.CreateDraft(
                queues: new[] { new QueueSettingsDraft((QueueKind)999, 10, 1, SaturationPolicy.Reject) }),
            ConfigurationTestData.CreatePolicy());

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.Path == "queues" && error.Code == ConfigurationErrorCode.InvalidValue);
    }

    [Fact]
    public void Validate_DuplicateQueueKindsAreRejected()
    {
        var duplicate = new QueueSettingsDraft(QueueKind.Request, 10, 1, SaturationPolicy.Reject);
        var result = ConfigurationValidator.Validate(
            ConfigurationTestData.CreateDraft(queues: new[] { duplicate, duplicate }),
            ConfigurationTestData.CreatePolicy());

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.Code == ConfigurationErrorCode.DuplicateValue);
    }

    [Fact]
    public void Validate_NullQueueEntryIsRejectedSafely()
    {
        IReadOnlyCollection<QueueSettingsDraft> queues = new QueueSettingsDraft[] { null! };

        var result = ConfigurationValidator.Validate(
            ConfigurationTestData.CreateDraft(queues: queues),
            ConfigurationTestData.CreatePolicy());

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error =>
            error.Path == "queues" && error.Code == ConfigurationErrorCode.InvalidValue);
    }

    [Fact]
    public void Validate_QueueEntryCountAboveKnownKindsIsRejected()
    {
        var queue = new QueueSettingsDraft(QueueKind.Request, 10, 1, SaturationPolicy.Reject);
        var queues = Enumerable.Repeat(queue, Enum.GetValues<QueueKind>().Length + 1).ToArray();

        var result = ConfigurationValidator.Validate(
            ConfigurationTestData.CreateDraft(queues: queues),
            ConfigurationTestData.CreatePolicy());

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error =>
            error.Path == "queues" && error.Code == ConfigurationErrorCode.TooManyItems);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("bad\ntrigger")]
    public void Validate_MalformedTriggersAreRejected(string trigger)
    {
        var result = ConfigurationValidator.Validate(
            ConfigurationTestData.CreateDraft(triggers: new[] { trigger }),
            ConfigurationTestData.CreatePolicy());

        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validate_DuplicateTriggersAreCaseInsensitive()
    {
        var result = ConfigurationValidator.Validate(
            ConfigurationTestData.CreateDraft(triggers: DuplicateTriggers),
            ConfigurationTestData.CreatePolicy());

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.Code == ConfigurationErrorCode.DuplicateValue);
    }

    [Fact]
    public void Validate_TriggerCountAbovePolicyLimitIsRejected()
    {
        var result = ConfigurationValidator.Validate(
            ConfigurationTestData.CreateDraft(triggers: TooManyTriggers),
            ConfigurationTestData.CreatePolicy(maximumTriggers: 1));

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.Code == ConfigurationErrorCode.TooManyItems);
    }

    [Fact]
    public void Validate_UnknownProviderIsRejected()
    {
        var result = ConfigurationValidator.Validate(
            ConfigurationTestData.CreateDraft(aiProvider: ConfigurationTestData.CreateProvider("not-approved")),
            ConfigurationTestData.CreatePolicy());

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.Code == ConfigurationErrorCode.UnknownProvider);
    }

    [Fact]
    public void Validate_ProviderMustBeApprovedForItsConfiguredPurpose()
    {
        var result = ConfigurationValidator.Validate(
            ConfigurationTestData.CreateDraft(aiProvider: ConfigurationTestData.CreateProvider("approved-tts")),
            ConfigurationTestData.CreatePolicy());

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error =>
            error.Path == "aiProvider" && error.Code == ConfigurationErrorCode.UnknownProvider);
    }

    [Fact]
    public void Validate_EnabledTtsRequiresProviderReference()
    {
        var result = ConfigurationValidator.Validate(
            ConfigurationTestData.CreateDraft(ttsEnabled: true),
            ConfigurationTestData.CreatePolicy());

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.Code == ConfigurationErrorCode.IncompatibleValue);
    }

    [Fact]
    public void Validate_EnabledTtsWithApprovedProviderIsAccepted()
    {
        var result = ConfigurationValidator.Validate(
            ConfigurationTestData.CreateDraft(
                ttsEnabled: true,
                ttsProvider: ConfigurationTestData.CreateProvider("approved-tts")),
            ConfigurationTestData.CreatePolicy());

        Assert.True(result.IsValid);
    }
}

using System.Reflection;
using ObsAi.Application.Configuration;
using ObsAi.Application.Contracts.Common;
using ObsAi.Application.Queues;
using ObsAi.Domain.Profiles;
using Xunit;

namespace ObsAi.Security.Tests;

public sealed class ConfigurationSecurityTests
{
    private static readonly Guid ProfileValue = Guid.Parse("33333333-3333-3333-3333-333333333333");
    private static readonly IReadOnlyCollection<string> DefaultTriggers = new[] { "!ia" };
    private static readonly IReadOnlyCollection<string> FirstTriggers = new[] { "!first" };

    [Fact]
    public void CommonConfigurationSurface_ContainsNoSecretValueField()
    {
        var forbiddenNames = new[] { "Secret", "ApiKey", "Password", "AccessToken", "RefreshToken", "ConnectionString" };
        var types = new[]
        {
            typeof(ConfigurationDraft),
            typeof(AssistantConfiguration),
            typeof(ProviderSelection),
            typeof(RateLimitSettings),
        };

        foreach (var type in types)
        {
            var memberNames = type.GetMembers(BindingFlags.Public | BindingFlags.Instance)
                .Select(member => member.Name)
                .ToList();

            Assert.DoesNotContain(memberNames, name => forbiddenNames.Any(
                forbidden => name.Contains(forbidden, StringComparison.OrdinalIgnoreCase)));
        }
    }

    [Fact]
    public void ProviderSelection_ExposesOnlyTypedReferencesInsteadOfOptionValues()
    {
        var properties = typeof(ProviderSelection).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        Assert.Equal(
            new[] { typeof(ProviderId), typeof(ProviderConfigurationReference), typeof(CredentialReference) },
            properties.Select(property => Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType));
        Assert.DoesNotContain(properties, property => property.PropertyType == typeof(string));
    }

    [Fact]
    public void ProviderReferences_RedactTheirIdentifiersFromDiagnosticText()
    {
        var configurationReference = ProviderConfigurationReference.Create("provider/private-reference");
        var credentialReference = CredentialReference.Create("credential/private-reference");

        Assert.Equal("[provider-configuration-reference]", configurationReference.ToString());
        Assert.Equal("[credential-reference]", credentialReference.ToString());
        Assert.DoesNotContain("private-reference", configurationReference.ToString(), StringComparison.Ordinal);
        Assert.DoesNotContain("private-reference", credentialReference.ToString(), StringComparison.Ordinal);
    }

    [Fact]
    public void ValidationErrors_DoNotEchoUntrustedConfigurationContent()
    {
        const string sentinel = "SENSITIVE_SENTINEL_VALUE";
        var draft = CreateDraft(triggers: new[] { sentinel + new string('x', 64) });

        var result = ConfigurationValidator.Validate(draft, CreatePolicy());
        var diagnosticText = string.Join('|', result.Errors.Select(error => $"{error.Path}:{error.Message}"));

        Assert.False(result.IsValid);
        Assert.DoesNotContain(sentinel, diagnosticText, StringComparison.Ordinal);
    }

    [Fact]
    public void ExcessiveResourceLimits_AreRejectedBeforeRuntimeObjectsArePublished()
    {
        var draft = CreateDraft(
            inputLimits: new InputLimitsDraft(int.MaxValue, int.MaxValue),
            queues: new[]
            {
                new QueueSettingsDraft(QueueKind.Request, int.MaxValue, int.MaxValue, SaturationPolicy.Reject),
            });

        var result = ConfigurationValidator.Validate(draft, CreatePolicy());

        Assert.False(result.IsValid);
        Assert.Null(result.Configuration);
        Assert.Contains(result.Errors, error => error.Code == ConfigurationErrorCode.OutOfRange);
    }

    [Fact]
    public void InvalidCandidate_CannotReplacePreviouslyApprovedConfiguration()
    {
        var state = ConfigurationState.Create(CreateDraft(), CreatePolicy());
        var approved = state.Current;

        var result = state.TryApply(CreateDraft(inputLimits: new InputLimitsDraft(int.MaxValue, 64)));

        Assert.False(result.WasApplied);
        Assert.Same(approved, state.Current);
    }

    [Fact]
    public void ConfigurationSnapshot_DoesNotShareMutableTriggerOrQueueCollections()
    {
        var triggers = new List<string> { "!ia" };
        var queues = new List<QueueSettingsDraft>
        {
            new(QueueKind.Request, 20, 2, SaturationPolicy.Reject),
        };

        var result = ConfigurationValidator.Validate(CreateDraft(triggers, queues: queues), CreatePolicy());
        triggers[0] = "!changed";
        queues[0] = new QueueSettingsDraft(QueueKind.Request, 99, 8, SaturationPolicy.DiscardOldest);

        Assert.True(result.IsValid);
        Assert.Equal("!ia", Assert.Single(result.Configuration!.EnabledTriggers));
        Assert.Equal(20, result.Configuration.Queues[QueueKind.Request].Capacity);
    }

    [Fact]
    public void IndependentConfigurationStates_DoNotShareAppliedValues()
    {
        var first = ConfigurationState.Create(CreateDraft(), CreatePolicy());
        var second = ConfigurationState.Create(CreateDraft(), CreatePolicy());

        first.TryApply(CreateDraft(triggers: FirstTriggers));

        Assert.Equal("!first", Assert.Single(first.Current.EnabledTriggers));
        Assert.Equal("!ia", Assert.Single(second.Current.EnabledTriggers));
    }

    private static ConfigurationPolicy CreatePolicy()
    {
        var queues = Enum.GetValues<QueueKind>().ToDictionary(
            kind => kind,
            _ => QueueConfigurationBounds.Create(
                BoundedInt32Setting.Create(1, 100, 10),
                BoundedInt32Setting.Create(1, 8, 2),
                SaturationPolicy.Reject));

        return ConfigurationPolicy.Create(
            4,
            4,
            24,
            AssistantProfileLimits.Create(32, 64, 64, 256),
            BoundedInt32Setting.Create(0, 300, 10),
            BoundedInt32Setting.Create(0, 300, 5),
            BoundedInt32Setting.Create(1, 50, 3),
            BoundedInt32Setting.Create(1, 2_000, 500),
            BoundedInt32Setting.Create(1, 256, 64),
            queues,
            new[] { ProviderId.Create("approved-ai") },
            Array.Empty<ProviderId>());
    }

    private static ConfigurationDraft CreateDraft(
        IReadOnlyCollection<string>? triggers = null,
        InputLimitsDraft? inputLimits = null,
        IReadOnlyCollection<QueueSettingsDraft>? queues = null)
    {
        var profile = AssistantProfile.Create(
            AssistantProfileId.Create(ProfileValue),
            "Principal",
            "Prestativo",
            "Conciso",
            "Responda de modo útil.",
            AssistantProfileLimits.Create(32, 64, 64, 256));

        return new ConfigurationDraft(
            new[] { profile },
            profile.Id,
            triggers ?? DefaultTriggers,
            false,
            null,
            null,
            null,
            inputLimits,
            queues);
    }
}

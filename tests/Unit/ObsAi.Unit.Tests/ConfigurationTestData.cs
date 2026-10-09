using ObsAi.Application.Configuration;
using ObsAi.Application.Contracts.Common;
using ObsAi.Application.Queues;
using ObsAi.Domain.Profiles;

namespace ObsAi.Unit.Tests;

internal static class ConfigurationTestData
{
    internal static readonly Guid PrimaryProfileValue = Guid.Parse("11111111-1111-1111-1111-111111111111");
    private static readonly IReadOnlyCollection<string> DefaultTriggers = new[] { "!ia" };

    internal static ConfigurationPolicy CreatePolicy(
        int maximumProfiles = 4,
        int maximumTriggers = 4,
        int maximumTriggerLength = 24,
        int maximumProfileNameLength = 32)
    {
        var queueBounds = Enum.GetValues<QueueKind>().ToDictionary(
            kind => kind,
            _ => QueueConfigurationBounds.Create(
                BoundedInt32Setting.Create(1, 100, 10),
                BoundedInt32Setting.Create(1, 8, 2),
                SaturationPolicy.Reject));

        return ConfigurationPolicy.Create(
            maximumProfiles,
            maximumTriggers,
            maximumTriggerLength,
            AssistantProfileLimits.Create(maximumProfileNameLength, 64, 64, 256),
            BoundedInt32Setting.Create(0, 300, 10),
            BoundedInt32Setting.Create(0, 300, 5),
            BoundedInt32Setting.Create(1, 50, 3),
            BoundedInt32Setting.Create(1, 2_000, 500),
            BoundedInt32Setting.Create(1, 256, 64),
            queueBounds,
            new[] { ProviderId.Create("approved-ai") },
            new[] { ProviderId.Create("approved-tts") });
    }

    internal static AssistantProfile CreateProfile(Guid? id = null, string name = "Principal") =>
        AssistantProfile.Create(
            AssistantProfileId.Create(id ?? PrimaryProfileValue),
            name,
            "Prestativo",
            "Conciso",
            "Responda de modo útil.",
            AssistantProfileLimits.Create(128, 128, 128, 512));

    internal static ProviderSelection CreateProvider(string providerId = "approved-ai") =>
        ProviderSelection.Create(
            ProviderId.Create(providerId),
            ProviderConfigurationReference.Create("configuration/default"),
            CredentialReference.Create("credential/default"));

    internal static ConfigurationDraft CreateDraft(
        IReadOnlyCollection<AssistantProfile>? profiles = null,
        AssistantProfileId? selectedProfileId = null,
        IReadOnlyCollection<string>? triggers = null,
        bool? ttsEnabled = false,
        ProviderSelection? aiProvider = null,
        ProviderSelection? ttsProvider = null,
        RateLimitDraft? rateLimits = null,
        InputLimitsDraft? inputLimits = null,
        IReadOnlyCollection<QueueSettingsDraft>? queues = null)
    {
        var configuredProfiles = profiles ?? new[] { CreateProfile() };
        return new ConfigurationDraft(
            configuredProfiles,
            selectedProfileId ?? configuredProfiles.First().Id,
            triggers ?? DefaultTriggers,
            ttsEnabled,
            aiProvider,
            ttsProvider,
            rateLimits,
            inputLimits,
            queues);
    }
}

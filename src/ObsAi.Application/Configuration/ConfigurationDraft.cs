using ObsAi.Application.Contracts.Common;
using ObsAi.Application.Queues;
using ObsAi.Domain.Profiles;

namespace ObsAi.Application.Configuration;

/// <summary>Untrusted candidate configuration validated atomically before use.</summary>
public sealed record ConfigurationDraft(
    IReadOnlyCollection<AssistantProfile>? Profiles,
    AssistantProfileId? SelectedProfileId,
    IReadOnlyCollection<string>? EnabledTriggers,
    bool? TtsEnabled,
    ProviderSelection? AiProvider,
    ProviderSelection? TtsProvider,
    RateLimitDraft? RateLimits,
    InputLimitsDraft? InputLimits,
    IReadOnlyCollection<QueueSettingsDraft>? Queues);

/// <summary>Provider selection composed exclusively of opaque identifiers and references.</summary>
public sealed record ProviderSelection
{
    private ProviderSelection(
        ProviderId providerId,
        ProviderConfigurationReference configurationReference,
        CredentialReference? credentialReference)
    {
        ProviderId = providerId;
        ConfigurationReference = configurationReference;
        CredentialReference = credentialReference;
    }

    public ProviderId ProviderId { get; }

    public ProviderConfigurationReference ConfigurationReference { get; }

    public CredentialReference? CredentialReference { get; }

    public static ProviderSelection Create(
        ProviderId providerId,
        ProviderConfigurationReference configurationReference,
        CredentialReference? credentialReference = null)
    {
        ArgumentNullException.ThrowIfNull(providerId);
        ArgumentNullException.ThrowIfNull(configurationReference);
        return new ProviderSelection(providerId, configurationReference, credentialReference);
    }

    public override string ToString() => $"[provider-selection:{ProviderId}]";
}

public sealed record RateLimitDraft(int? UserCooldownSeconds, int? GlobalCooldownSeconds, int? RequestsPerUser);

public sealed record InputLimitsDraft(int? MaximumTextLength, int? MaximumReferenceLength);

public sealed record QueueSettingsDraft(
    QueueKind Kind,
    int? Capacity,
    int? MaxConcurrency,
    SaturationPolicy? SaturationPolicy);

using System.Collections.ObjectModel;
using ObsAi.Application.Contracts.Common;
using ObsAi.Application.Queues;
using ObsAi.Domain.Profiles;

namespace ObsAi.Application.Configuration;

/// <summary>
/// Trusted composition policy that supplies safe ranges, explicit defaults and the provider
/// allowlist. Product values are injected here instead of being invented by the architecture.
/// </summary>
public sealed class ConfigurationPolicy
{
    private ConfigurationPolicy(
        int maximumProfiles,
        int maximumEnabledTriggers,
        int maximumTriggerLength,
        AssistantProfileLimits profileLimits,
        BoundedInt32Setting userCooldownSeconds,
        BoundedInt32Setting globalCooldownSeconds,
        BoundedInt32Setting requestsPerUser,
        BoundedInt32Setting inputTextLength,
        BoundedInt32Setting inputReferenceLength,
        IReadOnlyDictionary<QueueKind, QueueConfigurationBounds> queueBounds,
        IReadOnlyList<ProviderId> allowedAiProviders,
        IReadOnlyList<ProviderId> allowedTtsProviders)
    {
        MaximumProfiles = maximumProfiles;
        MaximumEnabledTriggers = maximumEnabledTriggers;
        MaximumTriggerLength = maximumTriggerLength;
        ProfileLimits = profileLimits;
        UserCooldownSeconds = userCooldownSeconds;
        GlobalCooldownSeconds = globalCooldownSeconds;
        RequestsPerUser = requestsPerUser;
        InputTextLength = inputTextLength;
        InputReferenceLength = inputReferenceLength;
        QueueBounds = queueBounds;
        AllowedAiProviders = allowedAiProviders;
        AllowedTtsProviders = allowedTtsProviders;
    }

    public int MaximumProfiles { get; }

    public int MaximumEnabledTriggers { get; }

    public int MaximumTriggerLength { get; }

    public AssistantProfileLimits ProfileLimits { get; }

    public BoundedInt32Setting UserCooldownSeconds { get; }

    public BoundedInt32Setting GlobalCooldownSeconds { get; }

    public BoundedInt32Setting RequestsPerUser { get; }

    public BoundedInt32Setting InputTextLength { get; }

    public BoundedInt32Setting InputReferenceLength { get; }

    public IReadOnlyDictionary<QueueKind, QueueConfigurationBounds> QueueBounds { get; }

    public IReadOnlyList<ProviderId> AllowedAiProviders { get; }

    public IReadOnlyList<ProviderId> AllowedTtsProviders { get; }

    public static ConfigurationPolicy Create(
        int maximumProfiles,
        int maximumEnabledTriggers,
        int maximumTriggerLength,
        AssistantProfileLimits profileLimits,
        BoundedInt32Setting userCooldownSeconds,
        BoundedInt32Setting globalCooldownSeconds,
        BoundedInt32Setting requestsPerUser,
        BoundedInt32Setting inputTextLength,
        BoundedInt32Setting inputReferenceLength,
        IReadOnlyDictionary<QueueKind, QueueConfigurationBounds> queueBounds,
        IEnumerable<ProviderId> allowedAiProviders,
        IEnumerable<ProviderId> allowedTtsProviders)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maximumProfiles);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maximumEnabledTriggers);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maximumTriggerLength);
        ArgumentNullException.ThrowIfNull(profileLimits);
        ArgumentNullException.ThrowIfNull(userCooldownSeconds);
        ArgumentNullException.ThrowIfNull(globalCooldownSeconds);
        ArgumentNullException.ThrowIfNull(requestsPerUser);
        ArgumentNullException.ThrowIfNull(inputTextLength);
        ArgumentNullException.ThrowIfNull(inputReferenceLength);
        ArgumentNullException.ThrowIfNull(queueBounds);
        ArgumentNullException.ThrowIfNull(allowedAiProviders);
        ArgumentNullException.ThrowIfNull(allowedTtsProviders);

        var queueCopy = new Dictionary<QueueKind, QueueConfigurationBounds>();
        foreach (var kind in Enum.GetValues<QueueKind>())
        {
            if (!queueBounds.TryGetValue(kind, out var bounds) || bounds is null)
            {
                throw new ArgumentException("Every queue kind must have approved bounds.", nameof(queueBounds));
            }

            queueCopy.Add(kind, bounds);
        }

        if (queueBounds.Count != queueCopy.Count || queueBounds.Keys.Any(kind => !Enum.IsDefined(kind)))
        {
            throw new ArgumentException("Queue bounds contain an unknown or duplicate queue kind.", nameof(queueBounds));
        }

        var aiProviders = CopyProviderAllowlist(allowedAiProviders, nameof(allowedAiProviders));
        var ttsProviders = CopyProviderAllowlist(allowedTtsProviders, nameof(allowedTtsProviders));

        return new ConfigurationPolicy(
            maximumProfiles,
            maximumEnabledTriggers,
            maximumTriggerLength,
            profileLimits,
            userCooldownSeconds,
            globalCooldownSeconds,
            requestsPerUser,
            inputTextLength,
            inputReferenceLength,
            new ReadOnlyDictionary<QueueKind, QueueConfigurationBounds>(queueCopy),
            aiProviders,
            ttsProviders);
    }

    internal bool IsAiProviderAllowed(ProviderId providerId) => AllowedAiProviders.Contains(providerId);

    internal bool IsTtsProviderAllowed(ProviderId providerId) => AllowedTtsProviders.Contains(providerId);

    private static ReadOnlyCollection<ProviderId> CopyProviderAllowlist(
        IEnumerable<ProviderId> providers,
        string parameterName)
    {
        var copy = providers.ToHashSet();
        if (copy.Any(provider => provider is null))
        {
            throw new ArgumentException("Provider allowlist cannot contain null entries.", parameterName);
        }

        return new ReadOnlyCollection<ProviderId>(copy.ToList());
    }
}

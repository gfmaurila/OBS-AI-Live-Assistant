using System.Collections.ObjectModel;
using ObsAi.Application.Queues;
using ObsAi.Application.Validation;
using ObsAi.Domain.Profiles;

namespace ObsAi.Application.Configuration;

/// <summary>Immutable configuration snapshot that is safe to consume at runtime.</summary>
public sealed class AssistantConfiguration
{
    internal AssistantConfiguration(
        IReadOnlyList<AssistantProfile> profiles,
        AssistantProfileId selectedProfileId,
        IReadOnlyList<string> enabledTriggers,
        bool ttsEnabled,
        ProviderSelection? aiProvider,
        ProviderSelection? ttsProvider,
        RateLimitSettings rateLimits,
        InputValidationLimits inputLimits,
        IReadOnlyDictionary<QueueKind, WorkQueueSettings> queues)
    {
        Profiles = profiles;
        SelectedProfileId = selectedProfileId;
        EnabledTriggers = enabledTriggers;
        TtsEnabled = ttsEnabled;
        AiProvider = aiProvider;
        TtsProvider = ttsProvider;
        RateLimits = rateLimits;
        InputLimits = inputLimits;
        Queues = queues;
    }

    public IReadOnlyList<AssistantProfile> Profiles { get; }

    public AssistantProfileId SelectedProfileId { get; }

    public IReadOnlyList<string> EnabledTriggers { get; }

    public bool TtsEnabled { get; }

    public ProviderSelection? AiProvider { get; }

    public ProviderSelection? TtsProvider { get; }

    public RateLimitSettings RateLimits { get; }

    public InputValidationLimits InputLimits { get; }

    public IReadOnlyDictionary<QueueKind, WorkQueueSettings> Queues { get; }

    internal static AssistantConfiguration Create(
        IEnumerable<AssistantProfile> profiles,
        AssistantProfileId selectedProfileId,
        IEnumerable<string> enabledTriggers,
        bool ttsEnabled,
        ProviderSelection? aiProvider,
        ProviderSelection? ttsProvider,
        RateLimitSettings rateLimits,
        InputValidationLimits inputLimits,
        IDictionary<QueueKind, WorkQueueSettings> queues) =>
        new(
            new ReadOnlyCollection<AssistantProfile>(profiles.ToList()),
            selectedProfileId,
            new ReadOnlyCollection<string>(enabledTriggers.ToList()),
            ttsEnabled,
            aiProvider,
            ttsProvider,
            rateLimits,
            inputLimits,
            new ReadOnlyDictionary<QueueKind, WorkQueueSettings>(new Dictionary<QueueKind, WorkQueueSettings>(queues)));
}

/// <summary>Validated rate-limit values. Enforcement belongs to TASK-010.</summary>
public sealed record RateLimitSettings
{
    private RateLimitSettings(int userCooldownSeconds, int globalCooldownSeconds, int requestsPerUser)
    {
        UserCooldownSeconds = userCooldownSeconds;
        GlobalCooldownSeconds = globalCooldownSeconds;
        RequestsPerUser = requestsPerUser;
    }

    public int UserCooldownSeconds { get; }

    public int GlobalCooldownSeconds { get; }

    public int RequestsPerUser { get; }

    internal static RateLimitSettings Create(int userCooldownSeconds, int globalCooldownSeconds, int requestsPerUser) =>
        new(userCooldownSeconds, globalCooldownSeconds, requestsPerUser);
}

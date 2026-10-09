using System.Collections.ObjectModel;

namespace ObsAi.Application.Configuration;

/// <summary>
/// Owns the last valid configuration snapshot. Candidate validation and replacement are serialized
/// so readers never observe a partially applied configuration.
/// </summary>
public sealed class ConfigurationState
{
    private readonly object sync = new();
    private readonly ConfigurationPolicy policy;
    private AssistantConfiguration current;

    private ConfigurationState(ConfigurationPolicy policy, AssistantConfiguration initial)
    {
        this.policy = policy;
        current = initial;
    }

    public AssistantConfiguration Current => Volatile.Read(ref current);

    public static ConfigurationState Create(ConfigurationDraft initialDraft, ConfigurationPolicy policy)
    {
        ArgumentNullException.ThrowIfNull(initialDraft);
        ArgumentNullException.ThrowIfNull(policy);

        var validation = ConfigurationValidator.Validate(initialDraft, policy);
        if (!validation.IsValid)
        {
            throw new ArgumentException("Initial configuration is invalid.", nameof(initialDraft));
        }

        return new ConfigurationState(policy, validation.Configuration!);
    }

    public ConfigurationApplyResult TryApply(ConfigurationDraft? candidate)
    {
        lock (sync)
        {
            var validation = ConfigurationValidator.Validate(candidate, policy);
            if (!validation.IsValid)
            {
                return ConfigurationApplyResult.Rejected(current, validation.Errors);
            }

            Volatile.Write(ref current, validation.Configuration!);
            return ConfigurationApplyResult.Applied(current);
        }
    }
}

public sealed class ConfigurationApplyResult
{
    private ConfigurationApplyResult(
        bool wasApplied,
        AssistantConfiguration configuration,
        IReadOnlyList<ConfigurationError> errors)
    {
        WasApplied = wasApplied;
        Configuration = configuration;
        Errors = errors;
    }

    public bool WasApplied { get; }

    public AssistantConfiguration Configuration { get; }

    public IReadOnlyList<ConfigurationError> Errors { get; }

    internal static ConfigurationApplyResult Applied(AssistantConfiguration configuration) =>
        new(true, configuration, Array.Empty<ConfigurationError>());

    internal static ConfigurationApplyResult Rejected(
        AssistantConfiguration current,
        IEnumerable<ConfigurationError> errors) =>
        new(false, current, new ReadOnlyCollection<ConfigurationError>(errors.ToList()));
}

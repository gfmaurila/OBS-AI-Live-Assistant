using System.Collections.ObjectModel;
using ObsAi.Application.Queues;
using ObsAi.Application.Validation;
using ObsAi.Domain.Profiles;

namespace ObsAi.Application.Configuration;

public enum ConfigurationErrorCode
{
    MissingRequiredValue,
    TooManyItems,
    DuplicateValue,
    InvalidValue,
    OutOfRange,
    UnknownProvider,
    IncompatibleValue,
}

public sealed record ConfigurationError(string Path, ConfigurationErrorCode Code, string Message);

public sealed class ConfigurationValidationResult
{
    private ConfigurationValidationResult(AssistantConfiguration? configuration, IReadOnlyList<ConfigurationError> errors)
    {
        Configuration = configuration;
        Errors = errors;
    }

    public bool IsValid => Configuration is not null;

    public AssistantConfiguration? Configuration { get; }

    public IReadOnlyList<ConfigurationError> Errors { get; }

    internal static ConfigurationValidationResult Valid(AssistantConfiguration configuration) =>
        new(configuration, Array.Empty<ConfigurationError>());

    internal static ConfigurationValidationResult Invalid(IEnumerable<ConfigurationError> errors) =>
        new(null, new ReadOnlyCollection<ConfigurationError>(errors.ToList()));
}

/// <summary>Validates an entire candidate before creating a runtime-safe immutable snapshot.</summary>
public static class ConfigurationValidator
{
    public static ConfigurationValidationResult Validate(ConfigurationDraft? draft, ConfigurationPolicy policy)
    {
        ArgumentNullException.ThrowIfNull(policy);

        if (draft is null)
        {
            return Invalid("configuration", ConfigurationErrorCode.MissingRequiredValue, "Configuration is required.");
        }

        var errors = new List<ConfigurationError>();
        var profiles = ValidateProfiles(draft, policy, errors);
        var triggers = ValidateTriggers(draft.EnabledTriggers, policy, errors);
        ValidateProvider("aiProvider", draft.AiProvider, policy.IsAiProviderAllowed, errors);
        ValidateProvider("ttsProvider", draft.TtsProvider, policy.IsTtsProviderAllowed, errors);

        var ttsEnabled = draft.TtsEnabled ?? false;
        if (ttsEnabled && draft.TtsProvider is null)
        {
            errors.Add(Error("ttsProvider", ConfigurationErrorCode.IncompatibleValue, "TTS requires a configured provider reference."));
        }

        var rateLimits = ValidateRateLimits(draft.RateLimits, policy, errors);
        var inputLimits = ValidateInputLimits(draft.InputLimits, policy, errors);
        var queues = ValidateQueues(draft.Queues, policy, errors);

        if (errors.Count > 0 || profiles is null || draft.SelectedProfileId is null || rateLimits is null || inputLimits is null || queues is null)
        {
            return ConfigurationValidationResult.Invalid(errors);
        }

        return ConfigurationValidationResult.Valid(
            AssistantConfiguration.Create(
                profiles,
                draft.SelectedProfileId,
                triggers,
                ttsEnabled,
                draft.AiProvider,
                draft.TtsProvider,
                rateLimits,
                inputLimits,
                queues));
    }

    private static List<AssistantProfile>? ValidateProfiles(
        ConfigurationDraft draft,
        ConfigurationPolicy policy,
        List<ConfigurationError> errors)
    {
        if (draft.Profiles is null || draft.Profiles.Count == 0)
        {
            errors.Add(Error("profiles", ConfigurationErrorCode.MissingRequiredValue, "At least one assistant profile is required."));
            return null;
        }

        if (draft.Profiles.Count > policy.MaximumProfiles)
        {
            errors.Add(Error("profiles", ConfigurationErrorCode.TooManyItems, "Profile count exceeds the approved limit."));
            return null;
        }

        if (draft.Profiles.Any(profile => profile is null))
        {
            errors.Add(Error("profiles", ConfigurationErrorCode.InvalidValue, "Profiles cannot contain null entries."));
            return null;
        }

        var profiles = draft.Profiles.ToList();
        if (profiles.Select(profile => profile.Id).Distinct().Count() != profiles.Count)
        {
            errors.Add(Error("profiles", ConfigurationErrorCode.DuplicateValue, "Profile identifiers must be unique."));
        }

        foreach (var profile in profiles)
        {
            ValidateProfileText(profile.Name, policy.ProfileLimits.MaximumNameLength, errors);
            ValidateProfileText(profile.Personality, policy.ProfileLimits.MaximumPersonalityLength, errors);
            ValidateProfileText(profile.ResponseStyle, policy.ProfileLimits.MaximumResponseStyleLength, errors);
            ValidateProfileText(profile.BehaviorInstructions, policy.ProfileLimits.MaximumBehaviorInstructionsLength, errors);
        }

        if (draft.SelectedProfileId is null)
        {
            errors.Add(Error("selectedProfileId", ConfigurationErrorCode.MissingRequiredValue, "A selected profile is required."));
        }
        else if (!profiles.Any(profile => profile.Id == draft.SelectedProfileId))
        {
            errors.Add(Error("selectedProfileId", ConfigurationErrorCode.InvalidValue, "Selected profile must exist in the profile collection."));
        }

        return profiles;
    }

    private static void ValidateProfileText(string value, int maximumLength, List<ConfigurationError> errors)
    {
        if (value.Length > maximumLength)
        {
            errors.Add(Error("profiles", ConfigurationErrorCode.OutOfRange, "Profile text exceeds the approved limit."));
        }
    }

    private static IReadOnlyList<string> ValidateTriggers(
        IReadOnlyCollection<string>? candidates,
        ConfigurationPolicy policy,
        List<ConfigurationError> errors)
    {
        if (candidates is null)
        {
            return Array.Empty<string>();
        }

        if (candidates.Count > policy.MaximumEnabledTriggers)
        {
            errors.Add(Error("enabledTriggers", ConfigurationErrorCode.TooManyItems, "Trigger count exceeds the approved limit."));
            return Array.Empty<string>();
        }

        var normalized = new List<string>(candidates.Count);
        foreach (var candidate in candidates)
        {
            if (string.IsNullOrWhiteSpace(candidate))
            {
                errors.Add(Error("enabledTriggers", ConfigurationErrorCode.InvalidValue, "Triggers must be non-empty printable text."));
                continue;
            }

            var trigger = candidate.Trim();
            if (trigger.Length > policy.MaximumTriggerLength || trigger.Any(char.IsControl))
            {
                errors.Add(Error("enabledTriggers", ConfigurationErrorCode.OutOfRange, "Trigger is outside the approved format or length."));
                continue;
            }

            normalized.Add(trigger);
        }

        if (normalized.Distinct(StringComparer.OrdinalIgnoreCase).Count() != normalized.Count)
        {
            errors.Add(Error("enabledTriggers", ConfigurationErrorCode.DuplicateValue, "Triggers must be unique."));
        }

        return new ReadOnlyCollection<string>(normalized);
    }

    private static void ValidateProvider(
        string path,
        ProviderSelection? provider,
        Func<ObsAi.Application.Contracts.Common.ProviderId, bool> isAllowed,
        List<ConfigurationError> errors)
    {
        if (provider is not null && !isAllowed(provider.ProviderId))
        {
            errors.Add(Error(path, ConfigurationErrorCode.UnknownProvider, "Provider is not present in the approved allowlist."));
        }
    }

    private static RateLimitSettings? ValidateRateLimits(
        RateLimitDraft? draft,
        ConfigurationPolicy policy,
        List<ConfigurationError> errors)
    {
        var user = draft?.UserCooldownSeconds ?? policy.UserCooldownSeconds.DefaultValue;
        var global = draft?.GlobalCooldownSeconds ?? policy.GlobalCooldownSeconds.DefaultValue;
        var requests = draft?.RequestsPerUser ?? policy.RequestsPerUser.DefaultValue;

        var valid = ValidateRange("rateLimits.userCooldownSeconds", user, policy.UserCooldownSeconds, errors)
            & ValidateRange("rateLimits.globalCooldownSeconds", global, policy.GlobalCooldownSeconds, errors)
            & ValidateRange("rateLimits.requestsPerUser", requests, policy.RequestsPerUser, errors);

        return valid ? RateLimitSettings.Create(user, global, requests) : null;
    }

    private static InputValidationLimits? ValidateInputLimits(
        InputLimitsDraft? draft,
        ConfigurationPolicy policy,
        List<ConfigurationError> errors)
    {
        var text = draft?.MaximumTextLength ?? policy.InputTextLength.DefaultValue;
        var reference = draft?.MaximumReferenceLength ?? policy.InputReferenceLength.DefaultValue;

        var valid = ValidateRange("inputLimits.maximumTextLength", text, policy.InputTextLength, errors)
            & ValidateRange("inputLimits.maximumReferenceLength", reference, policy.InputReferenceLength, errors);

        return valid ? InputValidationLimits.Create(text, reference) : null;
    }

    private static Dictionary<QueueKind, WorkQueueSettings>? ValidateQueues(
        IReadOnlyCollection<QueueSettingsDraft>? drafts,
        ConfigurationPolicy policy,
        List<ConfigurationError> errors)
    {
        var candidates = drafts ?? Array.Empty<QueueSettingsDraft>();
        if (candidates.Count > policy.QueueBounds.Count)
        {
            errors.Add(Error("queues", ConfigurationErrorCode.TooManyItems, "Queue entry count exceeds the approved limit."));
            return null;
        }

        var supplied = new Dictionary<QueueKind, QueueSettingsDraft>();
        foreach (var draft in candidates)
        {
            if (draft is null)
            {
                errors.Add(Error("queues", ConfigurationErrorCode.InvalidValue, "Queues cannot contain null entries."));
                continue;
            }

            if (!Enum.IsDefined(draft.Kind))
            {
                errors.Add(Error("queues", ConfigurationErrorCode.InvalidValue, "Queue kind is unknown."));
                continue;
            }

            if (!supplied.TryAdd(draft.Kind, draft))
            {
                errors.Add(Error("queues", ConfigurationErrorCode.DuplicateValue, "Queue kinds must be unique."));
            }
        }

        var result = new Dictionary<QueueKind, WorkQueueSettings>();
        foreach (var (kind, bounds) in policy.QueueBounds)
        {
            supplied.TryGetValue(kind, out var draft);
            var capacity = draft?.Capacity ?? bounds.Capacity.DefaultValue;
            var concurrency = draft?.MaxConcurrency ?? bounds.MaxConcurrency.DefaultValue;
            var saturation = draft?.SaturationPolicy ?? bounds.DefaultSaturationPolicy;

            var valid = ValidateRange($"queues.{kind}.capacity", capacity, bounds.Capacity, errors)
                & ValidateRange($"queues.{kind}.maxConcurrency", concurrency, bounds.MaxConcurrency, errors);

            if (!Enum.IsDefined(saturation))
            {
                errors.Add(Error($"queues.{kind}.saturationPolicy", ConfigurationErrorCode.InvalidValue, "Saturation policy is unknown."));
                valid = false;
            }

            if (valid)
            {
                result.Add(kind, WorkQueueSettings.Create(kind, capacity, concurrency, saturation));
            }
        }

        return errors.Count == 0 ? result : null;
    }

    private static bool ValidateRange(
        string path,
        int value,
        BoundedInt32Setting range,
        List<ConfigurationError> errors)
    {
        if (range.Contains(value))
        {
            return true;
        }

        errors.Add(Error(path, ConfigurationErrorCode.OutOfRange, "Value is outside the approved range."));
        return false;
    }

    private static ConfigurationValidationResult Invalid(string path, ConfigurationErrorCode code, string message) =>
        ConfigurationValidationResult.Invalid(new[] { Error(path, code, message) });

    private static ConfigurationError Error(string path, ConfigurationErrorCode code, string message) => new(path, code, message);
}

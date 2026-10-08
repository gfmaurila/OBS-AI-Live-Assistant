namespace ObsAi.Domain.Profiles;

/// <summary>
/// Describes the approved personality, response style and behavior for the assistant.
/// </summary>
public sealed class AssistantProfile
{
    private AssistantProfile(
        AssistantProfileId id,
        string name,
        string personality,
        string responseStyle,
        string behaviorInstructions)
    {
        Id = id;
        Name = name;
        Personality = personality;
        ResponseStyle = responseStyle;
        BehaviorInstructions = behaviorInstructions;
    }

    public AssistantProfileId Id { get; }

    public string Name { get; }

    public string Personality { get; }

    public string ResponseStyle { get; }

    public string BehaviorInstructions { get; }

    public static AssistantProfile Create(
        AssistantProfileId id,
        string name,
        string personality,
        string responseStyle,
        string behaviorInstructions,
        AssistantProfileLimits limits)
    {
        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNull(limits);

        return new AssistantProfile(
            id,
            NormalizeRequiredText(name, limits.MaximumNameLength, nameof(name)),
            NormalizeRequiredText(personality, limits.MaximumPersonalityLength, nameof(personality)),
            NormalizeRequiredText(responseStyle, limits.MaximumResponseStyleLength, nameof(responseStyle)),
            NormalizeRequiredText(
                behaviorInstructions,
                limits.MaximumBehaviorInstructionsLength,
                nameof(behaviorInstructions)));
    }

    public AssistantProfile Update(
        string name,
        string personality,
        string responseStyle,
        string behaviorInstructions,
        AssistantProfileLimits limits) =>
        Create(Id, name, personality, responseStyle, behaviorInstructions, limits);

    public override string ToString() => $"[assistant-profile:{Id}]";

    private static string NormalizeRequiredText(string value, int maximumLength, string parameterName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, parameterName);

        var normalized = value.Trim();
        if (normalized.Length > maximumLength || normalized.Any(char.IsControl))
        {
            throw new ArgumentOutOfRangeException(parameterName, "Profile text must be bounded and printable.");
        }

        return normalized;
    }
}

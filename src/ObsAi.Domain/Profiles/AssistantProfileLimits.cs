namespace ObsAi.Domain.Profiles;

/// <summary>
/// Defines explicit bounds for assistant profile text without imposing product defaults.
/// </summary>
public sealed record AssistantProfileLimits
{
    private AssistantProfileLimits(
        int maximumNameLength,
        int maximumPersonalityLength,
        int maximumResponseStyleLength,
        int maximumBehaviorInstructionsLength)
    {
        MaximumNameLength = maximumNameLength;
        MaximumPersonalityLength = maximumPersonalityLength;
        MaximumResponseStyleLength = maximumResponseStyleLength;
        MaximumBehaviorInstructionsLength = maximumBehaviorInstructionsLength;
    }

    public int MaximumNameLength { get; }

    public int MaximumPersonalityLength { get; }

    public int MaximumResponseStyleLength { get; }

    public int MaximumBehaviorInstructionsLength { get; }

    public static AssistantProfileLimits Create(
        int maximumNameLength,
        int maximumPersonalityLength,
        int maximumResponseStyleLength,
        int maximumBehaviorInstructionsLength)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maximumNameLength);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maximumPersonalityLength);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maximumResponseStyleLength);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maximumBehaviorInstructionsLength);

        return new AssistantProfileLimits(
            maximumNameLength,
            maximumPersonalityLength,
            maximumResponseStyleLength,
            maximumBehaviorInstructionsLength);
    }
}

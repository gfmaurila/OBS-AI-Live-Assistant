namespace ObsAi.Domain.Context;

/// <summary>
/// Defines caller-supplied bounds for ephemeral live context.
/// </summary>
public sealed record LiveContextLimits
{
    private LiveContextLimits(int maximumFields, int maximumValueLength, int maximumTotalLength)
    {
        MaximumFields = maximumFields;
        MaximumValueLength = maximumValueLength;
        MaximumTotalLength = maximumTotalLength;
    }

    public int MaximumFields { get; }

    public int MaximumValueLength { get; }

    public int MaximumTotalLength { get; }

    public static LiveContextLimits Create(int maximumFields, int maximumValueLength, int maximumTotalLength)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maximumFields);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maximumValueLength);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maximumTotalLength);

        if (maximumFields > Enum.GetValues<LiveContextField>().Length)
        {
            throw new ArgumentOutOfRangeException(nameof(maximumFields), "Field limit cannot exceed the allowlist size.");
        }

        return new LiveContextLimits(maximumFields, maximumValueLength, maximumTotalLength);
    }
}

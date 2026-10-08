namespace ObsAi.Application.Validation;

/// <summary>
/// Immutable, validated bounds for untrusted chat input. Invalid limits are rejected at creation,
/// so no validator can be configured with unbounded field sizes. Values are intentionally not fixed
/// by the architecture: the streamer configures limits per product decision (SEC-002, SEC-019).
/// </summary>
public sealed record InputValidationLimits
{
    private InputValidationLimits(int maximumTextLength, int maximumReferenceLength)
    {
        MaximumTextLength = maximumTextLength;
        MaximumReferenceLength = maximumReferenceLength;
    }

    /// <summary>Gets the maximum accepted length, in characters, of the normalized message text.</summary>
    public int MaximumTextLength { get; }

    /// <summary>Gets the maximum accepted length, in characters, of each operational identity reference.</summary>
    public int MaximumReferenceLength { get; }

    /// <summary>
    /// Creates validated limits. Negative or zero bounds are rejected with
    /// <see cref="ArgumentOutOfRangeException"/>.
    /// </summary>
    public static InputValidationLimits Create(int maximumTextLength, int maximumReferenceLength)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maximumTextLength);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maximumReferenceLength);

        return new InputValidationLimits(maximumTextLength, maximumReferenceLength);
    }
}

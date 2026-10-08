namespace ObsAi.Application.Validation;

/// <summary>
/// Safe diagnostic of a rejected input. The rejection exposes only a classification reason and
/// never echoes the offending payload, text or operational identity (RNF-004, SEC-002, SEC-024).
/// </summary>
public sealed record InputRejection
{
    private InputRejection(InputRejectionReason reason)
    {
        Reason = reason;
    }

    /// <summary>Gets the classification of why the input was rejected.</summary>
    public InputRejectionReason Reason { get; }

    /// <summary>
    /// Creates a rejection for a defined reason. Unknown reasons are rejected with
    /// <see cref="ArgumentOutOfRangeException"/>.
    /// </summary>
    public static InputRejection Create(InputRejectionReason reason)
    {
        if (!Enum.IsDefined(reason))
        {
            throw new ArgumentOutOfRangeException(nameof(reason), "Unknown input rejection reason.");
        }

        return new InputRejection(reason);
    }

    public override string ToString() => $"[input-rejection:{Reason}]";
}

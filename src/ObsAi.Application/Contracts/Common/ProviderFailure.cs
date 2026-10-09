namespace ObsAi.Application.Contracts.Common;

/// <summary>
/// A vendor-neutral failure safe to cross the application boundary.
/// </summary>
public sealed record ProviderFailure(
    ProviderFailureCode Code,
    string SafeMessage,
    bool IsTransient = false,
    TimeSpan? RetryAfter = null)
{
    /// <summary>Gets the stable application category used to handle this failure.</summary>
    public FailureCategory Category => Code switch
    {
        ProviderFailureCode.InvalidRequest => FailureCategory.InvalidInput,
        ProviderFailureCode.TimedOut => FailureCategory.Timeout,
        ProviderFailureCode.Cancelled => FailureCategory.Cancelled,
        ProviderFailureCode.Unknown => FailureCategory.InternalFailure,
        ProviderFailureCode.RateLimited or ProviderFailureCode.Unavailable when IsTransient =>
            FailureCategory.TransientFailure,
        _ => FailureCategory.PermanentFailure,
    };

    /// <summary>
    /// Gets a value indicating whether an explicitly idempotent operation may retry this failure.
    /// Authentication, authorization, quota, invalid input and unknown failures are never retryable,
    /// even if an adapter incorrectly marks them as transient.
    /// </summary>
    public bool IsRetryable =>
        IsTransient
        && Code is ProviderFailureCode.TimedOut
            or ProviderFailureCode.RateLimited
            or ProviderFailureCode.Unavailable;
}

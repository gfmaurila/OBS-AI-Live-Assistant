namespace ObsAi.Application.Contracts.Common;

/// <summary>
/// Stable failure categories used by application policies without exposing vendor details.
/// </summary>
public enum FailureCategory
{
    Timeout,
    Cancelled,
    TransientFailure,
    PermanentFailure,
    InvalidInput,
    InternalFailure,
}

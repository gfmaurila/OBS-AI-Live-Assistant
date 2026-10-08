namespace ObsAi.Application.Lifecycle;

/// <summary>
/// Explicit reason why an operation lease request was refused.
/// </summary>
public enum LeaseRejectionReason
{
    /// <summary>The lease was granted; no rejection occurred.</summary>
    None = 0,

    /// <summary>The runtime has no active session.</summary>
    RuntimeStopped = 1,

    /// <summary>The runtime is paused and refuses new work.</summary>
    RuntimePaused = 2,

    /// <summary>An ordered shutdown is already cancelling work.</summary>
    RuntimeStopping = 3,

    /// <summary>The requested session is not the session currently owned by the runtime.</summary>
    SessionMismatch = 4,
}

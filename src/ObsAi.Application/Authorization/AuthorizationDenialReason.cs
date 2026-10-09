namespace ObsAi.Application.Authorization;

/// <summary>Stable, non-sensitive reasons returned by fail-closed authorization decisions.</summary>
public enum AuthorizationDenialReason
{
    None = 0,
    MissingContext = 1,
    InvalidContext = 2,
    SessionMismatch = 3,
    SessionInactive = 4,
    UnknownOrigin = 5,
    UntrustedOrigin = 6,
    MissingPolicy = 7,
    CapabilityNotAllowlisted = 8,
    MissingAuthority = 9,
    IdentityMissing = 10,
    PermissionDenied = 11,
    AuthorizationScopeMismatch = 12,
    InternalEvaluationFailure = 13,
}

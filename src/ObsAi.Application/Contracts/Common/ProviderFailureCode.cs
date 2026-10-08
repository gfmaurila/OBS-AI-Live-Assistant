namespace ObsAi.Application.Contracts.Common;

public enum ProviderFailureCode
{
    InvalidRequest,
    AuthenticationFailed,
    AuthorizationFailed,
    QuotaExceeded,
    RateLimited,
    TimedOut,
    Cancelled,
    Unavailable,
    InvalidResponse,
    UnsupportedCapability,
    Unknown,
}

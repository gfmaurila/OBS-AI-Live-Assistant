using ObsAi.Application.Contracts.Common;
using ObsAi.Application.Lifecycle;

namespace ObsAi.Application.Authorization;

/// <summary>
/// Carries only the operational context needed to evaluate one protected capability. It never
/// carries a caller-supplied role, permission or authorization flag.
/// </summary>
public sealed record AuthorizationRequest(
    RequestContext Context,
    OperationLease Lease,
    CapabilityId Capability,
    AuthorizationRequestOrigin Origin);

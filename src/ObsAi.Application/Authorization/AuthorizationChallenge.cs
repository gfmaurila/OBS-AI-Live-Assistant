using ObsAi.Application.Contracts.Common;

namespace ObsAi.Application.Authorization;

/// <summary>
/// Supplies a trusted authority boundary with the minimum non-sensitive scope it must verify.
/// </summary>
public sealed record AuthorizationChallenge(RequestContext Context, CapabilityId Capability);

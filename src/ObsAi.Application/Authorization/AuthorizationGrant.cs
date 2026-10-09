using ObsAi.Domain.Sessions;

namespace ObsAi.Application.Authorization;

/// <summary>Represents authority verified for exactly one session and one capability.</summary>
public sealed record AuthorizationGrant
{
    private AuthorizationGrant(SessionId sessionId, CapabilityId capability)
    {
        SessionId = sessionId;
        Capability = capability;
    }

    public SessionId SessionId { get; }

    public CapabilityId Capability { get; }

    /// <summary>
    /// Creates scoped evidence for return by a trusted <see cref="IAuthorizationAuthority"/>.
    /// Request content must never create or supply this evidence to the gate.
    /// </summary>
    public static AuthorizationGrant Create(SessionId sessionId, CapabilityId capability)
    {
        ArgumentNullException.ThrowIfNull(sessionId);
        ArgumentNullException.ThrowIfNull(capability);
        return new AuthorizationGrant(sessionId, capability);
    }
}

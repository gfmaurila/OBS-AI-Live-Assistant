namespace ObsAi.Application.Authorization;

/// <summary>
/// Explicit authorization outcome containing only correlation, session, operation, capability and
/// a stable reason. It never contains request content, identities, credentials or exception text.
/// </summary>
public sealed class AuthorizationDecision
{
    private AuthorizationDecision(
        bool isAllowed,
        AuthorizationDenialReason reason,
        Guid correlationId,
        Guid sessionId,
        Guid operationId,
        CapabilityId? capability)
    {
        IsAllowed = isAllowed;
        Reason = reason;
        CorrelationId = correlationId;
        SessionId = sessionId;
        OperationId = operationId;
        Capability = capability;
    }

    public bool IsAllowed { get; }

    public AuthorizationDenialReason Reason { get; }

    public Guid CorrelationId { get; }

    public Guid SessionId { get; }

    public Guid OperationId { get; }

    public CapabilityId? Capability { get; }

    internal static AuthorizationDecision Allowed(AuthorizationRequest request) =>
        new(
            true,
            AuthorizationDenialReason.None,
            request.Context.CorrelationId,
            request.Context.SessionId,
            request.Lease.OperationId,
            request.Capability);

    internal static AuthorizationDecision Denied(AuthorizationRequest? request, AuthorizationDenialReason reason) =>
        new(
            false,
            reason,
            request?.Context?.CorrelationId ?? Guid.Empty,
            request?.Context?.SessionId ?? Guid.Empty,
            request?.Lease?.OperationId ?? Guid.Empty,
            request?.Capability);

    public override string ToString() =>
        $"AuthorizationDecision {{ IsAllowed = {IsAllowed}, Reason = {Reason}, CorrelationId = {CorrelationId:D}, SessionId = {SessionId:D}, OperationId = {OperationId:D}, Capability = {Capability} }}";
}

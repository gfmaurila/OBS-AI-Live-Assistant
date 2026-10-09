using ObsAi.Domain.Sessions;

namespace ObsAi.Application.Authorization;

/// <summary>Evaluates protected operations deterministically and denies every incomplete path.</summary>
public sealed class AuthorizationGate
{
    private AuthorizationGate()
    {
    }

    /// <summary>
    /// Evaluates one request. Only an active same-session lease, a trusted control origin, an exact
    /// allowlist match and same-scope evidence from the trusted authority can produce an allow.
    /// </summary>
    public static AuthorizationDecision Evaluate(
        AuthorizationRequest? request,
        AuthorizationPolicy? policy,
        IAuthorizationAuthority? authority)
    {
        if (request is null || request.Context is null || request.Lease is null || request.Capability is null)
        {
            return AuthorizationDecision.Denied(request, AuthorizationDenialReason.MissingContext);
        }

        if (request.Context.CorrelationId == Guid.Empty || request.Context.SessionId == Guid.Empty)
        {
            return AuthorizationDecision.Denied(request, AuthorizationDenialReason.InvalidContext);
        }

        if (request.Lease.SessionId.Value != request.Context.SessionId)
        {
            return AuthorizationDecision.Denied(request, AuthorizationDenialReason.SessionMismatch);
        }

        if (request.Lease.IsReleased || request.Lease.IsCancellationRequested)
        {
            return AuthorizationDecision.Denied(request, AuthorizationDenialReason.SessionInactive);
        }

        if (!Enum.IsDefined(request.Origin) || request.Origin == AuthorizationRequestOrigin.Unknown)
        {
            return AuthorizationDecision.Denied(request, AuthorizationDenialReason.UnknownOrigin);
        }

        if (request.Origin != AuthorizationRequestOrigin.TrustedControl)
        {
            return AuthorizationDecision.Denied(request, AuthorizationDenialReason.UntrustedOrigin);
        }

        if (policy is null)
        {
            return AuthorizationDecision.Denied(request, AuthorizationDenialReason.MissingPolicy);
        }

        if (!policy.IsAllowlisted(request.Capability))
        {
            return AuthorizationDecision.Denied(request, AuthorizationDenialReason.CapabilityNotAllowlisted);
        }

        if (authority is null)
        {
            return AuthorizationDecision.Denied(request, AuthorizationDenialReason.MissingAuthority);
        }

        try
        {
            var challenge = new AuthorizationChallenge(request.Context, request.Capability);
            AuthorityVerification? verification = authority.Verify(challenge);
            if (verification is null)
            {
                return AuthorizationDecision.Denied(request, AuthorizationDenialReason.InternalEvaluationFailure);
            }

            return verification.Status switch
            {
                AuthorityVerificationStatus.IdentityMissing => AuthorizationDecision.Denied(request, AuthorizationDenialReason.IdentityMissing),
                AuthorityVerificationStatus.PermissionDenied => AuthorizationDecision.Denied(request, AuthorizationDenialReason.PermissionDenied),
                AuthorityVerificationStatus.Authorized => EvaluateGrant(request, verification.Grant),
                _ => AuthorizationDecision.Denied(request, AuthorizationDenialReason.InternalEvaluationFailure),
            };
        }
        catch (Exception)
        {
            return AuthorizationDecision.Denied(request, AuthorizationDenialReason.InternalEvaluationFailure);
        }
    }

    private static AuthorizationDecision EvaluateGrant(AuthorizationRequest request, AuthorizationGrant? grant)
    {
        if (request.Lease.IsReleased || request.Lease.IsCancellationRequested)
        {
            return AuthorizationDecision.Denied(request, AuthorizationDenialReason.SessionInactive);
        }

        if (grant is null
            || grant.SessionId != SessionId.Create(request.Context.SessionId)
            || grant.Capability != request.Capability)
        {
            return AuthorizationDecision.Denied(request, AuthorizationDenialReason.AuthorizationScopeMismatch);
        }

        return AuthorizationDecision.Allowed(request);
    }
}

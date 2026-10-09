using ObsAi.Application.Authorization;
using ObsAi.Application.Contracts.Common;
using ObsAi.Application.Lifecycle;
using ObsAi.Domain.Context;
using ObsAi.Domain.Profiles;
using ObsAi.Domain.Sessions;
using Xunit;

namespace ObsAi.Security.Tests;

public sealed class AuthorizationSecurityTests
{
    private static readonly SessionId Session =
        SessionId.Create(Guid.Parse("b2000000-0000-4000-8000-000000000001"));

    private static readonly CapabilityId Capability = CapabilityId.Create("obs.protected-operation");

    [Theory]
    [InlineData(AuthorizationRequestOrigin.ViewerMessage)]
    [InlineData(AuthorizationRequestOrigin.AiOutput)]
    public void UntrustedOrigins_CannotElevatePrivilegeEvenWhenAuthorityWouldAllow(AuthorizationRequestOrigin origin)
    {
        AuthorizationRequest request = CreateRequest(origin);
        var authority = new AllowingAuthority();

        AuthorizationDecision decision = AuthorizationGate.Evaluate(
            request,
            AuthorizationPolicy.Create([Capability]),
            authority);

        Assert.False(decision.IsAllowed);
        Assert.Equal(AuthorizationDenialReason.UntrustedOrigin, decision.Reason);
        Assert.Equal(0, authority.CallCount);
    }

    [Fact]
    public void EmptyAllowlist_DeniesEveryCapability()
    {
        AuthorizationDecision decision = AuthorizationGate.Evaluate(
            CreateRequest(),
            AuthorizationPolicy.Create([]),
            new AllowingAuthority());

        Assert.False(decision.IsAllowed);
        Assert.Equal(AuthorizationDenialReason.CapabilityNotAllowlisted, decision.Reason);
    }

    [Fact]
    public void CancelledSessionAuthority_CannotBeReused()
    {
        AuthorizationRequest request = CreateRequest();
        request.Lease.Cancel();

        AuthorizationDecision decision = AuthorizationGate.Evaluate(
            request,
            AuthorizationPolicy.Create([Capability]),
            new AllowingAuthority());

        Assert.False(decision.IsAllowed);
        Assert.Equal(AuthorizationDenialReason.SessionInactive, decision.Reason);
    }

    [Fact]
    public void CrossSessionGrant_IsDenied()
    {
        SessionId foreignSession = SessionId.Create(Guid.Parse("b2000000-0000-4000-8000-000000000099"));
        var authority = new DelegateAuthority(challenge => AuthorityVerification.Authorized(
            AuthorizationGrant.Create(foreignSession, challenge.Capability)));

        AuthorizationDecision decision = AuthorizationGate.Evaluate(
            CreateRequest(),
            AuthorizationPolicy.Create([Capability]),
            authority);

        Assert.False(decision.IsAllowed);
        Assert.Equal(AuthorizationDenialReason.AuthorizationScopeMismatch, decision.Reason);
    }

    [Fact]
    public void AuthorityException_IsConvertedToSafeFailClosedDecision()
    {
        const string SecretMarker = "API_KEY_DO_NOT_LEAK_123";
        var authority = new DelegateAuthority(_ => throw new InvalidOperationException(SecretMarker));

        AuthorizationDecision decision = AuthorizationGate.Evaluate(
            CreateRequest(),
            AuthorizationPolicy.Create([Capability]),
            authority);

        Assert.False(decision.IsAllowed);
        Assert.Equal(AuthorizationDenialReason.InternalEvaluationFailure, decision.Reason);
        Assert.DoesNotContain(SecretMarker, decision.ToString(), StringComparison.Ordinal);
    }

    [Fact]
    public void SessionRevokedDuringAuthorityVerification_IsRecheckedAndDenied()
    {
        AuthorizationRequest request = CreateRequest();
        var authority = new DelegateAuthority(challenge =>
        {
            request.Lease.Cancel();
            return AuthorityVerification.Authorized(
                AuthorizationGrant.Create(SessionId.Create(challenge.Context.SessionId), challenge.Capability));
        });

        AuthorizationDecision decision = AuthorizationGate.Evaluate(
            request,
            AuthorizationPolicy.Create([Capability]),
            authority);

        Assert.False(decision.IsAllowed);
        Assert.Equal(AuthorizationDenialReason.SessionInactive, decision.Reason);
    }

    [Fact]
    public void RepeatedEvaluation_IsDeterministicAndDoesNotMutatePolicy()
    {
        AuthorizationRequest request = CreateRequest();
        AuthorizationPolicy policy = AuthorizationPolicy.Create([Capability]);
        var authority = new AllowingAuthority();

        AuthorizationDecision first = AuthorizationGate.Evaluate(request, policy, authority);
        AuthorizationDecision second = AuthorizationGate.Evaluate(request, policy, authority);

        Assert.True(first.IsAllowed);
        Assert.True(second.IsAllowed);
        Assert.Equal(first.Reason, second.Reason);
        Assert.Equal(first.CorrelationId, second.CorrelationId);
        Assert.Equal(1, policy.AllowlistedCapabilityCount);
    }

    [Fact]
    public void RequestContract_ContainsNoCallerSuppliedRolePermissionOrIdentity()
    {
        string[] propertyNames = typeof(AuthorizationRequest).GetProperties().Select(property => property.Name).ToArray();

        Assert.DoesNotContain(propertyNames, name => name.Contains("Role", StringComparison.OrdinalIgnoreCase));
        Assert.DoesNotContain(propertyNames, name => name.Contains("Permission", StringComparison.OrdinalIgnoreCase));
        Assert.DoesNotContain(propertyNames, name => name.Contains("Identity", StringComparison.OrdinalIgnoreCase));
        Assert.DoesNotContain(propertyNames, name => name.Contains("Content", StringComparison.OrdinalIgnoreCase));
    }

    private static AuthorizationRequest CreateRequest(
        AuthorizationRequestOrigin origin = AuthorizationRequestOrigin.TrustedControl)
    {
        var orchestrator = new AssistantSessionOrchestrator();
        orchestrator.StartSession(
            Session,
            AssistantProfileId.Create(Guid.Parse("b2000000-0000-4000-8000-000000000002")),
            LiveContext.Create(
                Session,
                new Dictionary<LiveContextField, string> { [LiveContextField.LiveTitle] = "Synthetic live" },
                LiveContextLimits.Create(1, 32, 32)),
            new DateTimeOffset(2026, 10, 9, 12, 0, 0, TimeSpan.Zero));
        LeaseAcquisition acquisition = orchestrator.TryAcquireOperation(Session);
        Assert.True(acquisition.IsAccepted);

        return new AuthorizationRequest(
            new RequestContext(
                Guid.Parse("b2000000-0000-4000-8000-000000000003"),
                Session.Value,
                null),
            acquisition.Lease!,
            Capability,
            origin);
    }

    private sealed class AllowingAuthority : IAuthorizationAuthority
    {
        public int CallCount { get; private set; }

        public AuthorityVerification Verify(AuthorizationChallenge challenge)
        {
            CallCount++;
            return AuthorityVerification.Authorized(
                AuthorizationGrant.Create(SessionId.Create(challenge.Context.SessionId), challenge.Capability));
        }
    }

    private sealed class DelegateAuthority(Func<AuthorizationChallenge, AuthorityVerification> verify) : IAuthorizationAuthority
    {
        public AuthorityVerification Verify(AuthorizationChallenge challenge) => verify(challenge);
    }
}

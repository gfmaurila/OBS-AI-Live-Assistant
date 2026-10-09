using ObsAi.Application.Authorization;
using ObsAi.Application.Contracts.Common;
using ObsAi.Application.Lifecycle;
using ObsAi.Domain.Context;
using ObsAi.Domain.Profiles;
using ObsAi.Domain.Sessions;
using Xunit;

namespace ObsAi.Unit.Tests;

public sealed class AuthorizationGateTests
{
    private static readonly Guid SessionValue = Guid.Parse("a1000000-0000-4000-8000-000000000001");
    private static readonly Guid OtherSessionValue = Guid.Parse("a1000000-0000-4000-8000-000000000002");
    private static readonly Guid CorrelationValue = Guid.Parse("a1000000-0000-4000-8000-000000000003");
    private static readonly CapabilityId Capability = CapabilityId.Create("obs.scene.switch");

    [Fact]
    public void Evaluate_AllRequiredControlsMatch_AllowsExplicitly()
    {
        AuthorizationRequest request = CreateRequest();
        AuthorizationPolicy policy = AuthorizationPolicy.Create([Capability]);
        var authority = new StubAuthority(challenge => AuthorityVerification.Authorized(
            AuthorizationGrant.Create(SessionId.Create(challenge.Context.SessionId), challenge.Capability)));

        AuthorizationDecision decision = AuthorizationGate.Evaluate(request, policy, authority);

        Assert.True(decision.IsAllowed);
        Assert.Equal(AuthorizationDenialReason.None, decision.Reason);
        Assert.Equal(CorrelationValue, decision.CorrelationId);
        Assert.Equal(SessionValue, decision.SessionId);
        Assert.Equal(request.Lease.OperationId, decision.OperationId);
        Assert.Equal(Capability, decision.Capability);
    }

    [Theory]
    [InlineData(AuthorizationRequestOrigin.ViewerMessage)]
    [InlineData(AuthorizationRequestOrigin.AiOutput)]
    public void Evaluate_UntrustedContentOrigin_DeniesWithoutConsultingAuthority(AuthorizationRequestOrigin origin)
    {
        AuthorizationRequest request = CreateRequest(origin);
        var authority = new StubAuthority(_ => throw new InvalidOperationException("Must not be called."));

        AuthorizationDecision decision = AuthorizationGate.Evaluate(
            request,
            AuthorizationPolicy.Create([Capability]),
            authority);

        Assert.False(decision.IsAllowed);
        Assert.Equal(AuthorizationDenialReason.UntrustedOrigin, decision.Reason);
        Assert.Equal(0, authority.CallCount);
    }

    [Fact]
    public void Evaluate_NonAllowlistedCapability_DeniesWithoutConsultingAuthority()
    {
        AuthorizationRequest request = CreateRequest();
        var authority = new StubAuthority(_ => throw new InvalidOperationException("Must not be called."));

        AuthorizationDecision decision = AuthorizationGate.Evaluate(
            request,
            AuthorizationPolicy.Create([CapabilityId.Create("obs.audio.mute")]),
            authority);

        Assert.False(decision.IsAllowed);
        Assert.Equal(AuthorizationDenialReason.CapabilityNotAllowlisted, decision.Reason);
        Assert.Equal(0, authority.CallCount);
    }

    [Fact]
    public void Evaluate_MissingRequest_DeniesWithEmptyCorrelation()
    {
        AuthorizationDecision decision = AuthorizationGate.Evaluate(null, AuthorizationPolicy.Create([]), null);

        Assert.False(decision.IsAllowed);
        Assert.Equal(AuthorizationDenialReason.MissingContext, decision.Reason);
        Assert.Equal(Guid.Empty, decision.CorrelationId);
    }

    [Fact]
    public void Evaluate_NullContext_DeniesWithoutThrowing()
    {
        AuthorizationRequest request = CreateRequest() with { Context = null! };

        AuthorizationDecision decision = AuthorizationGate.Evaluate(request, AuthorizationPolicy.Create([Capability]), null);

        Assert.False(decision.IsAllowed);
        Assert.Equal(AuthorizationDenialReason.MissingContext, decision.Reason);
        Assert.Equal(Guid.Empty, decision.CorrelationId);
    }

    [Fact]
    public void Evaluate_EmptyCorrelation_DeniesInvalidContext()
    {
        AuthorizationRequest valid = CreateRequest();
        AuthorizationRequest request = valid with
        {
            Context = valid.Context with { CorrelationId = Guid.Empty },
        };

        AuthorizationDecision decision = AuthorizationGate.Evaluate(request, AuthorizationPolicy.Create([Capability]), null);

        Assert.False(decision.IsAllowed);
        Assert.Equal(AuthorizationDenialReason.InvalidContext, decision.Reason);
    }

    [Fact]
    public void Evaluate_ContextFromAnotherSession_DeniesSessionMismatch()
    {
        AuthorizationRequest valid = CreateRequest();
        AuthorizationRequest request = valid with
        {
            Context = valid.Context with { SessionId = OtherSessionValue },
        };

        AuthorizationDecision decision = AuthorizationGate.Evaluate(request, AuthorizationPolicy.Create([Capability]), null);

        Assert.False(decision.IsAllowed);
        Assert.Equal(AuthorizationDenialReason.SessionMismatch, decision.Reason);
    }

    [Fact]
    public void Evaluate_ReleasedLease_DeniesInactiveSession()
    {
        AuthorizationRequest request = CreateRequest();
        request.Lease.Dispose();

        AuthorizationDecision decision = AuthorizationGate.Evaluate(request, AuthorizationPolicy.Create([Capability]), null);

        Assert.False(decision.IsAllowed);
        Assert.Equal(AuthorizationDenialReason.SessionInactive, decision.Reason);
    }

    [Fact]
    public void Evaluate_UnknownOrigin_Denies()
    {
        AuthorizationRequest request = CreateRequest((AuthorizationRequestOrigin)int.MaxValue);

        AuthorizationDecision decision = AuthorizationGate.Evaluate(request, AuthorizationPolicy.Create([Capability]), null);

        Assert.False(decision.IsAllowed);
        Assert.Equal(AuthorizationDenialReason.UnknownOrigin, decision.Reason);
    }

    [Fact]
    public void Evaluate_MissingPolicy_DeniesBeforeAuthority()
    {
        AuthorizationRequest request = CreateRequest();
        var authority = new StubAuthority(_ => throw new InvalidOperationException("Must not be called."));

        AuthorizationDecision decision = AuthorizationGate.Evaluate(request, null, authority);

        Assert.False(decision.IsAllowed);
        Assert.Equal(AuthorizationDenialReason.MissingPolicy, decision.Reason);
        Assert.Equal(0, authority.CallCount);
    }

    [Fact]
    public void Evaluate_MissingAuthority_Denies()
    {
        AuthorizationDecision decision = AuthorizationGate.Evaluate(
            CreateRequest(),
            AuthorizationPolicy.Create([Capability]),
            null);

        Assert.False(decision.IsAllowed);
        Assert.Equal(AuthorizationDenialReason.MissingAuthority, decision.Reason);
    }

    [Theory]
    [InlineData(AuthorityVerificationStatus.IdentityMissing, AuthorizationDenialReason.IdentityMissing)]
    [InlineData(AuthorityVerificationStatus.PermissionDenied, AuthorizationDenialReason.PermissionDenied)]
    public void Evaluate_AuthorityDenial_PreservesSafeReason(
        AuthorityVerificationStatus status,
        AuthorizationDenialReason expectedReason)
    {
        var authority = new StubAuthority(_ => AuthorityVerification.Denied(status));

        AuthorizationDecision decision = AuthorizationGate.Evaluate(
            CreateRequest(),
            AuthorizationPolicy.Create([Capability]),
            authority);

        Assert.False(decision.IsAllowed);
        Assert.Equal(expectedReason, decision.Reason);
    }

    [Fact]
    public void Evaluate_AuthorityFailure_DeniesWithoutPropagatingException()
    {
        var authority = new StubAuthority(_ => throw new InvalidOperationException("sensitive internal failure"));

        AuthorizationDecision decision = AuthorizationGate.Evaluate(
            CreateRequest(),
            AuthorizationPolicy.Create([Capability]),
            authority);

        Assert.False(decision.IsAllowed);
        Assert.Equal(AuthorizationDenialReason.InternalEvaluationFailure, decision.Reason);
        Assert.DoesNotContain("sensitive", decision.ToString(), StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Evaluate_GrantForAnotherSession_DeniesScopeMismatch()
    {
        var authority = new StubAuthority(_ => AuthorityVerification.Authorized(
            AuthorizationGrant.Create(SessionId.Create(OtherSessionValue), Capability)));

        AuthorizationDecision decision = AuthorizationGate.Evaluate(
            CreateRequest(),
            AuthorizationPolicy.Create([Capability]),
            authority);

        Assert.False(decision.IsAllowed);
        Assert.Equal(AuthorizationDenialReason.AuthorizationScopeMismatch, decision.Reason);
    }

    [Fact]
    public void Evaluate_GrantForAnotherCapability_DeniesScopeMismatch()
    {
        var authority = new StubAuthority(_ => AuthorityVerification.Authorized(
            AuthorizationGrant.Create(SessionId.Create(SessionValue), CapabilityId.Create("obs.audio.mute"))));

        AuthorizationDecision decision = AuthorizationGate.Evaluate(
            CreateRequest(),
            AuthorizationPolicy.Create([Capability]),
            authority);

        Assert.False(decision.IsAllowed);
        Assert.Equal(AuthorizationDenialReason.AuthorizationScopeMismatch, decision.Reason);
    }

    [Fact]
    public void AuthorizationPolicy_CopiesSourceAndNeverWidensAfterCreation()
    {
        var source = new List<CapabilityId> { Capability };
        AuthorizationPolicy policy = AuthorizationPolicy.Create(source);

        source.Clear();
        source.Add(CapabilityId.Create("obs.audio.mute"));

        Assert.True(policy.IsAllowlisted(Capability));
        Assert.False(policy.IsAllowlisted(source[0]));
        Assert.Equal(1, policy.AllowlistedCapabilityCount);
    }

    [Theory]
    [InlineData("obs.scene.switch", "obs.scene.switch")]
    [InlineData("  OBS.SCENE.SWITCH  ", "obs.scene.switch")]
    public void CapabilityId_Create_CanonicalizesValidIdentifiers(string input, string expected)
    {
        Assert.Equal(expected, CapabilityId.Create(input).Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(".obs")]
    [InlineData("obs.")]
    [InlineData("obs_scene")]
    [InlineData("obs/scene")]
    public void CapabilityId_Create_RejectsInvalidIdentifiers(string input)
    {
        Assert.ThrowsAny<ArgumentException>(() => CapabilityId.Create(input));
    }

    [Fact]
    public void AuthorityVerification_DeniedRejectsAuthorizedAndUnknownStatuses()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => AuthorityVerification.Denied(AuthorityVerificationStatus.Authorized));
        Assert.Throws<ArgumentOutOfRangeException>(() => AuthorityVerification.Denied((AuthorityVerificationStatus)int.MaxValue));
    }

    private static AuthorizationRequest CreateRequest(
        AuthorizationRequestOrigin origin = AuthorizationRequestOrigin.TrustedControl)
    {
        SessionId sessionId = SessionId.Create(SessionValue);
        var orchestrator = new AssistantSessionOrchestrator();
        orchestrator.StartSession(
            sessionId,
            AssistantProfileId.Create(Guid.Parse("a1000000-0000-4000-8000-000000000004")),
            LiveContext.Create(
                sessionId,
                new Dictionary<LiveContextField, string> { [LiveContextField.LiveTitle] = "Live atual" },
                LiveContextLimits.Create(1, 32, 32)),
            new DateTimeOffset(2026, 10, 9, 12, 0, 0, TimeSpan.Zero));

        LeaseAcquisition acquisition = orchestrator.TryAcquireOperation(sessionId);
        Assert.True(acquisition.IsAccepted);
        return new AuthorizationRequest(
            new RequestContext(CorrelationValue, SessionValue, null),
            acquisition.Lease!,
            Capability,
            origin);
    }

    private sealed class StubAuthority(Func<AuthorizationChallenge, AuthorityVerification> verify) : IAuthorizationAuthority
    {
        public int CallCount { get; private set; }

        public AuthorityVerification Verify(AuthorizationChallenge challenge)
        {
            CallCount++;
            return verify(challenge);
        }
    }
}

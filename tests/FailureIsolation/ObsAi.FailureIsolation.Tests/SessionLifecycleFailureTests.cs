using ObsAi.Application.Lifecycle;
using ObsAi.Domain.Context;
using ObsAi.Domain.Profiles;
using ObsAi.Domain.Sessions;
using Xunit;

namespace ObsAi.FailureIsolation.Tests;

public sealed class SessionLifecycleFailureTests
{
    private static readonly DateTimeOffset StartTime = new(2026, 10, 8, 12, 0, 0, TimeSpan.Zero);
    private static readonly DateTimeOffset EndTime = new(2026, 10, 8, 14, 0, 0, TimeSpan.Zero);

    private static readonly SessionId Session =
        SessionId.Create(Guid.Parse("dddddddd-0000-4000-8000-000000000001"));

    private static readonly SessionId NextSession =
        SessionId.Create(Guid.Parse("dddddddd-0000-4000-8000-000000000002"));

    private static readonly AssistantProfileId ProfileId =
        AssistantProfileId.Create(Guid.Parse("dddddddd-0000-4000-8000-000000000003"));

    [Fact]
    public void Shutdown_SurvivesFaultyCancellationObserversAndStillRecovers()
    {
        var orchestrator = new AssistantSessionOrchestrator();
        var context = CreateContext(Session);
        orchestrator.StartSession(Session, ProfileId, context, StartTime);
        var first = AcceptLease(orchestrator, Session);
        var second = AcceptLease(orchestrator, Session);
        first.CancellationToken.Register(static () => throw new InvalidOperationException("observer failure"));
        second.CancellationToken.Register(static () => throw new InvalidOperationException("observer failure"));

        orchestrator.Shutdown(EndTime);

        Assert.Equal(AssistantRuntimeState.Stopped, orchestrator.State);
        Assert.Null(orchestrator.CurrentSessionId);
        Assert.Equal(0, orchestrator.InFlightOperationCount);
        Assert.True(first.IsReleased);
        Assert.True(second.IsReleased);
        Assert.True(context.IsCleared);

        orchestrator.StartSession(NextSession, ProfileId, CreateContext(NextSession), StartTime);
        Assert.Equal(AssistantRuntimeState.Running, orchestrator.State);
        Assert.True(orchestrator.TryAcquireOperation(NextSession).IsAccepted);
    }

    [Fact]
    public void FailedResultPublication_KeepsRuntimeAndLeaseUsable()
    {
        var orchestrator = new AssistantSessionOrchestrator();
        orchestrator.StartSession(Session, ProfileId, CreateContext(Session), StartTime);
        var lease = AcceptLease(orchestrator, Session);
        var published = new List<string>();

        Assert.Throws<InvalidOperationException>(() =>
        {
            orchestrator.TryCommitResult(lease, "output", _ => throw new InvalidOperationException("publication failure"));
        });

        Assert.Equal(AssistantRuntimeState.Running, orchestrator.State);
        Assert.False(lease.IsReleased);
        Assert.Equal(1, orchestrator.InFlightOperationCount);

        Assert.True(orchestrator.TryCommitResult(lease, "output", published.Add));
        Assert.Collection(published, item => Assert.Equal("output", item));
        Assert.Equal(0, orchestrator.InFlightOperationCount);
    }

    [Fact]
    public void RejectedSessionStart_LeavesNoAuthorityAndNoAmbiguousState()
    {
        var orchestrator = new AssistantSessionOrchestrator();
        var foreignContext = CreateContext(NextSession);

        Assert.Throws<ArgumentNullException>(() => orchestrator.StartSession(null!, ProfileId, CreateContext(Session), StartTime));
        Assert.Throws<ArgumentNullException>(() => orchestrator.StartSession(Session, null!, CreateContext(Session), StartTime));
        Assert.Throws<ArgumentException>(() => orchestrator.StartSession(Session, ProfileId, foreignContext, StartTime));

        Assert.Equal(AssistantRuntimeState.Stopped, orchestrator.State);
        Assert.Null(orchestrator.CurrentSessionId);
        Assert.Equal(0, orchestrator.InFlightOperationCount);
        Assert.Equal(LeaseRejectionReason.RuntimeStopped, orchestrator.TryAcquireOperation(Session).Reason);
    }

    private static OperationLease AcceptLease(AssistantSessionOrchestrator orchestrator, SessionId sessionId)
    {
        var acquisition = orchestrator.TryAcquireOperation(sessionId);
        Assert.True(acquisition.IsAccepted);
        return acquisition.Lease!;
    }

    private static LiveContext CreateContext(SessionId sessionId) =>
        LiveContext.Create(
            sessionId,
            new Dictionary<LiveContextField, string> { [LiveContextField.LiveTitle] = "Live atual" },
            LiveContextLimits.Create(1, 32, 32));
}

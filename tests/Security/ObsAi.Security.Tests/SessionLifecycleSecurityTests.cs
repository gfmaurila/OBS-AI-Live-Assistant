using ObsAi.Application.Lifecycle;
using ObsAi.Domain.Context;
using ObsAi.Domain.Profiles;
using ObsAi.Domain.Sessions;
using Xunit;

namespace ObsAi.Security.Tests;

public sealed class SessionLifecycleSecurityTests
{
    private static readonly DateTimeOffset StartTime = new(2026, 10, 8, 12, 0, 0, TimeSpan.Zero);
    private static readonly DateTimeOffset EndTime = new(2026, 10, 8, 14, 0, 0, TimeSpan.Zero);

    private static readonly SessionId FirstSession =
        SessionId.Create(Guid.Parse("eeeeeeee-0000-4000-8000-000000000001"));

    private static readonly SessionId SecondSession =
        SessionId.Create(Guid.Parse("eeeeeeee-0000-4000-8000-000000000002"));

    private static readonly AssistantProfileId ProfileId =
        AssistantProfileId.Create(Guid.Parse("eeeeeeee-0000-4000-8000-000000000003"));

    [Fact]
    public void CancelledOperation_RecoversCapacityAndNeverPublishesOutput()
    {
        var orchestrator = new AssistantSessionOrchestrator();
        orchestrator.StartSession(FirstSession, ProfileId, CreateContext(FirstSession), StartTime);
        var lease = AcceptLease(orchestrator, FirstSession);
        var published = new List<string>();

        lease.Cancel();
        var committed = orchestrator.TryCommitResult(lease, "cancelled output", published.Add);

        Assert.False(committed);
        Assert.Empty(published);
        Assert.True(lease.IsReleased);
        Assert.Equal(0, orchestrator.InFlightOperationCount);
        Assert.Equal(AssistantRuntimeState.Running, orchestrator.State);
    }

    [Fact]
    public void Shutdown_RevokesEveryLeaseBeforeAnyLateOutputIsAccepted()
    {
        var orchestrator = new AssistantSessionOrchestrator();
        orchestrator.StartSession(FirstSession, ProfileId, CreateContext(FirstSession), StartTime);
        var leases = new[]
        {
            AcceptLease(orchestrator, FirstSession),
            AcceptLease(orchestrator, FirstSession),
            AcceptLease(orchestrator, FirstSession),
        };
        var published = new List<string>();

        orchestrator.Shutdown(EndTime);

        Assert.All(leases, lease =>
        {
            Assert.True(lease.IsReleased);
            Assert.True(lease.IsCancellationRequested);
            Assert.False(orchestrator.TryCommitResult(lease, "late output", published.Add));
        });
        Assert.Empty(published);
        Assert.Equal(0, orchestrator.InFlightOperationCount);
        Assert.Equal(AssistantRuntimeState.Stopped, orchestrator.State);
    }

    [Fact]
    public void PausedRuntime_NeverWidensAdmissionForAnyCaller()
    {
        var orchestrator = new AssistantSessionOrchestrator();
        orchestrator.StartSession(FirstSession, ProfileId, CreateContext(FirstSession), StartTime);
        var lease = AcceptLease(orchestrator, FirstSession);

        orchestrator.Pause();
        lease.Dispose();

        Assert.Equal(LeaseRejectionReason.RuntimePaused, orchestrator.TryAcquireOperation(FirstSession).Reason);
        Assert.Equal(AssistantRuntimeState.Paused, orchestrator.State);
        Assert.Equal(0, orchestrator.InFlightOperationCount);
    }

    [Fact]
    public void SessionContextAndLateResults_NeverCrossIntoAnotherSession()
    {
        var orchestrator = new AssistantSessionOrchestrator();
        var oldContext = CreateContext(FirstSession);
        orchestrator.StartSession(FirstSession, ProfileId, oldContext, StartTime);
        var staleLease = AcceptLease(orchestrator, FirstSession);
        var published = new List<string>();

        orchestrator.Shutdown(EndTime);

        Assert.True(oldContext.IsCleared);
        Assert.Equal(LeaseRejectionReason.RuntimeStopped, orchestrator.TryAcquireOperation(FirstSession).Reason);
        Assert.Throws<ArgumentException>(() => orchestrator.StartSession(FirstSession, ProfileId, oldContext, StartTime));
        Assert.Equal(AssistantRuntimeState.Stopped, orchestrator.State);

        orchestrator.StartSession(SecondSession, ProfileId, CreateContext(SecondSession), StartTime);

        Assert.False(orchestrator.TryCommitResult(staleLease, "stale output", published.Add));
        Assert.Empty(published);
        Assert.Equal(LeaseRejectionReason.SessionMismatch, orchestrator.TryAcquireOperation(FirstSession).Reason);
        Assert.True(orchestrator.TryAcquireOperation(SecondSession).IsAccepted);
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

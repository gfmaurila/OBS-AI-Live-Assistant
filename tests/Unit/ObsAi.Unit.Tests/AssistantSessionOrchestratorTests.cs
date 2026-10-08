using ObsAi.Application.Lifecycle;
using ObsAi.Domain.Context;
using ObsAi.Domain.Profiles;
using ObsAi.Domain.Sessions;
using Xunit;

namespace ObsAi.Unit.Tests;

public sealed class AssistantSessionOrchestratorTests
{
    private static readonly DateTimeOffset StartTime = new(2026, 10, 8, 12, 0, 0, TimeSpan.Zero);
    private static readonly DateTimeOffset EndTime = new(2026, 10, 8, 14, 0, 0, TimeSpan.Zero);

    private static readonly SessionId FirstSession =
        SessionId.Create(Guid.Parse("aaaaaaaa-0000-4000-8000-000000000001"));

    private static readonly SessionId SecondSession =
        SessionId.Create(Guid.Parse("aaaaaaaa-0000-4000-8000-000000000002"));

    private static readonly AssistantProfileId ProfileId =
        AssistantProfileId.Create(Guid.Parse("bbbbbbbb-0000-4000-8000-000000000001"));

    [Fact]
    public void StartSession_RunsRuntimeAndExposesSessionIdentity()
    {
        var orchestrator = new AssistantSessionOrchestrator();
        var context = CreateContext(FirstSession);

        orchestrator.StartSession(FirstSession, ProfileId, context, StartTime);

        Assert.Equal(AssistantRuntimeState.Running, orchestrator.State);
        Assert.Equal(FirstSession, orchestrator.CurrentSessionId);
        Assert.Equal(0, orchestrator.InFlightOperationCount);
        Assert.False(context.IsCleared);
    }

    [Fact]
    public void StartSession_WhileAnotherSessionIsActive_IsRejectedWithoutSideEffects()
    {
        var orchestrator = new AssistantSessionOrchestrator();
        orchestrator.StartSession(FirstSession, ProfileId, CreateContext(FirstSession), StartTime);

        Assert.Throws<InvalidOperationException>(() => orchestrator.StartSession(
            SecondSession,
            ProfileId,
            CreateContext(SecondSession),
            StartTime));

        Assert.Equal(AssistantRuntimeState.Running, orchestrator.State);
        Assert.Equal(FirstSession, orchestrator.CurrentSessionId);
    }

    [Fact]
    public void StartSession_WithForeignContext_FailsWithoutCreatingAuthority()
    {
        var orchestrator = new AssistantSessionOrchestrator();

        Assert.Throws<ArgumentException>(() => orchestrator.StartSession(
            FirstSession,
            ProfileId,
            CreateContext(SecondSession),
            StartTime));

        Assert.Equal(AssistantRuntimeState.Stopped, orchestrator.State);
        Assert.Null(orchestrator.CurrentSessionId);
        Assert.Equal(LeaseRejectionReason.RuntimeStopped, orchestrator.TryAcquireOperation(FirstSession).Reason);
    }

    [Fact]
    public void Pause_BlocksNewOperationsAndLetsInFlightWorkFinish()
    {
        var orchestrator = new AssistantSessionOrchestrator();
        orchestrator.StartSession(FirstSession, ProfileId, CreateContext(FirstSession), StartTime);
        var lease = AcceptLease(orchestrator, FirstSession);
        var published = new List<string>();

        orchestrator.Pause();

        var rejected = orchestrator.TryAcquireOperation(FirstSession);
        Assert.False(rejected.IsAccepted);
        Assert.Equal(LeaseRejectionReason.RuntimePaused, rejected.Reason);
        Assert.Equal(AssistantRuntimeState.Paused, orchestrator.State);
        Assert.False(lease.IsReleased);
        Assert.False(lease.IsCancellationRequested);
        Assert.True(orchestrator.TryCommitResult(lease, "in-flight output", published.Add));
        Assert.Collection(published, item => Assert.Equal("in-flight output", item));
        Assert.Equal(0, orchestrator.InFlightOperationCount);
    }

    [Fact]
    public void Pause_OnNonRunningRuntime_IsRejected()
    {
        var orchestrator = new AssistantSessionOrchestrator();

        Assert.Throws<InvalidOperationException>(() => orchestrator.Pause());
        Assert.Equal(AssistantRuntimeState.Stopped, orchestrator.State);

        orchestrator.StartSession(FirstSession, ProfileId, CreateContext(FirstSession), StartTime);
        orchestrator.Pause();

        Assert.Throws<InvalidOperationException>(() => orchestrator.Pause());
        Assert.Equal(AssistantRuntimeState.Paused, orchestrator.State);
    }

    [Fact]
    public void Resume_RestoresAdmissionAfterPause()
    {
        var orchestrator = new AssistantSessionOrchestrator();
        orchestrator.StartSession(FirstSession, ProfileId, CreateContext(FirstSession), StartTime);
        orchestrator.Pause();

        orchestrator.Resume();

        Assert.Equal(AssistantRuntimeState.Running, orchestrator.State);
        Assert.True(orchestrator.TryAcquireOperation(FirstSession).IsAccepted);
    }

    [Fact]
    public void Resume_OnNonPausedRuntime_IsRejected()
    {
        var orchestrator = new AssistantSessionOrchestrator();

        Assert.Throws<InvalidOperationException>(() => orchestrator.Resume());

        orchestrator.StartSession(FirstSession, ProfileId, CreateContext(FirstSession), StartTime);

        Assert.Throws<InvalidOperationException>(() => orchestrator.Resume());
        Assert.Equal(AssistantRuntimeState.Running, orchestrator.State);
    }

    [Fact]
    public void TryAcquireOperation_OnStoppedRuntime_ReportsExplicitReason()
    {
        var orchestrator = new AssistantSessionOrchestrator();

        var acquisition = orchestrator.TryAcquireOperation(FirstSession);

        Assert.False(acquisition.IsAccepted);
        Assert.Equal(LeaseRejectionReason.RuntimeStopped, acquisition.Reason);
        Assert.Null(acquisition.Lease);
        Assert.Equal(0, orchestrator.InFlightOperationCount);
    }

    [Fact]
    public void TryAcquireOperation_ForForeignSession_ReportsSessionMismatch()
    {
        var orchestrator = new AssistantSessionOrchestrator();
        orchestrator.StartSession(FirstSession, ProfileId, CreateContext(FirstSession), StartTime);

        var acquisition = orchestrator.TryAcquireOperation(SecondSession);

        Assert.False(acquisition.IsAccepted);
        Assert.Equal(LeaseRejectionReason.SessionMismatch, acquisition.Reason);
        Assert.Equal(0, orchestrator.InFlightOperationCount);
    }

    [Fact]
    public void TryAcquireOperation_WhileShutdownCancelsWork_ReportsRuntimeStopping()
    {
        var orchestrator = new AssistantSessionOrchestrator();
        orchestrator.StartSession(FirstSession, ProfileId, CreateContext(FirstSession), StartTime);
        var lease = AcceptLease(orchestrator, FirstSession);
        LeaseRejectionReason? reasonSeenByCancelledWork = null;

        lease.CancellationToken.Register(() =>
        {
            reasonSeenByCancelledWork = orchestrator.TryAcquireOperation(FirstSession).Reason;
        });

        orchestrator.Shutdown(EndTime);

        Assert.Equal(LeaseRejectionReason.RuntimeStopping, reasonSeenByCancelledWork);
    }

    [Fact]
    public void Shutdown_CancelsWorkReleasesLeasesAndEndsSession()
    {
        var orchestrator = new AssistantSessionOrchestrator();
        var context = CreateContext(FirstSession);
        orchestrator.StartSession(FirstSession, ProfileId, context, StartTime);
        var lease = AcceptLease(orchestrator, FirstSession);

        orchestrator.Shutdown(EndTime);

        Assert.Equal(AssistantRuntimeState.Stopped, orchestrator.State);
        Assert.Null(orchestrator.CurrentSessionId);
        Assert.Equal(0, orchestrator.InFlightOperationCount);
        Assert.True(lease.IsReleased);
        Assert.True(lease.IsCancellationRequested);
        Assert.True(context.IsCleared);
        Assert.Throws<InvalidOperationException>(() => context.Entries);
    }

    [Fact]
    public void Shutdown_OnStoppedRuntime_IsIdempotent()
    {
        var orchestrator = new AssistantSessionOrchestrator();
        orchestrator.StartSession(FirstSession, ProfileId, CreateContext(FirstSession), StartTime);
        orchestrator.Shutdown(EndTime);

        orchestrator.Shutdown(EndTime);
        orchestrator.Shutdown(EndTime);

        Assert.Equal(AssistantRuntimeState.Stopped, orchestrator.State);
        Assert.Null(orchestrator.CurrentSessionId);
    }

    [Fact]
    public void Shutdown_WithNonUtcTimestamp_FailsBeforeChangingState()
    {
        var orchestrator = new AssistantSessionOrchestrator();
        orchestrator.StartSession(FirstSession, ProfileId, CreateContext(FirstSession), StartTime);
        var lease = AcceptLease(orchestrator, FirstSession);

        Assert.Throws<ArgumentException>(() => orchestrator.Shutdown(EndTime.ToOffset(TimeSpan.FromHours(-3))));

        Assert.Equal(AssistantRuntimeState.Running, orchestrator.State);
        Assert.Equal(FirstSession, orchestrator.CurrentSessionId);
        Assert.False(lease.IsReleased);
    }

    [Fact]
    public void Shutdown_BeforeSessionStart_FailsBeforeChangingState()
    {
        var orchestrator = new AssistantSessionOrchestrator();
        orchestrator.StartSession(FirstSession, ProfileId, CreateContext(FirstSession), StartTime);

        Assert.Throws<ArgumentOutOfRangeException>(() => orchestrator.Shutdown(StartTime.AddTicks(-1)));

        Assert.Equal(AssistantRuntimeState.Running, orchestrator.State);
        Assert.Equal(FirstSession, orchestrator.CurrentSessionId);
    }

    [Fact]
    public void TryCommitResult_ReleasesLeaseAndRefusesASecondCommit()
    {
        var orchestrator = new AssistantSessionOrchestrator();
        orchestrator.StartSession(FirstSession, ProfileId, CreateContext(FirstSession), StartTime);
        var lease = AcceptLease(orchestrator, FirstSession);
        var published = new List<string>();

        Assert.True(orchestrator.TryCommitResult(lease, "first", published.Add));
        Assert.False(orchestrator.TryCommitResult(lease, "second", published.Add));

        Assert.Collection(published, item => Assert.Equal("first", item));
        Assert.True(lease.IsReleased);
        Assert.Equal(0, orchestrator.InFlightOperationCount);
    }

    [Fact]
    public void TryCommitResult_WithCancelledLease_IsDiscarded()
    {
        var orchestrator = new AssistantSessionOrchestrator();
        orchestrator.StartSession(FirstSession, ProfileId, CreateContext(FirstSession), StartTime);
        var lease = AcceptLease(orchestrator, FirstSession);
        var published = new List<string>();

        lease.Cancel();
        var committed = orchestrator.TryCommitResult(lease, "cancelled output", published.Add);

        Assert.False(committed);
        Assert.Empty(published);
        Assert.Equal(AssistantRuntimeState.Running, orchestrator.State);
        Assert.Equal(0, orchestrator.InFlightOperationCount);
    }

    [Fact]
    public void TryCommitResult_AfterShutdown_IsDiscarded()
    {
        var orchestrator = new AssistantSessionOrchestrator();
        orchestrator.StartSession(FirstSession, ProfileId, CreateContext(FirstSession), StartTime);
        var lease = AcceptLease(orchestrator, FirstSession);
        var published = new List<string>();

        orchestrator.Shutdown(EndTime);
        var committed = orchestrator.TryCommitResult(lease, "late output", published.Add);

        Assert.False(committed);
        Assert.Empty(published);
    }

    [Fact]
    public void TryCommitResult_FromAPreviousSession_IsDiscardedAfterRestart()
    {
        var orchestrator = new AssistantSessionOrchestrator();
        orchestrator.StartSession(FirstSession, ProfileId, CreateContext(FirstSession), StartTime);
        var staleLease = AcceptLease(orchestrator, FirstSession);

        orchestrator.Shutdown(EndTime);
        orchestrator.StartSession(SecondSession, ProfileId, CreateContext(SecondSession), StartTime);
        var published = new List<string>();
        var committed = orchestrator.TryCommitResult(staleLease, "stale output", published.Add);

        Assert.False(committed);
        Assert.Empty(published);
        Assert.Equal(LeaseRejectionReason.SessionMismatch, orchestrator.TryAcquireOperation(FirstSession).Reason);
        Assert.True(orchestrator.TryAcquireOperation(SecondSession).IsAccepted);
    }

    [Fact]
    public void LeaseDispose_ReleasesCapacityAndIsIdempotent()
    {
        var orchestrator = new AssistantSessionOrchestrator();
        orchestrator.StartSession(FirstSession, ProfileId, CreateContext(FirstSession), StartTime);
        var lease = AcceptLease(orchestrator, FirstSession);

        lease.Dispose();
        lease.Dispose();

        Assert.True(lease.IsReleased);
        Assert.Equal(0, orchestrator.InFlightOperationCount);
        Assert.False(orchestrator.TryCommitResult(lease, "abandoned output", _ => { }));
    }

    [Fact]
    public void LeaseAcquisition_WithoutExplicitReasonOrLease_IsRejected()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => LeaseAcquisition.Rejected(LeaseRejectionReason.None));
        Assert.Throws<ArgumentNullException>(() => LeaseAcquisition.Accepted(null!));
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

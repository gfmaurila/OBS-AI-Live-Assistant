using ObsAi.Application.Lifecycle;
using ObsAi.Domain.Context;
using ObsAi.Domain.Profiles;
using ObsAi.Domain.Sessions;
using Xunit;

namespace ObsAi.Integration.Tests;

public sealed class SessionLifecycleFlowTests
{
    private static readonly TimeSpan BoundaryTimeout = TimeSpan.FromSeconds(10);

    private static readonly DateTimeOffset StartTime = new(2026, 10, 8, 12, 0, 0, TimeSpan.Zero);
    private static readonly DateTimeOffset EndTime = new(2026, 10, 8, 14, 0, 0, TimeSpan.Zero);

    private static readonly SessionId Session =
        SessionId.Create(Guid.Parse("cccccccc-0000-4000-8000-000000000001"));

    private static readonly AssistantProfileId ProfileId =
        AssistantProfileId.Create(Guid.Parse("cccccccc-0000-4000-8000-000000000002"));

    [Fact]
    public async Task AdmittedOperation_CrossesTheBoundaryAndPublishesExactlyOnce()
    {
        var orchestrator = new AssistantSessionOrchestrator();
        orchestrator.StartSession(Session, ProfileId, CreateContext(Session), StartTime);
        var lease = AcceptLease(orchestrator);
        var published = new List<string>();

        var output = await Task.Run(
            () =>
            {
                lease.CancellationToken.ThrowIfCancellationRequested();
                return "provider output";
            },
            lease.CancellationToken).WaitAsync(BoundaryTimeout);

        var committed = orchestrator.TryCommitResult(lease, output, published.Add);

        Assert.True(committed);
        Assert.Collection(published, item => Assert.Equal("provider output", item));
        Assert.True(lease.IsReleased);
        Assert.Equal(0, orchestrator.InFlightOperationCount);
        Assert.Equal(AssistantRuntimeState.Running, orchestrator.State);
    }

    [Fact]
    public async Task Shutdown_DuringInFlightWork_CancelsItAndDiscardsItsLateOutput()
    {
        var orchestrator = new AssistantSessionOrchestrator();
        orchestrator.StartSession(Session, ProfileId, CreateContext(Session), StartTime);
        var lease = AcceptLease(orchestrator);
        var published = new List<string>();
        var workStarted = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var observedCancellation = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

        Task inFlight = Task.Run(async () =>
        {
            workStarted.SetResult();
            try
            {
                await Task.Delay(Timeout.InfiniteTimeSpan, lease.CancellationToken);
                observedCancellation.SetResult(false);
            }
            catch (OperationCanceledException)
            {
                observedCancellation.SetResult(true);
            }
        });

        await workStarted.Task.WaitAsync(BoundaryTimeout);
        orchestrator.Shutdown(EndTime);

        Assert.True(await observedCancellation.Task.WaitAsync(BoundaryTimeout));
        await inFlight.WaitAsync(BoundaryTimeout);
        Assert.False(orchestrator.TryCommitResult(lease, "late output", published.Add));
        Assert.Empty(published);
        Assert.Equal(AssistantRuntimeState.Stopped, orchestrator.State);
        Assert.Equal(0, orchestrator.InFlightOperationCount);
        Assert.Null(orchestrator.CurrentSessionId);
    }

    [Fact]
    public async Task PausedRuntime_DispatchesNoNewWorkWhileInFlightWorkFinishes()
    {
        var orchestrator = new AssistantSessionOrchestrator();
        orchestrator.StartSession(Session, ProfileId, CreateContext(Session), StartTime);
        var lease = AcceptLease(orchestrator);
        var published = new List<string>();
        var gate = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

        Task<string> inFlight = Task.Run(async () =>
        {
            await gate.Task;
            return "finished in flight";
        }, lease.CancellationToken);

        orchestrator.Pause();
        Assert.Equal(LeaseRejectionReason.RuntimePaused, orchestrator.TryAcquireOperation(Session).Reason);

        gate.SetResult(true);
        var output = await inFlight.WaitAsync(BoundaryTimeout);

        Assert.True(orchestrator.TryCommitResult(lease, output, published.Add));
        Assert.Collection(published, item => Assert.Equal("finished in flight", item));

        orchestrator.Resume();
        Assert.True(orchestrator.TryAcquireOperation(Session).IsAccepted);
        Assert.Equal(AssistantRuntimeState.Running, orchestrator.State);
    }

    private static OperationLease AcceptLease(AssistantSessionOrchestrator orchestrator)
    {
        var acquisition = orchestrator.TryAcquireOperation(Session);
        Assert.True(acquisition.IsAccepted);
        return acquisition.Lease!;
    }

    private static LiveContext CreateContext(SessionId sessionId) =>
        LiveContext.Create(
            sessionId,
            new Dictionary<LiveContextField, string> { [LiveContextField.LiveTitle] = "Live atual" },
            LiveContextLimits.Create(1, 32, 32));
}

using ObsAi.Application.Queues;
using Xunit;

namespace ObsAi.FailureIsolation.Tests;

public sealed class QueueFailureTests
{
    private static WorkQueueSettings RequestSettings(int capacity, int concurrency = 1) =>
        WorkQueueSettings.Create(QueueKind.Request, capacity, concurrency, SaturationPolicy.Reject);

    [Fact]
    public async Task HandlerException_DoesNotCrashTheRunner_AndRemainingWorkStillCompletes()
    {
        var failures = new List<string>();
        using var runner = new WorkQueueRunner<string>(
            RequestSettings(4),
            (item, _) => item == "boom"
                ? throw new InvalidOperationException("handler failure")
                : Task.CompletedTask,
            (_, item) => failures.Add(item));

        foreach (var item in new[] { "ok-1", "boom", "ok-2", "ok-3" })
        {
            Assert.True(runner.Buffer.TryEnqueue(item).IsAccepted);
        }

        runner.Start();

        using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(10));
        await runner.ShutdownAsync().WaitAsync(timeout.Token);

        Assert.Equal(3, runner.TotalProcessed);
        Assert.Equal(1, runner.TotalItemFailures);
        Assert.Equal(0, runner.InFlightCount);
        Assert.Equal(0, runner.PendingCount);
        Assert.True(runner.Buffer.IsClosed);
        Assert.Collection(failures, item => Assert.Equal("boom", item));
    }

    [Fact]
    public async Task CancelledShutdown_WithBlockedWorker_LeavesNoOrphanWorkOrFailure()
    {
        var gate = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        var started = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        using var runner = new WorkQueueRunner<string>(
            RequestSettings(1),
            async (_, token) =>
            {
                started.TrySetResult(true);
                await gate.Task.WaitAsync(token);
            });

        Assert.True(runner.Buffer.TryEnqueue("stuck").IsAccepted);
        runner.Start();

        using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(10));
        await started.Task.WaitAsync(timeout.Token);

        using var cancel = new CancellationTokenSource();
        var shutdown = runner.ShutdownAsync(cancel.Token);
        cancel.Cancel();
        await shutdown.WaitAsync(timeout.Token);

        Assert.Equal(0, runner.InFlightCount);
        Assert.Equal(0, runner.PendingCount);
        Assert.Equal(0, runner.TotalProcessed);
        Assert.Equal(0, runner.TotalItemFailures);
        Assert.True(runner.IsShutdownRequested);
    }

    [Fact]
    public async Task LateEnqueue_AfterShutdown_IsRefusedWithoutStateMutation()
    {
        using var runner = new WorkQueueRunner<string>(RequestSettings(2), (_, _) => Task.CompletedTask);
        runner.Buffer.TryEnqueue("early");
        runner.Start();

        using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(10));
        var firstOutcome = runner.Buffer.TryEnqueue("while-running");
        await runner.ShutdownAsync().WaitAsync(timeout.Token);
        var lateOutcome = runner.Buffer.TryEnqueue("late");

        Assert.True(firstOutcome.IsAccepted);
        Assert.Equal(QueueEnqueueStatus.Closed, lateOutcome.Status);
        Assert.True(runner.Buffer.IsClosed);
        Assert.Equal(2, runner.Buffer.TotalAccepted);
        Assert.Equal(1, runner.Buffer.TotalRejected);
    }

    [Fact]
    public async Task DoubleStart_IsRejected_AndWorkIsConsumedExactlyOnce()
    {
        var processed = new List<string>();
        using var runner = new WorkQueueRunner<string>(
            RequestSettings(3),
            (item, _) =>
            {
                lock (processed)
                {
                    processed.Add(item);
                }

                return Task.CompletedTask;
            });

        runner.Buffer.TryEnqueue("a");
        runner.Buffer.TryEnqueue("b");
        runner.Start();

        Assert.Throws<InvalidOperationException>(() => runner.Start());

        using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(10));
        await runner.ShutdownAsync().WaitAsync(timeout.Token);

        Assert.Equal(2, runner.TotalProcessed);
        Assert.Collection(processed, item => Assert.Equal("a", item), item => Assert.Equal("b", item));
    }
}

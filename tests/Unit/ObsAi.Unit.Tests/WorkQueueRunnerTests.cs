using ObsAi.Application.Queues;
using Xunit;

namespace ObsAi.Unit.Tests;

public sealed class WorkQueueRunnerTests
{
    private static WorkQueueSettings RequestSettings(int capacity, int concurrency = 1) =>
        WorkQueueSettings.Create(QueueKind.Request, capacity, concurrency, SaturationPolicy.Reject);

    [Fact]
    public void Start_MoreThanOnce_IsRejectedWithoutSideEffects()
    {
        using var runner = new WorkQueueRunner<string>(RequestSettings(1), (_, _) => Task.CompletedTask);

        runner.Start();

        Assert.Throws<InvalidOperationException>(() => runner.Start());
    }

    [Fact]
    public async Task Shutdown_WithoutStart_CompletesImmediately()
    {
        using var runner = new WorkQueueRunner<string>(RequestSettings(1), (_, _) => Task.CompletedTask);

        await runner.ShutdownAsync();

        Assert.True(runner.IsShutdownRequested);
        Assert.True(runner.Buffer.IsClosed);
        Assert.Equal(0, runner.PendingCount);
        Assert.Equal(0, runner.TotalProcessed);
    }

    [Fact]
    public async Task Shutdown_WithEmptyStartedRunner_LeavesNothingInFlight()
    {
        using var runner = new WorkQueueRunner<string>(RequestSettings(3), (_, _) => Task.CompletedTask);
        runner.Start();

        await runner.ShutdownAsync();

        Assert.Equal(0, runner.PendingCount);
        Assert.Equal(0, runner.InFlightCount);
        Assert.Equal(0, runner.TotalProcessed);
    }

    [Fact]
    public async Task Processing_NeverExceedsConfiguredConcurrency_AndCompletesAllItems()
    {
        const int capacity = 6;
        const int concurrency = 3;
        var holdGate = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        var allWorkersActive = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        var observedPeak = 0;
        var gate = new object();

        async Task Handler(string _item, CancellationToken token)
        {
            int current = Interlocked.Increment(ref activeCount);
            lock (gate)
            {
                if (current > observedPeak)
                {
                    observedPeak = current;
                }
            }

            try
            {
                if (current == concurrency)
                {
                    allWorkersActive.TrySetResult(true);
                }

                await holdGate.Task.WaitAsync(token);
            }
            finally
            {
                Interlocked.Decrement(ref activeCount);
            }
        }

        using (var runner = new WorkQueueRunner<string>(RequestSettings(capacity, concurrency), Handler))
        {
            for (int i = 0; i < capacity; i++)
            {
                Assert.True(runner.Buffer.TryEnqueue($"item-{i}").IsAccepted);
            }

            runner.Start();

            using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            await allWorkersActive.Task.WaitAsync(timeout.Token);
            holdGate.TrySetResult(true);

            await runner.ShutdownAsync().WaitAsync(timeout.Token);

            Assert.Equal(concurrency, observedPeak);
            Assert.Equal(capacity, runner.TotalProcessed);
            Assert.Equal(0, runner.PendingCount);
            Assert.Equal(0, runner.InFlightCount);
        }
    }

    [Fact]
    public async Task FailedItem_IsReportedButRunnerKeepsProcessingTheRemainingWork()
    {
        var failures = new List<string>();
        using var runner = new WorkQueueRunner<string>(
            RequestSettings(5),
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
        Assert.True(runner.IsShutdownRequested);
        Assert.Collection(failures, item => Assert.Equal("boom", item));
    }

    [Fact]
    public async Task Shutdown_DrainsPendingItemsBeforeCompleting()
    {
        var gate = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        var started = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        using var runner = new WorkQueueRunner<string>(
            RequestSettings(3),
            async (_, token) =>
            {
                started.TrySetResult(true);
                await gate.Task.WaitAsync(token);
            });

        for (int i = 0; i < 3; i++)
        {
            Assert.True(runner.Buffer.TryEnqueue($"item-{i}").IsAccepted);
        }

        runner.Start();
        using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(10));
        await started.Task.WaitAsync(timeout.Token);

        var shutdown = runner.ShutdownAsync();
        Assert.False(shutdown.IsCompleted);

        gate.TrySetResult(true);
        await shutdown.WaitAsync(timeout.Token);

        Assert.Equal(3, runner.TotalProcessed);
        Assert.Equal(0, runner.PendingCount);
        Assert.Equal(0, runner.InFlightCount);
        Assert.True(runner.IsShutdownRequested);
    }

    [Fact]
    public async Task Shutdown_WithCancelledToken_ReturnsPromptlyAndLeavesNoOrphanWork()
    {
        var started = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        var gate = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        using var runner = new WorkQueueRunner<string>(
            RequestSettings(2, 2),
            async (_, token) =>
            {
                if (Interlocked.Increment(ref activeCount) == 2)
                {
                    started.TrySetResult(true);
                }

                await gate.Task.WaitAsync(token);
            });

        runner.Buffer.TryEnqueue("a");
        runner.Buffer.TryEnqueue("b");
        runner.Start();

        using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(10));
        await started.Task.WaitAsync(timeout.Token);

        using var cancel = new CancellationTokenSource();
        var shutdown = runner.ShutdownAsync(cancel.Token);
        Assert.False(shutdown.IsCompleted);

        cancel.Cancel();
        await shutdown.WaitAsync(timeout.Token);

        Assert.Equal(0, runner.InFlightCount);
        Assert.Equal(0, runner.PendingCount);
        Assert.Equal(0, runner.TotalProcessed);
        Assert.Equal(0, runner.TotalItemFailures);
    }

    [Fact]
    public async Task Shutdown_CalledTwice_ReturnsTheSameTask()
    {
        using var runner = new WorkQueueRunner<string>(RequestSettings(1), (_, _) => Task.CompletedTask);
        runner.Start();

        var first = runner.ShutdownAsync();
        var second = runner.ShutdownAsync();

        Assert.Same(first, second);

        using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(10));
        await first.WaitAsync(timeout.Token);
    }

    private int activeCount;
}

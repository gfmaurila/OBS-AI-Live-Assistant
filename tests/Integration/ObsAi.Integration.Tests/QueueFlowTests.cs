using ObsAi.Application.Queues;
using Xunit;

namespace ObsAi.Integration.Tests;

public sealed class QueueFlowTests
{
    private static readonly int[] RecentWindow = { 47, 48, 49 };

    private static WorkQueueSettings Settings(QueueKind kind, int capacity, int concurrency = 1) =>
        WorkQueueSettings.Create(kind, capacity, concurrency, SaturationPolicy.Reject);

    [Fact]
    public async Task SlowStageQueue_DoesNotBlockIndependentStageQueues()
    {
        var requestGate = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        var requestStarted = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        using var requestRunner = new WorkQueueRunner<string>(
            Settings(QueueKind.Request, 5, 1),
            async (_, token) =>
            {
                requestStarted.TrySetResult(true);
                await requestGate.Task.WaitAsync(token);
            });
        using var responseRunner = new WorkQueueRunner<string>(
            Settings(QueueKind.Response, 20, 2),
            (_, _) => Task.CompletedTask);

        for (int i = 0; i < 3; i++)
        {
            Assert.True(requestRunner.Buffer.TryEnqueue($"request-{i}").IsAccepted);
        }

        requestRunner.Start();
        responseRunner.Start();

        using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(10));
        await requestStarted.Task.WaitAsync(timeout.Token);
        Assert.Equal(1, requestRunner.InFlightCount);

        for (int i = 0; i < 10; i++)
        {
            Assert.True(responseRunner.Buffer.TryEnqueue($"response-{i}").IsAccepted);
        }

        await AwaitUntilAsync(() => responseRunner.TotalProcessed == 10, timeout.Token);

        Assert.Equal(1, requestRunner.InFlightCount);
        Assert.Equal(0, responseRunner.PendingCount);
        Assert.Equal(10, responseRunner.TotalProcessed);

        requestGate.TrySetResult(true);
        await requestRunner.ShutdownAsync().WaitAsync(timeout.Token);
        await responseRunner.ShutdownAsync().WaitAsync(timeout.Token);

        Assert.Equal(3, requestRunner.TotalProcessed);
        Assert.Equal(0, requestRunner.InFlightCount);
        Assert.Equal(0, responseRunner.InFlightCount);
    }

    [Fact]
    public async Task Overload_UnderRejectPolicy_HoldsMemoryStrictlyBounded()
    {
        const int capacity = 5;
        const int flood = 1000;
        var holdGate = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        using var runner = new WorkQueueRunner<int>(
            Settings(QueueKind.Request, capacity),
            async (_, token) => await holdGate.Task.WaitAsync(token));

        runner.Start();

        using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(10));
        Assert.True(runner.Buffer.TryEnqueue(0).IsAccepted);
        await AwaitUntilAsync(() => runner.InFlightCount == 1, timeout.Token);

        for (int i = 1; i < flood; i++)
        {
            var outcome = runner.Buffer.TryEnqueue(i);
            Assert.True(outcome.IsAccepted || outcome.Status == QueueEnqueueStatus.RejectedFull);
            Assert.True(runner.Buffer.Count <= capacity);
        }

        Assert.Equal(capacity + 1, runner.Buffer.TotalAccepted);
        Assert.Equal(flood - 1 - capacity, runner.Buffer.TotalRejected);
        Assert.Equal(capacity, runner.Buffer.Count);
        Assert.Equal(0, runner.Buffer.TotalDropped);

        holdGate.TrySetResult(true);
        await runner.ShutdownAsync().WaitAsync(timeout.Token);

        Assert.Equal(capacity + 1, runner.TotalProcessed);
        Assert.Equal(0, runner.PendingCount);
        Assert.Equal(0, runner.InFlightCount);
    }

    [Fact]
    public async Task Overload_UnderDiscardPolicy_KeepsOnlyTheMostRecentWindow()
    {
        const int capacity = 3;
        var holdGate = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        using var runner = new WorkQueueRunner<int>(
            WorkQueueSettings.Create(QueueKind.Response, capacity, 1, SaturationPolicy.DiscardOldest),
            async (_, token) => await holdGate.Task.WaitAsync(token));

        runner.Start();

        using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(10));
        Assert.True(runner.Buffer.TryEnqueue(0).IsAccepted);
        await AwaitUntilAsync(() => runner.InFlightCount == 1, timeout.Token);

        for (int i = 1; i < 50; i++)
        {
            var outcome = runner.Buffer.TryEnqueue(i);
            Assert.True(outcome.IsAccepted);
            Assert.True(runner.Buffer.Count <= capacity);
            if (i > capacity)
            {
                Assert.Equal(QueueEnqueueStatus.DroppedOldest, outcome.Status);
            }
        }

        Assert.Equal(capacity, runner.Buffer.Count);
        Assert.Equal(49 - capacity, runner.Buffer.TotalDropped);

        var window = new List<int>();
        while (runner.Buffer.TryDequeue(out int item))
        {
            window.Add(item);
        }

        Assert.Equal(RecentWindow, window);

        holdGate.TrySetResult(true);
        await runner.ShutdownAsync().WaitAsync(timeout.Token);
        Assert.Equal(0, runner.InFlightCount);
    }

    [Fact]
    public async Task ControlledLoad_ProcessesAllWorkWithoutDropsOrFailures()
    {
        const int load = 100;
        const int concurrency = 4;
        var processed = new List<int>();
        using var runner = new WorkQueueRunner<int>(
            Settings(QueueKind.Tts, load, concurrency),
            (item, _) =>
            {
                lock (processed)
                {
                    processed.Add(item);
                }

                return Task.CompletedTask;
            });

        for (int i = 0; i < load; i++)
        {
            Assert.True(runner.Buffer.TryEnqueue(i).IsAccepted);
        }

        runner.Start();

        using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        await runner.ShutdownAsync().WaitAsync(timeout.Token);

        Assert.Equal(load, runner.TotalProcessed);
        Assert.Equal(0, runner.TotalItemFailures);
        Assert.Equal(0, runner.Buffer.TotalRejected);
        Assert.Equal(0, runner.Buffer.TotalDropped);
        Assert.Equal(0, runner.PendingCount);
        Assert.Equal(0, runner.InFlightCount);
        Assert.Equal(load, processed.Count);
    }

    private static async Task AwaitUntilAsync(Func<bool> condition, CancellationToken token)
    {
        while (!condition())
        {
            token.ThrowIfCancellationRequested();
            await Task.Yield();
        }
    }
}

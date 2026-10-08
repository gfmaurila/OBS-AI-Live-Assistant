using System.Reflection;
using ObsAi.Application.Queues;
using Xunit;

namespace ObsAi.Security.Tests;

public sealed class QueueSecurityTests
{
    private static readonly string[] KeptItems = { "item-3", "item-4" };

    private static WorkQueueSettings RejectSettings(QueueKind kind, int capacity) =>
        WorkQueueSettings.Create(kind, capacity, 1, SaturationPolicy.Reject);

    [Fact]
    public void ObservableSurface_DoesNotExposePendingItemContent()
    {
        var surface = typeof(IWorkBuffer).GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

        var exposedTypes = new List<Type>();
        exposedTypes.AddRange(surface.OfType<PropertyInfo>().Select(property => property.PropertyType));
        foreach (var method in surface.OfType<MethodInfo>())
        {
            exposedTypes.AddRange(method.GetParameters().Select(parameter => parameter.ParameterType));
            exposedTypes.AddRange(new[] { method.ReturnType });
        }

        Assert.DoesNotContain(exposedTypes, type => type.IsGenericType || type.IsArray);
        Assert.All(exposedTypes, type =>
        {
            Assert.True(type == typeof(bool) || type == typeof(long) || type == typeof(int) || type == typeof(QueueKind),
                $"Unexpected observability type: {type.Name}");
        });
    }

    [Fact]
    public async Task AdversarialProducer_CanNeverGrowMemoryBeyondCapacity()
    {
        const int capacity = 10;
        var holdGate = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        using var runner = new WorkQueueRunner<int>(
            RejectSettings(QueueKind.Request, capacity),
            async (_, token) => await holdGate.Task.WaitAsync(token));

        runner.Start();

        using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(10));
        Assert.True(runner.Buffer.TryEnqueue(0).IsAccepted);
        while (runner.InFlightCount == 0)
        {
            timeout.Token.ThrowIfCancellationRequested();
            await Task.Yield();
        }

        for (int i = 1; i < 10_000; i++)
        {
            runner.Buffer.TryEnqueue(i);
            Assert.True(runner.Buffer.Count <= capacity);
        }

        Assert.Equal(capacity, runner.Buffer.Count);
        Assert.Equal(capacity + 1, runner.Buffer.TotalAccepted);
        Assert.Equal(9_999 - capacity, runner.Buffer.TotalRejected);

        holdGate.TrySetResult(true);
        await runner.ShutdownAsync().WaitAsync(timeout.Token);
        Assert.Equal(0, runner.PendingCount);
    }

    [Fact]
    public async Task StageQueues_AreFullyIsolated_NoCrossStageContamination()
    {
        var holdGate = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        using var requestRunner = new WorkQueueRunner<string>(
            RejectSettings(QueueKind.Request, 2),
            async (_, token) => await holdGate.Task.WaitAsync(token));
        using var ttsRunner = new WorkQueueRunner<string>(
            RejectSettings(QueueKind.Tts, 2),
            (_, _) => Task.CompletedTask);

        Assert.True(requestRunner.Buffer.TryEnqueue("request-1").IsAccepted);
        Assert.Equal(1, requestRunner.Buffer.Count);
        Assert.Equal(0, ttsRunner.Buffer.Count);
        Assert.Equal(QueueKind.Request, requestRunner.Buffer.Kind);
        Assert.Equal(QueueKind.Tts, ttsRunner.Buffer.Kind);

        requestRunner.Start();
        ttsRunner.Start();

        using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(10));
        while (requestRunner.InFlightCount == 0)
        {
            timeout.Token.ThrowIfCancellationRequested();
            await Task.Yield();
        }

        Assert.True(ttsRunner.Buffer.TryEnqueue("tts-1").IsAccepted);
        Assert.Equal(1, ttsRunner.Buffer.Count);
        Assert.Equal(0, requestRunner.Buffer.Count);
        Assert.Equal(1, ttsRunner.Buffer.TotalAccepted);
        Assert.Equal(1, requestRunner.Buffer.TotalAccepted);
        Assert.Equal(0, requestRunner.Buffer.TotalDropped);
        Assert.Equal(0, ttsRunner.Buffer.TotalDropped);

        holdGate.TrySetResult(true);
        await requestRunner.ShutdownAsync().WaitAsync(timeout.Token);
        await ttsRunner.ShutdownAsync().WaitAsync(timeout.Token);
        Assert.Equal(1, requestRunner.TotalProcessed);
        Assert.Equal(1, ttsRunner.TotalProcessed);
    }

    [Fact]
    public async Task CompletedBuffer_IsFinal_AndNeverGrowsRegardlessOfAttempts()
    {
        using var runner = new WorkQueueRunner<string>(RejectSettings(QueueKind.Response, 3), (_, _) => Task.CompletedTask);
        runner.Buffer.TryEnqueue("a");
        runner.Start();
        await runner.ShutdownAsync();

        var before = runner.Buffer.Count;
        for (int i = 0; i < 50; i++)
        {
            var outcome = runner.Buffer.TryEnqueue($"late-{i}");
            Assert.Equal(QueueEnqueueStatus.Closed, outcome.Status);
        }

        Assert.Equal(before, runner.Buffer.Count);
        Assert.Equal(50, runner.Buffer.TotalRejected);
        Assert.True(runner.Buffer.IsClosed);
    }

    [Fact]
    public void DroppedItems_AreReportedAndNeverRetained()
    {
        var buffer = new BoundedWorkBuffer<string>(
            WorkQueueSettings.Create(QueueKind.Tts, 2, 1, SaturationPolicy.DiscardOldest));

        for (int i = 0; i < 5; i++)
        {
            var outcome = buffer.TryEnqueue($"item-{i}");
            if (i >= 2)
            {
                Assert.Equal(QueueEnqueueStatus.DroppedOldest, outcome.Status);
                Assert.Equal($"item-{i - 2}", outcome.DroppedItem);
            }
            else
            {
                Assert.True(outcome.IsAccepted);
            }
        }

        Assert.Equal(2, buffer.Count);
        Assert.Equal(5, buffer.TotalAccepted);
        Assert.Equal(3, buffer.TotalDropped);
        var kept = new List<string>();
        while (buffer.TryDequeue(out string? item))
        {
            kept.Add(item!);
        }

        Assert.Equal(KeptItems, kept);
    }
}

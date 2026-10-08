using ObsAi.Application.Queues;
using Xunit;

namespace ObsAi.Unit.Tests;

public sealed class BoundedWorkBufferTests
{
    private static WorkQueueSettings RejectSettings(int capacity) =>
        WorkQueueSettings.Create(QueueKind.Request, capacity, 1, SaturationPolicy.Reject);

    private static WorkQueueSettings DiscardSettings(int capacity) =>
        WorkQueueSettings.Create(QueueKind.Response, capacity, 1, SaturationPolicy.DiscardOldest);

    [Fact]
    public void Create_WithNullSettings_IsRejected()
    {
        Assert.Throws<ArgumentNullException>(() => new BoundedWorkBuffer<string>(null!));
    }

    [Fact]
    public void TryEnqueue_UpToCapacity_AcceptsAllInFifoOrder()
    {
        var buffer = new BoundedWorkBuffer<string>(RejectSettings(3));

        Assert.True(buffer.TryEnqueue("a").IsAccepted);
        Assert.True(buffer.TryEnqueue("b").IsAccepted);
        Assert.True(buffer.TryEnqueue("c").IsAccepted);

        Assert.Equal(3, buffer.Count);
        Assert.True(buffer.IsFull);
        Assert.True(buffer.TryDequeue(out string? first));
        Assert.Equal("a", first);
        Assert.True(buffer.TryDequeue(out string? second));
        Assert.Equal("b", second);
        Assert.True(buffer.TryDequeue(out string? third));
        Assert.Equal("c", third);
        Assert.False(buffer.TryDequeue(out _));
    }

    [Fact]
    public void TryEnqueue_OverCapacityWithReject_RefusesNewWorkAndKeepsMemoryBounded()
    {
        var buffer = new BoundedWorkBuffer<string>(RejectSettings(2));

        Assert.True(buffer.TryEnqueue("a").IsAccepted);
        Assert.True(buffer.TryEnqueue("b").IsAccepted);
        var outcome = buffer.TryEnqueue("c");

        Assert.Equal(QueueEnqueueStatus.RejectedFull, outcome.Status);
        Assert.False(outcome.IsAccepted);
        Assert.Equal(2, buffer.Count);
        Assert.Equal(2, buffer.TotalAccepted);
        Assert.Equal(1, buffer.TotalRejected);
        Assert.Equal(0, buffer.TotalDropped);
    }

    [Fact]
    public void TryEnqueue_OverCapacityWithDiscard_ReplacesOldestAndReportsDroppedItem()
    {
        var buffer = new BoundedWorkBuffer<string>(DiscardSettings(2));

        buffer.TryEnqueue("a");
        buffer.TryEnqueue("b");
        var firstDiscard = buffer.TryEnqueue("c");
        var secondDiscard = buffer.TryEnqueue("d");

        Assert.Equal(QueueEnqueueStatus.DroppedOldest, firstDiscard.Status);
        Assert.Equal("a", firstDiscard.DroppedItem);
        Assert.True(firstDiscard.IsAccepted);
        Assert.Equal("b", secondDiscard.DroppedItem);
        Assert.Equal(2, buffer.Count);
        Assert.Equal(4, buffer.TotalAccepted);
        Assert.Equal(2, buffer.TotalDropped);

        Assert.True(buffer.TryDequeue(out string? first));
        Assert.Equal("c", first);
        Assert.True(buffer.TryDequeue(out string? second));
        Assert.Equal("d", second);
    }

    [Fact]
    public void TryEnqueue_AfterComplete_IsRefusedAndCountedAsRejection()
    {
        var buffer = new BoundedWorkBuffer<string>(RejectSettings(3));
        buffer.TryEnqueue("a");
        buffer.Complete();

        var outcome = buffer.TryEnqueue("b");

        Assert.Equal(QueueEnqueueStatus.Closed, outcome.Status);
        Assert.False(outcome.IsAccepted);
        Assert.True(buffer.IsClosed);
        Assert.Equal(1, buffer.Count);
        Assert.Equal(1, buffer.TotalAccepted);
        Assert.Equal(1, buffer.TotalRejected);
    }

    [Fact]
    public async Task WaitToDequeueAsync_PendingItemsAreDrainedThenWaitEndsAfterComplete()
    {
        var buffer = new BoundedWorkBuffer<string>(RejectSettings(2));
        buffer.TryEnqueue("a");
        buffer.TryEnqueue("b");

        buffer.Complete();

        Assert.True(await buffer.WaitToDequeueAsync(CancellationToken.None));
        Assert.True(buffer.TryDequeue(out string? first));
        Assert.Equal("a", first);
        Assert.True(await buffer.WaitToDequeueAsync(CancellationToken.None));
        Assert.True(buffer.TryDequeue(out string? second));
        Assert.Equal("b", second);
        Assert.False(await buffer.WaitToDequeueAsync(CancellationToken.None));
        Assert.False(buffer.TryDequeue(out _));
    }

    [Fact]
    public async Task WaitToDequeueAsync_EmptyQueue_NeverCompletesUntilAnItemArrives()
    {
        var buffer = new BoundedWorkBuffer<string>(RejectSettings(1));
        using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(10));

        var pending = buffer.WaitToDequeueAsync(timeout.Token).AsTask();
        Assert.False(pending.IsCompleted);
        Assert.True(buffer.TryEnqueue("arrived").IsAccepted);

        Assert.True(await pending);
        Assert.True(buffer.TryDequeue(out string? item));
        Assert.Equal("arrived", item);
    }

    [Fact]
    public async Task WaitToDequeueAsync_CancellationRequestedBeforeWait_ThrowsOperationCanceled()
    {
        var buffer = new BoundedWorkBuffer<string>(RejectSettings(1));
        using var cts = new CancellationTokenSource();
        cts.Cancel();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() =>
            buffer.WaitToDequeueAsync(cts.Token).AsTask());

        Assert.Equal(0, buffer.Count);
    }

    [Fact]
    public async Task WaitToDequeueAsync_CancellationDuringWait_ThrowsAndLeavesBufferUsable()
    {
        var buffer = new BoundedWorkBuffer<string>(RejectSettings(1));
        using var cts = new CancellationTokenSource();
        var pending = buffer.WaitToDequeueAsync(cts.Token).AsTask();
        await Task.Yield();

        cts.Cancel();
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => pending);

        Assert.True(buffer.TryEnqueue("after-cancel").IsAccepted);
        Assert.Equal(1, buffer.Count);
    }

    [Fact]
    public async Task WaitToDequeueAsync_ConcurrentConsumers_DeliverTheWholeSetWithoutLoss()
    {
        const int delivered = 10;
        var buffer = new BoundedWorkBuffer<int>(RejectSettings(delivered));

        async Task<int> ConsumeAsync()
        {
            int consumed = 0;
            while (await buffer.WaitToDequeueAsync(CancellationToken.None))
            {
                if (buffer.TryDequeue(out int item))
                {
                    consumed++;
                }
            }

            return consumed;
        }

        var first = ConsumeAsync();
        var second = ConsumeAsync();
        for (int i = 1; i <= delivered; i++)
        {
            Assert.True(buffer.TryEnqueue(i).IsAccepted);
        }

        buffer.Complete();

        var parts = await Task.WhenAll(first, second);

        Assert.Equal(delivered, parts.Sum());
        Assert.Equal(delivered, buffer.TotalAccepted);
        Assert.Equal(0, buffer.Count);
    }

    [Fact]
    public void Complete_IsIdempotent()
    {
        var buffer = new BoundedWorkBuffer<string>(RejectSettings(1));

        buffer.Complete();
        buffer.Complete();

        Assert.True(buffer.IsClosed);
    }

    [Fact]
    public void Counters_TrackAcceptedRejectedAndDroppedExactly()
    {
        var rejected = new BoundedWorkBuffer<string>(RejectSettings(2));
        rejected.TryEnqueue("a");
        rejected.TryEnqueue("b");
        rejected.TryEnqueue("c");
        rejected.Complete();
        rejected.TryEnqueue("d");

        Assert.Equal(2, rejected.TotalAccepted);
        Assert.Equal(2, rejected.TotalRejected);
        Assert.Equal(0, rejected.TotalDropped);

        var discarded = new BoundedWorkBuffer<string>(DiscardSettings(2));
        discarded.TryEnqueue("x");
        discarded.TryEnqueue("y");
        discarded.TryEnqueue("z");

        Assert.Equal(3, discarded.TotalAccepted);
        Assert.Equal(1, discarded.TotalDropped);
        Assert.Equal(0, discarded.TotalRejected);
    }
}

namespace ObsAi.Application.Queues;

/// <summary>
/// Thread-safe, FIFO bounded buffer for local work items. Memory is always bounded by
/// <see cref="WorkQueueSettings.Capacity"/>: an overflowing producer never grows the buffer and the
/// saturation policy is explicit and observable (RF-013, RF-021, RF-025, RNF-008).
/// </summary>
/// <remarks>
/// The buffer is local to the Assistant Core process and never references a session, broker, provider
/// or external resource. Session isolation remains the responsibility of the lifecycle and lease
/// layer (TASK-007); the buffer is intentionally session-agnostic.
/// </remarks>
/// <typeparam name="T">The type of the work items handled by the buffer.</typeparam>
public sealed class BoundedWorkBuffer<T> : IWorkBuffer
{
    private readonly object sync = new();
    private readonly Queue<T> items = new();
    private readonly List<TaskCompletionSource<bool>> itemWaiters = new();
    private readonly int capacity;
    private readonly SaturationPolicy saturationPolicy;
    private bool closed;
    private long totalAccepted;
    private long totalRejected;
    private long totalDropped;

    /// <summary>Creates a bounded buffer from validated settings. Invalid limits are rejected.</summary>
    public BoundedWorkBuffer(WorkQueueSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);
        capacity = settings.Capacity;
        saturationPolicy = settings.SaturationPolicy;
        Kind = settings.Kind;

        if (capacity < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(settings), "Buffer capacity must be at least 1.");
        }

        if (!Enum.IsDefined(settings.Kind))
        {
            throw new ArgumentOutOfRangeException(nameof(settings), "Unknown queue kind.");
        }

        if (!Enum.IsDefined(settings.SaturationPolicy))
        {
            throw new ArgumentOutOfRangeException(nameof(settings), "Unknown saturation policy.");
        }
    }

    /// <inheritdoc />
    public QueueKind Kind { get; }

    /// <inheritdoc />
    public int Capacity => capacity;

    /// <inheritdoc />
    public int Count
    {
        get
        {
            lock (sync)
            {
                return items.Count;
            }
        }
    }

    /// <inheritdoc />
    public bool IsClosed
    {
        get
        {
            lock (sync)
            {
                return closed;
            }
        }
    }

    /// <inheritdoc />
    public bool IsFull
    {
        get
        {
            lock (sync)
            {
                return !closed && items.Count == capacity;
            }
        }
    }

    /// <inheritdoc />
    public long TotalAccepted
    {
        get
        {
            lock (sync)
            {
                return totalAccepted;
            }
        }
    }

    /// <inheritdoc />
    public long TotalRejected
    {
        get
        {
            lock (sync)
            {
                return totalRejected;
            }
        }
    }

    /// <inheritdoc />
    public long TotalDropped
    {
        get
        {
            lock (sync)
            {
                return totalDropped;
            }
        }
    }

    /// <summary>
    /// Attempts to enqueue one work item. The outcome is explicit: accepted, refused because the
    /// buffer is full, dropped the oldest pending item, or refused because the buffer is closed.
    /// </summary>
    public EnqueueOutcome<T> TryEnqueue(T item)
    {
        TaskCompletionSource<bool>[] waitersToSignal;
        EnqueueOutcome<T> outcome;

        lock (sync)
        {
            if (closed)
            {
                totalRejected++;
                return EnqueueOutcome.Closed<T>();
            }

            if (items.Count < capacity)
            {
                items.Enqueue(item);
                totalAccepted++;
                outcome = EnqueueOutcome.Accepted<T>();
            }
            else
            {
                switch (saturationPolicy)
                {
                    case SaturationPolicy.Reject:
                        totalRejected++;
                        return EnqueueOutcome.RejectedFull<T>();
                    case SaturationPolicy.DiscardOldest:
                        T dropped = items.Dequeue();
                        items.Enqueue(item);
                        totalAccepted++;
                        totalDropped++;
                        outcome = EnqueueOutcome.DroppedOldest(dropped);
                        break;
                    default:
                        throw new InvalidOperationException("Unknown saturation policy.");
                }
            }

            waitersToSignal = CollectItemWaitersLocked();
        }

        SignalItemWaiters(waitersToSignal);
        return outcome;
    }

    /// <summary>
    /// Dequeues the oldest pending item, if any. Returns <c>true</c> with the item, or <c>false</c>
    /// when the buffer is temporarily or permanently empty. <paramref name="item"/> is undefined when
    /// the return value is <c>false</c>.
    /// </summary>
    public bool TryDequeue(out T item)
    {
        lock (sync)
        {
            if (items.Count == 0)
            {
                item = default!;
                return false;
            }

            item = items.Dequeue();
            return true;
        }
    }

    /// <summary>
    /// Waits, without busy waiting, until an item is available or the buffer is permanently drained.
    /// Returns <c>true</c> when at least one item is available to be dequeued via
    /// <see cref="TryDequeue(out T)"/> (a concurrent consumer may still take it), or <c>false</c>
    /// when the buffer is closed and empty. Observation of <paramref name="cancellationToken"/>
    /// throws <see cref="OperationCanceledException"/>.
    /// </summary>
    public async ValueTask<bool> WaitToDequeueAsync(CancellationToken cancellationToken)
    {
        while (true)
        {
            TaskCompletionSource<bool>? waiter = null;

            lock (sync)
            {
                if (items.Count > 0)
                {
                    return true;
                }

                if (closed)
                {
                    return false;
                }

                waiter = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
                itemWaiters.Add(waiter);
            }

            if (cancellationToken.IsCancellationRequested)
            {
                TryRemoveWaiter(waiter);
                throw new OperationCanceledException(cancellationToken);
            }

            try
            {
                await waiter.Task.WaitAsync(cancellationToken);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                TryRemoveWaiter(waiter);
                throw;
            }
        }
    }

    /// <summary>
    /// Closes the buffer for new work. Pending items remain dequeuable by consumers until drained;
    /// after the buffer is closed and drained, enqueues are always refused and waits end with
    /// <c>false</c>.
    /// </summary>
    public void Complete()
    {
        TaskCompletionSource<bool>[] waitersToSignal;

        lock (sync)
        {
            if (closed)
            {
                return;
            }

            closed = true;
            waitersToSignal = CollectItemWaitersLocked();
        }

        SignalItemWaiters(waitersToSignal);
    }

    private TaskCompletionSource<bool>[] CollectItemWaitersLocked()
    {
        if (itemWaiters.Count == 0)
        {
            return Array.Empty<TaskCompletionSource<bool>>();
        }

        TaskCompletionSource<bool>[] waiters = itemWaiters.ToArray();
        itemWaiters.Clear();
        return waiters;
    }

    private void SignalItemWaiters(TaskCompletionSource<bool>[] waiters)
    {
        foreach (TaskCompletionSource<bool> waiter in waiters)
        {
            waiter.TrySetResult(true);
        }
    }

    private void TryRemoveWaiter(TaskCompletionSource<bool> waiter)
    {
        lock (sync)
        {
            itemWaiters.Remove(waiter);
        }
    }
}

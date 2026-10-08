namespace ObsAi.Application.Queues;

/// <summary>
/// Immutable, validated configuration of one bounded work queue and its runner. Invalid limits are
/// rejected at creation, so no queue can be instantiated with an unbounded or negative capacity.
/// Values are intentionally not fixed by the architecture: the streamer configures capacities and
/// concurrency per stage (RNF-008, SEC-015, OQ-010).
/// </summary>
public sealed record WorkQueueSettings
{
    private WorkQueueSettings(QueueKind kind, int capacity, int maxConcurrency, SaturationPolicy saturationPolicy)
    {
        Kind = kind;
        Capacity = capacity;
        MaxConcurrency = maxConcurrency;
        SaturationPolicy = saturationPolicy;
    }

    /// <summary>Gets the pipeline stage this queue belongs to.</summary>
    public QueueKind Kind { get; }

    /// <summary>Gets the maximum number of pending items the queue can hold.</summary>
    public int Capacity { get; }

    /// <summary>Gets the maximum number of work items processed concurrently by the runner.</summary>
    public int MaxConcurrency { get; }

    /// <summary>Gets the explicit policy applied when the queue reaches its capacity.</summary>
    public SaturationPolicy SaturationPolicy { get; }

    /// <summary>
    /// Creates validated settings. Non-positive capacities or concurrency limits are rejected with
    /// <see cref="ArgumentOutOfRangeException"/>.
    /// </summary>
    public static WorkQueueSettings Create(
        QueueKind kind,
        int capacity,
        int maxConcurrency,
        SaturationPolicy saturationPolicy)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(capacity, 1);
        ArgumentOutOfRangeException.ThrowIfLessThan(maxConcurrency, 1);

        if (!Enum.IsDefined(kind))
        {
            throw new ArgumentOutOfRangeException(nameof(kind), "Unknown queue kind.");
        }

        if (!Enum.IsDefined(saturationPolicy))
        {
            throw new ArgumentOutOfRangeException(nameof(saturationPolicy), "Unknown saturation policy.");
        }

        return new WorkQueueSettings(kind, capacity, maxConcurrency, saturationPolicy);
    }
}

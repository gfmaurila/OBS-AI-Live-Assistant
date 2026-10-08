namespace ObsAi.Application.Queues;

/// <summary>
/// Observable, vendor-neutral view of a bounded work buffer. Saturation is observable through the
/// counters without exposing any work item content (SEC-015, SEC-019, RNF-016).
/// </summary>
public interface IWorkBuffer
{
    /// <summary>Gets the pipeline stage this buffer belongs to.</summary>
    QueueKind Kind { get; }

    /// <summary>Gets the maximum number of pending items the buffer can hold.</summary>
    int Capacity { get; }

    /// <summary>Gets the number of pending items currently held by the buffer.</summary>
    int Count { get; }

    /// <summary>Gets a value indicating whether the buffer is closed for new work.</summary>
    bool IsClosed { get; }

    /// <summary>Gets a value indicating whether the buffer currently holds its full capacity.</summary>
    bool IsFull { get; }

    /// <summary>Gets the total number of items admitted since the buffer was created.</summary>
    long TotalAccepted { get; }

    /// <summary>Gets the total number of enqueue attempts refused (rejected or refused after close).</summary>
    long TotalRejected { get; }

    /// <summary>Gets the total number of items dropped by the discard policy.</summary>
    long TotalDropped { get; }
}

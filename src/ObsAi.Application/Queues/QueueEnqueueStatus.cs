namespace ObsAi.Application.Queues;

/// <summary>Describes the result of a single enqueue attempt on a bounded work queue.</summary>
public enum QueueEnqueueStatus
{
    /// <summary>The work item was queued and will be processed.</summary>
    Accepted,

    /// <summary>The queue was full and the configured policy refused the new work (SEC-015).</summary>
    RejectedFull,

    /// <summary>
    /// The queue was full and, under the discard policy, the oldest pending work item was dropped
    /// and replaced by the new one. The dropped item is reported through <see cref="EnqueueOutcome{T}.DroppedItem"/>.
    /// </summary>
    DroppedOldest,

    /// <summary>The queue is closed and no new work can ever be admitted.</summary>
    Closed,
}

namespace ObsAi.Application.Queues;

/// <summary>
/// Defines the explicit, observable behaviour applied when a bounded queue is full (SEC-015).
/// </summary>
public enum SaturationPolicy
{
    /// <summary>
    /// New work is refused with an explicit <see cref="QueueEnqueueStatus.RejectedFull"/> outcome.
    /// The queue size and its memory never grow beyond <see cref="WorkQueueSettings.Capacity"/>.
    /// </summary>
    Reject,

    /// <summary>
    /// To make room for the new work, the oldest pending work item is dropped and reported to the
    /// caller. Only the oldest pending item is dropped and exactly one item is replaced, so the
    /// queue size stays constant and no item is ever duplicated.
    /// </summary>
    DiscardOldest,
}

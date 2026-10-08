namespace ObsAi.Application.Queues;

/// <summary>
/// Non-generic companion entry point for creating <see cref="EnqueueOutcome{T}"/> values without
/// static members on the generic type.
/// </summary>
public static class EnqueueOutcome
{
    /// <summary>Creates an accepted outcome.</summary>
    public static EnqueueOutcome<T> Accepted<T>() => new(QueueEnqueueStatus.Accepted, default);

    /// <summary>Creates a full-buffer rejection outcome for the <see cref="SaturationPolicy.Reject"/> policy.</summary>
    public static EnqueueOutcome<T> RejectedFull<T>() => new(QueueEnqueueStatus.RejectedFull, default);

    /// <summary>
    /// Creates a discard outcome where <paramref name="droppedItem"/> is the oldest pending item that
    /// was replaced by the new work under the <see cref="SaturationPolicy.DiscardOldest"/> policy.
    /// </summary>
    public static EnqueueOutcome<T> DroppedOldest<T>(T droppedItem) => new(QueueEnqueueStatus.DroppedOldest, droppedItem);

    /// <summary>Creates a closed-buffer outcome; the item was never admitted.</summary>
    public static EnqueueOutcome<T> Closed<T>() => new(QueueEnqueueStatus.Closed, default);
}

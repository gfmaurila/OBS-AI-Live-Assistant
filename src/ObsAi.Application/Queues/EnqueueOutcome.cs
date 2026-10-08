namespace ObsAi.Application.Queues;

/// <summary>
/// Reports the outcome of one enqueue attempt. Construction is internal so the only way to create a
/// value is through the <see cref="EnqueueOutcome"/> companion factories, keeping the possible state
/// combinations explicit: a dropped item is only ever present when the status is
/// <see cref="QueueEnqueueStatus.DroppedOldest"/>.
/// </summary>
/// <typeparam name="T">The type of the work items handled by the buffer.</typeparam>
public sealed record EnqueueOutcome<T>
{
    internal EnqueueOutcome(QueueEnqueueStatus status, T? droppedItem)
    {
        Status = status;
        DroppedItem = droppedItem;
    }

    /// <summary>Gets the status of the enqueue attempt.</summary>
    public QueueEnqueueStatus Status { get; }

    /// <summary>
    /// Gets the dropped work item when <see cref="Status"/> is
    /// <see cref="QueueEnqueueStatus.DroppedOldest"/>; otherwise it is the default value.
    /// </summary>
    public T? DroppedItem { get; }

    /// <summary>
    /// Gets a value indicating whether the item was actually queued, including when the discard
    /// policy had to replace the oldest pending item to admit it. Retrying a
    /// <see cref="QueueEnqueueStatus.DroppedOldest"/> outcome would duplicate work, so it counts as
    /// accepted for its own item.
    /// </summary>
    public bool IsAccepted =>
        Status is QueueEnqueueStatus.Accepted or QueueEnqueueStatus.DroppedOldest;
}

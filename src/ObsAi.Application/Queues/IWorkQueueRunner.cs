namespace ObsAi.Application.Queues;

/// <summary>
/// Observable, vendor-neutral view of a bounded work execution policy. Effective concurrency never
/// exceeds the configured limit and failures never terminate the runner (RNF-008, SEC-015).
/// </summary>
public interface IWorkQueueRunner
{
    /// <summary>Gets the number of work items currently executing.</summary>
    int InFlightCount { get; }

    /// <summary>Gets the number of pending items still waiting to execute.</summary>
    int PendingCount { get; }

    /// <summary>Gets the total number of items successfully processed.</summary>
    long TotalProcessed { get; }

    /// <summary>Gets the total number of items whose handler failed.</summary>
    long TotalItemFailures { get; }

    /// <summary>Gets a value indicating whether a coordinated shutdown was requested.</summary>
    bool IsShutdownRequested { get; }

    /// <summary>
    /// Starts the bounded worker pool. Calling <see cref="Start"/> more than once is rejected.
    /// </summary>
    void Start();

    /// <summary>
    /// Performs the coordinated shutdown: closes the queue for new work, lets pending items drain
    /// unless <paramref name="cancellationToken"/> is cancelled, and waits until every worker has
    /// finished, leaving no orphan work. The returned task is the same for every caller.
    /// </summary>
    Task ShutdownAsync(CancellationToken cancellationToken = default);
}

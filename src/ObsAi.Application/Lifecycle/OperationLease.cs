using ObsAi.Domain.Sessions;

namespace ObsAi.Application.Lifecycle;

/// <summary>
/// Grants exactly one operation the right to produce output for exactly one session while the
/// runtime admits it. Cancellation, release and late-result admission stay under the authority of
/// the <see cref="AssistantSessionOrchestrator"/> that created the lease.
/// </summary>
public sealed class OperationLease : IDisposable
{
    private readonly CancellationTokenSource cancellationSource;
    private readonly CancellationToken cancellationToken;
    private readonly Action<OperationLease> cancel;
    private readonly Action<OperationLease> release;
    private int isReleased;

    internal OperationLease(
        SessionId sessionId,
        Guid operationId,
        long generation,
        CancellationTokenSource cancellationSource,
        Action<OperationLease> cancel,
        Action<OperationLease> release)
    {
        SessionId = sessionId;
        OperationId = operationId;
        Generation = generation;
        this.cancellationSource = cancellationSource;
        cancellationToken = cancellationSource.Token;
        this.cancel = cancel;
        this.release = release;
    }

    /// <summary>Gets the session the operation is bound to.</summary>
    public SessionId SessionId { get; }

    /// <summary>Gets the unique identifier of the operation.</summary>
    public Guid OperationId { get; }

    /// <summary>Gets the runtime generation the operation belongs to.</summary>
    public long Generation { get; }

    /// <summary>Gets the token the operation must observe while it works.</summary>
    public CancellationToken CancellationToken => cancellationToken;

    /// <summary>Gets a value indicating whether the lease no longer carries authority.</summary>
    public bool IsReleased => Volatile.Read(ref isReleased) != 0;

    /// <summary>Gets a value indicating whether the operation was cancelled.</summary>
    public bool IsCancellationRequested => cancellationToken.IsCancellationRequested;

    /// <summary>
    /// Cancels this operation only and releases its capacity. After cancellation no result of this
    /// operation may be committed.
    /// </summary>
    public void Cancel() => cancel(this);

    /// <summary>
    /// Releases the lease without cancelling the work. Releasing is idempotent and discards any
    /// pending result of the operation.
    /// </summary>
    public void Dispose() => release(this);

    internal void MarkReleased() => Volatile.Write(ref isReleased, 1);

    internal void CancelCore() => cancellationSource.Cancel();
}

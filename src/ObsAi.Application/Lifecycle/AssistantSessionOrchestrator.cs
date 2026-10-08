using ObsAi.Domain.Context;
using ObsAi.Domain.Profiles;
using ObsAi.Domain.Sessions;

namespace ObsAi.Application.Lifecycle;

/// <summary>
/// Owns the session lifecycle of the Assistant Core: start, pause, resume, ordered shutdown,
/// admission of operations through session leases and atomic discard of late results.
/// </summary>
/// <remarks>
/// The orchestrator is the single admission point for every operation regardless of its origin, and
/// it never exposes the session it owns, so no caller can end a session behind its authority.
/// </remarks>
public sealed class AssistantSessionOrchestrator
{
    private readonly object sync = new();
    private readonly Dictionary<Guid, OperationLease> activeLeases = new();

    private AssistantRuntimeState state = AssistantRuntimeState.Stopped;
    private AssistantSession? currentSession;
    private long generation;

    /// <summary>Gets the explicit runtime state.</summary>
    public AssistantRuntimeState State
    {
        get
        {
            lock (sync)
            {
                return state;
            }
        }
    }

    /// <summary>Gets the identifier of the session currently owned by the runtime, if any.</summary>
    public SessionId? CurrentSessionId
    {
        get
        {
            lock (sync)
            {
                return currentSession?.Id;
            }
        }
    }

    /// <summary>Gets the number of operations that still hold a lease.</summary>
    public int InFlightOperationCount
    {
        get
        {
            lock (sync)
            {
                return activeLeases.Count;
            }
        }
    }

    /// <summary>
    /// Starts a session and moves the runtime to <see cref="AssistantRuntimeState.Running"/>.
    /// The runtime must be stopped; a rejected start leaves the runtime untouched.
    /// </summary>
    public void StartSession(
        SessionId sessionId,
        AssistantProfileId profileId,
        LiveContext liveContext,
        DateTimeOffset startedAtUtc)
    {
        ArgumentNullException.ThrowIfNull(sessionId);
        ArgumentNullException.ThrowIfNull(profileId);
        ArgumentNullException.ThrowIfNull(liveContext);

        lock (sync)
        {
            if (state != AssistantRuntimeState.Stopped)
            {
                throw new InvalidOperationException("A session can only be started while the runtime is stopped.");
            }

            AssistantSession started = AssistantSession.Start(sessionId, profileId, liveContext, startedAtUtc);
            currentSession = started;
            generation++;
            state = AssistantRuntimeState.Running;
        }
    }

    /// <summary>
    /// Pauses the runtime: new operations are rejected while in-flight work keeps its leases.
    /// </summary>
    public void Pause()
    {
        lock (sync)
        {
            if (state != AssistantRuntimeState.Running)
            {
                throw new InvalidOperationException("Only a running runtime can be paused.");
            }

            state = AssistantRuntimeState.Paused;
        }
    }

    /// <summary>
    /// Resumes a paused runtime so new operations can be admitted again.
    /// </summary>
    public void Resume()
    {
        lock (sync)
        {
            if (state != AssistantRuntimeState.Paused)
            {
                throw new InvalidOperationException("Only a paused runtime can be resumed.");
            }

            state = AssistantRuntimeState.Running;
        }
    }

    /// <summary>
    /// Runs the ordered shutdown: closes admission, cancels in-flight work, releases every lease,
    /// ends the session and settles on <see cref="AssistantRuntimeState.Stopped"/>. Calling it on an
    /// already stopped runtime is a no-op.
    /// </summary>
    public void Shutdown(DateTimeOffset endedAtUtc)
    {
        EnsureUtc(endedAtUtc, nameof(endedAtUtc));

        lock (sync)
        {
            if (state == AssistantRuntimeState.Stopped || state == AssistantRuntimeState.Stopping)
            {
                return;
            }

            if (currentSession is not null && endedAtUtc < currentSession.StartedAtUtc)
            {
                throw new ArgumentOutOfRangeException(nameof(endedAtUtc), "Shutdown cannot precede the session start.");
            }

            state = AssistantRuntimeState.Stopping;

            OperationLease[] leases = activeLeases.Values.ToArray();
            foreach (OperationLease lease in leases)
            {
                try
                {
                    CancelLease(lease);
                }
                catch (Exception)
                {
                    // A faulty cancellation observer must never break the ordered shutdown.
                }
            }

            currentSession?.End(endedAtUtc);
            currentSession = null;
            generation++;
            state = AssistantRuntimeState.Stopped;
        }
    }

    /// <summary>
    /// Requests admission for one operation bound to <paramref name="sessionId"/>. Rejections carry an
    /// explicit reason and never change the runtime state.
    /// </summary>
    public LeaseAcquisition TryAcquireOperation(SessionId sessionId)
    {
        ArgumentNullException.ThrowIfNull(sessionId);

        lock (sync)
        {
            LeaseRejectionReason rejection = EvaluateAdmission(sessionId);
            if (rejection != LeaseRejectionReason.None)
            {
                return LeaseAcquisition.Rejected(rejection);
            }

            var operationId = Guid.NewGuid();
            var cancellationSource = new CancellationTokenSource();
            var lease = new OperationLease(
                sessionId,
                operationId,
                generation,
                cancellationSource,
                CancelLease,
                ReleaseLease);

            activeLeases.Add(operationId, lease);
            return LeaseAcquisition.Accepted(lease);
        }
    }

    /// <summary>
    /// Commits the result of an operation while it still carries authority, then finishes the
    /// operation by releasing its lease. Late, cancelled, released or foreign-session results are
    /// discarded and reported as not committed.
    /// </summary>
    public bool TryCommitResult<T>(OperationLease lease, T result, Action<T> commit)
    {
        ArgumentNullException.ThrowIfNull(lease);
        ArgumentNullException.ThrowIfNull(commit);

        lock (sync)
        {
            if (!CanCommit(lease))
            {
                return false;
            }

            commit(result);
            ReleaseLease(lease);
            return true;
        }
    }

    private static void EnsureUtc(DateTimeOffset value, string parameterName)
    {
        if (value.Offset != TimeSpan.Zero)
        {
            throw new ArgumentException("Shutdown timestamps must use UTC.", parameterName);
        }
    }

    private LeaseRejectionReason EvaluateAdmission(SessionId sessionId)
    {
        if (state == AssistantRuntimeState.Stopping)
        {
            return LeaseRejectionReason.RuntimeStopping;
        }

        if (state == AssistantRuntimeState.Stopped)
        {
            return LeaseRejectionReason.RuntimeStopped;
        }

        if (currentSession is null || currentSession.Id != sessionId)
        {
            return LeaseRejectionReason.SessionMismatch;
        }

        if (state == AssistantRuntimeState.Paused)
        {
            return LeaseRejectionReason.RuntimePaused;
        }

        return LeaseRejectionReason.None;
    }

    private bool CanCommit(OperationLease lease)
    {
        if (lease.IsReleased || lease.IsCancellationRequested)
        {
            return false;
        }

        if (lease.Generation != generation || currentSession is null || lease.SessionId != currentSession.Id)
        {
            return false;
        }

        return state == AssistantRuntimeState.Running || state == AssistantRuntimeState.Paused;
    }

    private void CancelLease(OperationLease lease)
    {
        lock (sync)
        {
            if (lease.IsReleased)
            {
                return;
            }

            try
            {
                lease.CancelCore();
            }
            finally
            {
                ReleaseLease(lease);
            }
        }
    }

    private void ReleaseLease(OperationLease lease)
    {
        lock (sync)
        {
            if (lease.IsReleased)
            {
                return;
            }

            activeLeases.Remove(lease.OperationId);
            lease.MarkReleased();
        }
    }
}

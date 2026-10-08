namespace ObsAi.Application.Queues;

/// <summary>
/// Runs the items of a <see cref="BoundedWorkBuffer{T}"/> with a fixed, configured concurrency limit.
/// A slow item only occupies its own worker slot, so a slow stage never blocks other buffers.
/// Handler failures are isolated per item and never terminate the runner (SEC-020, SEC-021).
/// </summary>
/// <typeparam name="T">The type of the work items handled by the buffer.</typeparam>
public sealed class WorkQueueRunner<T> : IWorkQueueRunner, IDisposable
{
    private readonly object sync = new();
    private readonly BoundedWorkBuffer<T> buffer;
    private readonly Func<T, CancellationToken, Task> handler;
    private readonly Action<Exception, T>? onItemFailure;
    private readonly int maxConcurrency;
    private readonly CancellationTokenSource shutdownSource = new();
    private readonly List<Task> workers = new();
    private Task? shutdownTask;
    private long inFlight;
    private long processed;
    private long itemFailures;
    private int started;

    /// <summary>
    /// Creates a runner for the given <paramref name="settings"/>. The same settings are used to
    /// build the bounded buffer. State is only created; nothing starts until <see cref="Start"/>.
    /// </summary>
    public WorkQueueRunner(
        WorkQueueSettings settings,
        Func<T, CancellationToken, Task> handler,
        Action<Exception, T>? onItemFailure = null)
    {
        ArgumentNullException.ThrowIfNull(settings);
        ArgumentNullException.ThrowIfNull(handler);

        buffer = new BoundedWorkBuffer<T>(settings);
        this.handler = handler;
        this.onItemFailure = onItemFailure;
        maxConcurrency = settings.MaxConcurrency;

        if (maxConcurrency < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(settings), "MaxConcurrency must be at least 1.");
        }
    }

    /// <summary>Gets the bounded buffer owned by this runner.</summary>
    public BoundedWorkBuffer<T> Buffer => buffer;

    /// <inheritdoc />
    public int InFlightCount => (int)Interlocked.Read(ref inFlight);

    /// <inheritdoc />
    public int PendingCount => buffer.Count;

    /// <inheritdoc />
    public long TotalProcessed => Interlocked.Read(ref processed);

    /// <inheritdoc />
    public long TotalItemFailures => Interlocked.Read(ref itemFailures);

    /// <inheritdoc />
    public bool IsShutdownRequested
    {
        get
        {
            lock (sync)
            {
                return shutdownTask is not null;
            }
        }
    }

    /// <inheritdoc />
    public void Start()
    {
        if (Interlocked.CompareExchange(ref started, 1, 0) != 0)
        {
            throw new InvalidOperationException("The runner has already been started.");
        }

        lock (sync)
        {
            for (int i = 0; i < maxConcurrency; i++)
            {
                workers.Add(Task.Run(RunWorker));
            }
        }
    }

    /// <inheritdoc />
    public Task ShutdownAsync(CancellationToken cancellationToken = default)
    {
        buffer.Complete();

        lock (sync)
        {
            return shutdownTask ??= DrainAndSignalAsync(cancellationToken);
        }
    }

    /// <summary>
    /// Releases the shutdown cancellation source. Callers must await
    /// <see cref="ShutdownAsync(CancellationToken)"/> before disposing so no orphan work is left.
    /// </summary>
    public void Dispose() => shutdownSource.Cancel();

    private async Task RunWorker()
    {
        while (true)
        {
            bool itemAvailable;
            try
            {
                itemAvailable = await buffer.WaitToDequeueAsync(shutdownSource.Token);
            }
            catch (OperationCanceledException)
            {
                return;
            }

            if (!itemAvailable)
            {
                return;
            }

            if (!buffer.TryDequeue(out T? item))
            {
                continue;
            }

            Interlocked.Increment(ref inFlight);
            try
            {
                await handler(item, shutdownSource.Token);
                Interlocked.Increment(ref processed);
            }
            catch (OperationCanceledException) when (shutdownSource.IsCancellationRequested)
            {
                // A shutdown cancelled this in-flight item deliberately; it is not a handler failure.
            }
            catch (Exception exception)
            {
                Interlocked.Increment(ref itemFailures);
                onItemFailure?.Invoke(exception, item);
            }
            finally
            {
                Interlocked.Decrement(ref inFlight);
            }
        }
    }

    private async Task DrainAndSignalAsync(CancellationToken cancellationToken)
    {
        using var registration = cancellationToken.Register(static state => ((CancellationTokenSource)state!).Cancel(), shutdownSource);
        await Task.WhenAll(workers).ConfigureAwait(false);
    }
}

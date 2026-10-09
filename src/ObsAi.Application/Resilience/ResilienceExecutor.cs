using ObsAi.Application.Contracts.Common;

namespace ObsAi.Application.Resilience;

/// <summary>
/// Executes vendor-neutral operations with bounded timeout, cancellation and retry semantics.
/// Late completion is observed but never returned after the applicable boundary has expired.
/// </summary>
public sealed class ResilienceExecutor(TimeProvider? timeProvider = null)
{
    private const string TimeoutMessage = "The operation timed out.";
    private const string CancelledMessage = "The operation was cancelled.";
    private const string InternalFailureMessage = "The operation failed internally.";

    private readonly TimeProvider timeProvider = timeProvider ?? TimeProvider.System;

    /// <summary>
    /// Executes an operation under the supplied policy. Retry occurs only when the operation is
    /// explicitly idempotent and the normalized failure is allowlisted as retryable.
    /// </summary>
    public async ValueTask<ProviderResult<T>> ExecuteAsync<T>(
        Func<CancellationToken, ValueTask<ProviderResult<T>>> operation,
        ResiliencePolicy policy,
        OperationIdempotency idempotency,
        DateTimeOffset? deadlineUtc = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(operation);
        ArgumentNullException.ThrowIfNull(policy);

        if (!Enum.IsDefined(idempotency))
        {
            throw new ArgumentOutOfRangeException(nameof(idempotency));
        }

        if (cancellationToken.IsCancellationRequested)
        {
            return Cancelled<T>();
        }

        DateTimeOffset? effectiveDeadline = GetEffectiveDeadline(policy.TotalTimeout, deadlineUtc);

        for (int attempt = 1; attempt <= policy.MaximumAttempts; attempt++)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Cancelled<T>();
            }

            TimeSpan attemptTimeout = policy.AttemptTimeout;
            if (effectiveDeadline is { } deadline)
            {
                TimeSpan remaining = deadline - timeProvider.GetUtcNow();
                if (remaining <= TimeSpan.Zero)
                {
                    return TimedOut<T>();
                }

                if (remaining < attemptTimeout)
                {
                    attemptTimeout = remaining;
                }
            }

            ProviderResult<T> result = await ExecuteAttemptAsync(
                operation,
                attemptTimeout,
                cancellationToken).ConfigureAwait(false);

            if (result.IsSuccess || result.Failure?.Category == FailureCategory.Cancelled)
            {
                return result;
            }

            if (attempt == policy.MaximumAttempts
                || idempotency != OperationIdempotency.Idempotent
                || result.Failure is not { IsRetryable: true } failure)
            {
                return result;
            }

            TimeSpan retryDelay = policy.GetRetryDelay(attempt, failure.RetryAfter);
            ProviderResult<T>? delayOutcome = await WaitBeforeRetryAsync<T>(
                retryDelay,
                effectiveDeadline,
                cancellationToken).ConfigureAwait(false);

            if (delayOutcome is not null)
            {
                return delayOutcome;
            }
        }

        return InternalFailure<T>();
    }

    private async ValueTask<ProviderResult<T>> ExecuteAttemptAsync<T>(
        Func<CancellationToken, ValueTask<ProviderResult<T>>> operation,
        TimeSpan timeout,
        CancellationToken cancellationToken)
    {
        using var operationCancellation = new CancellationTokenSource();
        using var boundaryCancellation = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        Task<ProviderResult<T>> operationTask;
        try
        {
            operationTask = operation(operationCancellation.Token).AsTask();
        }
        catch (OperationCanceledException)
        {
            return Cancelled<T>();
        }
        catch (Exception)
        {
            return InternalFailure<T>();
        }

        Task boundaryTask = Task.Delay(timeout, timeProvider, boundaryCancellation.Token);
        Task completed = await Task.WhenAny(operationTask, boundaryTask).ConfigureAwait(false);

        if (completed == operationTask)
        {
            await CancelSafelyAsync(boundaryCancellation).ConfigureAwait(false);
            return await NormalizeCompletionAsync(operationTask).ConfigureAwait(false);
        }

        await CancelSafelyAsync(operationCancellation).ConfigureAwait(false);
        _ = ObserveLateCompletionAsync(operationTask);

        return cancellationToken.IsCancellationRequested
            ? Cancelled<T>()
            : TimedOut<T>();
    }

    private async ValueTask<ProviderResult<T>?> WaitBeforeRetryAsync<T>(
        TimeSpan retryDelay,
        DateTimeOffset? effectiveDeadline,
        CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Cancelled<T>();
        }

        if (effectiveDeadline is { } deadline)
        {
            TimeSpan remaining = deadline - timeProvider.GetUtcNow();
            if (remaining <= TimeSpan.Zero)
            {
                return TimedOut<T>();
            }

            if (retryDelay >= remaining)
            {
                try
                {
                    await Task.Delay(remaining, timeProvider, cancellationToken).ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    return Cancelled<T>();
                }

                return TimedOut<T>();
            }
        }

        try
        {
            await Task.Delay(retryDelay, timeProvider, cancellationToken).ConfigureAwait(false);
            return null;
        }
        catch (OperationCanceledException)
        {
            return Cancelled<T>();
        }
    }

    private DateTimeOffset? GetEffectiveDeadline(TimeSpan? totalTimeout, DateTimeOffset? requestDeadline)
    {
        DateTimeOffset? policyDeadline = totalTimeout is { } timeout
            ? timeProvider.GetUtcNow() + timeout
            : null;

        if (policyDeadline is null)
        {
            return requestDeadline;
        }

        if (requestDeadline is null)
        {
            return policyDeadline;
        }

        return policyDeadline <= requestDeadline ? policyDeadline : requestDeadline;
    }

    private static async ValueTask<ProviderResult<T>> NormalizeCompletionAsync<T>(Task<ProviderResult<T>> operationTask)
    {
        try
        {
            return await operationTask.ConfigureAwait(false);
        }
        catch (OperationCanceledException)
        {
            return Cancelled<T>();
        }
        catch (Exception)
        {
            return InternalFailure<T>();
        }
    }

    private static async Task ObserveLateCompletionAsync<T>(Task<ProviderResult<T>> operationTask)
    {
        try
        {
            await operationTask.ConfigureAwait(false);
        }
        catch (Exception)
        {
            // The boundary already returned a safe result; observing prevents an unobserved fault.
        }
    }

    private static async ValueTask CancelSafelyAsync(CancellationTokenSource source)
    {
        try
        {
            await source.CancelAsync().ConfigureAwait(false);
        }
        catch (Exception)
        {
            // Cancellation observers are untrusted collaborators and cannot change the outcome.
        }
    }

    private static ProviderResult<T> TimedOut<T>() =>
        ProviderResult.Failed<T>(new ProviderFailure(
            ProviderFailureCode.TimedOut,
            TimeoutMessage,
            IsTransient: true));

    private static ProviderResult<T> Cancelled<T>() =>
        ProviderResult.Failed<T>(new ProviderFailure(
            ProviderFailureCode.Cancelled,
            CancelledMessage));

    private static ProviderResult<T> InternalFailure<T>() =>
        ProviderResult.Failed<T>(new ProviderFailure(
            ProviderFailureCode.Unknown,
            InternalFailureMessage));
}

using ObsAi.Application.Contracts.Common;
using ObsAi.Application.Resilience;
using Xunit;

namespace ObsAi.Unit.Tests;

public sealed class ResilienceExecutorTests
{
    private static readonly DateTimeOffset Now = new(2026, 10, 8, 12, 0, 0, TimeSpan.Zero);

    [Fact]
    public async Task OperationCompletesWithinTimeout_ReturnsSuccessOnce()
    {
        var attempts = 0;
        var executor = new ResilienceExecutor();

        ProviderResult<string> result = await executor.ExecuteAsync(
            _ =>
            {
                attempts++;
                return ValueTask.FromResult(ProviderResult.Success("ok"));
            },
            SingleAttemptPolicy(),
            OperationIdempotency.NonIdempotent);

        Assert.True(result.IsSuccess);
        Assert.Equal("ok", result.Value);
        Assert.Equal(1, attempts);
    }

    [Fact]
    public async Task Timeout_CancelsAttemptAndRejectsItsLateResult()
    {
        var time = new ManualTimeProvider(Now);
        var executor = new ResilienceExecutor(time);
        var lateCompletion = new TaskCompletionSource<ProviderResult<string>>(
            TaskCreationOptions.RunContinuationsAsynchronously);
        CancellationToken observedToken = default;

        ValueTask<ProviderResult<string>> Operation(CancellationToken token)
        {
            observedToken = token;
            return new ValueTask<ProviderResult<string>>(lateCompletion.Task);
        }

        Task<ProviderResult<string>> execution = executor.ExecuteAsync(
            Operation,
            SingleAttemptPolicy(),
            OperationIdempotency.Idempotent).AsTask();

        time.Advance(TimeSpan.FromSeconds(5));
        ProviderResult<string> result = await execution;

        Assert.Equal(FailureCategory.Timeout, result.Failure?.Category);
        Assert.True(observedToken.IsCancellationRequested);

        lateCompletion.SetResult(ProviderResult.Success("late output"));
        Assert.False(result.IsSuccess);
        Assert.Null(result.Value);
    }

    [Fact]
    public async Task Timeout_IsolatesFaultyCancellationObserver()
    {
        var time = new ManualTimeProvider(Now);
        var pending = new TaskCompletionSource<ProviderResult<string>>(
            TaskCreationOptions.RunContinuationsAsynchronously);

        ValueTask<ProviderResult<string>> Operation(CancellationToken token)
        {
            token.Register(static () => throw new InvalidOperationException("observer failure"));
            return new ValueTask<ProviderResult<string>>(pending.Task);
        }

        Task<ProviderResult<string>> execution = new ResilienceExecutor(time).ExecuteAsync(
            Operation,
            SingleAttemptPolicy(),
            OperationIdempotency.Idempotent).AsTask();

        time.Advance(TimeSpan.FromSeconds(5));
        ProviderResult<string> result = await execution;

        Assert.Equal(FailureCategory.Timeout, result.Failure?.Category);
        pending.SetResult(ProviderResult.Success("late"));
    }

    [Fact]
    public async Task CancellationBeforeExecution_DoesNotInvokeOperation()
    {
        var invoked = false;
        using var cancellation = new CancellationTokenSource();
        cancellation.Cancel();

        ProviderResult<string> result = await new ResilienceExecutor().ExecuteAsync(
            _ =>
            {
                invoked = true;
                return ValueTask.FromResult(ProviderResult.Success("unexpected"));
            },
            SingleAttemptPolicy(),
            OperationIdempotency.Idempotent,
            cancellationToken: cancellation.Token);

        Assert.False(invoked);
        Assert.Equal(FailureCategory.Cancelled, result.Failure?.Category);
    }

    [Fact]
    public async Task CancellationDuringExecution_StopsNewResultAcceptance()
    {
        var started = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        using var cancellation = new CancellationTokenSource();

        async ValueTask<ProviderResult<string>> Operation(CancellationToken token)
        {
            started.SetResult(true);
            await Task.Delay(Timeout.InfiniteTimeSpan, token);
            return ProviderResult.Success("unexpected");
        }

        Task<ProviderResult<string>> execution = new ResilienceExecutor().ExecuteAsync(
            Operation,
            SingleAttemptPolicy(),
            OperationIdempotency.Idempotent,
            cancellationToken: cancellation.Token).AsTask();

        await started.Task;
        await cancellation.CancelAsync();
        ProviderResult<string> result = await execution;

        Assert.Equal(FailureCategory.Cancelled, result.Failure?.Category);
    }

    [Fact]
    public async Task ExternalCancellation_IsolatesFaultyOperationObserver()
    {
        var started = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        var pending = new TaskCompletionSource<ProviderResult<string>>(
            TaskCreationOptions.RunContinuationsAsynchronously);
        using var cancellation = new CancellationTokenSource();

        ValueTask<ProviderResult<string>> Operation(CancellationToken token)
        {
            token.Register(static () => throw new InvalidOperationException("observer failure"));
            started.SetResult(true);
            return new ValueTask<ProviderResult<string>>(pending.Task);
        }

        Task<ProviderResult<string>> execution = new ResilienceExecutor().ExecuteAsync(
            Operation,
            SingleAttemptPolicy(),
            OperationIdempotency.Idempotent,
            cancellationToken: cancellation.Token).AsTask();

        await started.Task;
        await cancellation.CancelAsync();
        ProviderResult<string> result = await execution;

        Assert.Equal(FailureCategory.Cancelled, result.Failure?.Category);
        pending.SetResult(ProviderResult.Success("late"));
    }

    [Fact]
    public async Task CancellationAfterCompletion_DoesNotRewriteSuccessfulResult()
    {
        using var cancellation = new CancellationTokenSource();

        ProviderResult<string> result = await new ResilienceExecutor().ExecuteAsync(
            _ => ValueTask.FromResult(ProviderResult.Success("done")),
            SingleAttemptPolicy(),
            OperationIdempotency.Idempotent,
            cancellationToken: cancellation.Token);
        await cancellation.CancelAsync();

        Assert.True(result.IsSuccess);
        Assert.Equal("done", result.Value);
    }

    [Fact]
    public async Task TransientIdempotentFailure_RetriesUntilSuccess()
    {
        var attempts = 0;
        ResiliencePolicy policy = ResiliencePolicy.Create(
            TimeSpan.FromSeconds(5),
            null,
            3,
            [TimeSpan.Zero, TimeSpan.Zero]);

        ProviderResult<string> result = await new ResilienceExecutor().ExecuteAsync(
            _ => ValueTask.FromResult(++attempts < 3
                ? TransientFailure<string>()
                : ProviderResult.Success("recovered")),
            policy,
            OperationIdempotency.Idempotent);

        Assert.True(result.IsSuccess);
        Assert.Equal("recovered", result.Value);
        Assert.Equal(3, attempts);
    }

    [Fact]
    public async Task UnsupportedProviderRetryAfter_IsIgnoredWithoutEscapingBoundary()
    {
        var attempts = 0;
        ResiliencePolicy policy = ResiliencePolicy.Create(
            TimeSpan.FromSeconds(5),
            null,
            2,
            [TimeSpan.Zero]);

        ProviderResult<string> result = await new ResilienceExecutor().ExecuteAsync(
            _ => ValueTask.FromResult(++attempts == 1
                ? ProviderResult.Failed<string>(new ProviderFailure(
                    ProviderFailureCode.RateLimited,
                    "Rate limited.",
                    IsTransient: true,
                    RetryAfter: TimeSpan.MaxValue))
                : ProviderResult.Success("recovered")),
            policy,
            OperationIdempotency.Idempotent);

        Assert.True(result.IsSuccess);
        Assert.Equal(2, attempts);
    }

    [Fact]
    public async Task RepeatedTransientFailure_StopsAtMaximumAttempts()
    {
        var attempts = 0;
        ResiliencePolicy policy = ResiliencePolicy.Create(
            TimeSpan.FromSeconds(5),
            null,
            3,
            [TimeSpan.Zero, TimeSpan.Zero]);

        ProviderResult<string> result = await new ResilienceExecutor().ExecuteAsync(
            _ =>
            {
                attempts++;
                return ValueTask.FromResult(TransientFailure<string>());
            },
            policy,
            OperationIdempotency.Idempotent);

        Assert.Equal(3, attempts);
        Assert.Equal(FailureCategory.TransientFailure, result.Failure?.Category);
    }

    [Fact]
    public async Task PermanentFailure_NeverRetries()
    {
        var attempts = 0;

        ProviderResult<string> result = await new ResilienceExecutor().ExecuteAsync(
            _ =>
            {
                attempts++;
                return ValueTask.FromResult(ProviderResult.Failed<string>(new ProviderFailure(
                    ProviderFailureCode.AuthenticationFailed,
                    "Authentication failed.")));
            },
            ThreeAttemptPolicy(),
            OperationIdempotency.Idempotent);

        Assert.Equal(1, attempts);
        Assert.Equal(FailureCategory.PermanentFailure, result.Failure?.Category);
    }

    [Fact]
    public async Task NonIdempotentOperation_NeverRetriesTransientFailure()
    {
        var attempts = 0;

        ProviderResult<string> result = await new ResilienceExecutor().ExecuteAsync(
            _ =>
            {
                attempts++;
                return ValueTask.FromResult(TransientFailure<string>());
            },
            ThreeAttemptPolicy(),
            OperationIdempotency.NonIdempotent);

        Assert.Equal(1, attempts);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task CancellationDuringBackoff_PreventsNextAttempt()
    {
        var attempts = 0;
        var firstAttemptFinished = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        using var cancellation = new CancellationTokenSource();
        ResiliencePolicy policy = ResiliencePolicy.Create(
            TimeSpan.FromMinutes(1),
            null,
            2,
            [TimeSpan.FromMinutes(1)]);

        Task<ProviderResult<string>> execution = new ResilienceExecutor().ExecuteAsync(
            _ =>
            {
                attempts++;
                firstAttemptFinished.SetResult(true);
                return ValueTask.FromResult(TransientFailure<string>());
            },
            policy,
            OperationIdempotency.Idempotent,
            cancellationToken: cancellation.Token).AsTask();

        await firstAttemptFinished.Task;
        await cancellation.CancelAsync();
        ProviderResult<string> result = await execution;

        Assert.Equal(1, attempts);
        Assert.Equal(FailureCategory.Cancelled, result.Failure?.Category);
    }

    [Fact]
    public async Task TotalTimeout_ExpiresDuringBackoffWithoutAnotherAttempt()
    {
        var time = new ManualTimeProvider(Now);
        var attempts = 0;
        ResiliencePolicy policy = ResiliencePolicy.Create(
            TimeSpan.FromMinutes(1),
            TimeSpan.FromSeconds(10),
            2,
            [TimeSpan.FromMinutes(1)]);

        Task<ProviderResult<string>> execution = new ResilienceExecutor(time).ExecuteAsync(
            _ =>
            {
                attempts++;
                return ValueTask.FromResult(TransientFailure<string>());
            },
            policy,
            OperationIdempotency.Idempotent).AsTask();

        time.Advance(TimeSpan.FromSeconds(10));
        ProviderResult<string> result = await execution;

        Assert.Equal(1, attempts);
        Assert.Equal(FailureCategory.Timeout, result.Failure?.Category);
    }

    [Fact]
    public async Task ExpiredRequestDeadline_PreventsOperationInvocation()
    {
        var time = new ManualTimeProvider(Now);
        var invoked = false;

        ProviderResult<string> result = await new ResilienceExecutor(time).ExecuteAsync(
            _ =>
            {
                invoked = true;
                return ValueTask.FromResult(ProviderResult.Success("unexpected"));
            },
            SingleAttemptPolicy(),
            OperationIdempotency.Idempotent,
            deadlineUtc: Now);

        Assert.False(invoked);
        Assert.Equal(FailureCategory.Timeout, result.Failure?.Category);
    }

    [Fact]
    public async Task UnexpectedException_BecomesSafeInternalFailure()
    {
        const string sensitiveDetail = "credential-value-must-not-leak";

        ProviderResult<string> result = await new ResilienceExecutor().ExecuteAsync<string>(
            _ => throw new InvalidOperationException(sensitiveDetail),
            SingleAttemptPolicy(),
            OperationIdempotency.Idempotent);

        Assert.Equal(FailureCategory.InternalFailure, result.Failure?.Category);
        Assert.DoesNotContain(sensitiveDetail, result.Failure?.SafeMessage, StringComparison.Ordinal);
    }

    [Fact]
    public async Task ConcurrentExecutions_DoNotShareAttemptsOrResults()
    {
        var executor = new ResilienceExecutor();
        Task<ProviderResult<int>>[] operations = Enumerable.Range(0, 32)
            .Select(value => executor.ExecuteAsync(
                _ => ValueTask.FromResult(ProviderResult.Success(value)),
                SingleAttemptPolicy(),
                OperationIdempotency.Idempotent).AsTask())
            .ToArray();

        ProviderResult<int>[] results = await Task.WhenAll(operations);

        Assert.Equal(Enumerable.Range(0, 32), results.Select(result => result.Value));
        Assert.All(results, result => Assert.True(result.IsSuccess));
    }

    private static ResiliencePolicy SingleAttemptPolicy() =>
        ResiliencePolicy.Create(TimeSpan.FromSeconds(5), null, 1, []);

    private static ResiliencePolicy ThreeAttemptPolicy() =>
        ResiliencePolicy.Create(
            TimeSpan.FromSeconds(5),
            null,
            3,
            [TimeSpan.Zero, TimeSpan.Zero]);

    private static ProviderResult<T> TransientFailure<T>() =>
        ProviderResult.Failed<T>(new ProviderFailure(
            ProviderFailureCode.Unavailable,
            "Provider unavailable.",
            IsTransient: true));
}

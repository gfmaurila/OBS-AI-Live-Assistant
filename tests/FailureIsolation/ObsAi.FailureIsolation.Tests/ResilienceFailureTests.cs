using ObsAi.Application.Contracts.Common;
using ObsAi.Application.Resilience;
using Xunit;

namespace ObsAi.FailureIsolation.Tests;

public sealed class ResilienceFailureTests
{
    [Fact]
    public async Task RepeatedTransientFailure_IsBoundedAndDoesNotLoop()
    {
        var attempts = 0;
        ResiliencePolicy policy = ResiliencePolicy.Create(
            TimeSpan.FromMinutes(1),
            null,
            4,
            [TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero]);

        ProviderResult<string> result = await new ResilienceExecutor().ExecuteAsync(
            _ =>
            {
                attempts++;
                return ValueTask.FromResult(ProviderResult.Failed<string>(new ProviderFailure(
                    ProviderFailureCode.Unavailable,
                    "Dependency unavailable.",
                    IsTransient: true)));
            },
            policy,
            OperationIdempotency.Idempotent);

        Assert.Equal(4, attempts);
        Assert.Equal(FailureCategory.TransientFailure, result.Failure?.Category);
    }

    [Fact]
    public async Task SynchronousAndAsynchronousExceptions_AreContainedAsInternalFailure()
    {
        var executor = new ResilienceExecutor();
        ResiliencePolicy policy = ResiliencePolicy.Create(TimeSpan.FromMinutes(1), null, 1, []);

        ProviderResult<string> synchronous = await executor.ExecuteAsync<string>(
            _ => throw new InvalidOperationException("synchronous detail"),
            policy,
            OperationIdempotency.Idempotent);
        ProviderResult<string> asynchronous = await executor.ExecuteAsync<string>(
            async _ =>
            {
                await Task.Yield();
                throw new InvalidOperationException("asynchronous detail");
            },
            policy,
            OperationIdempotency.Idempotent);

        Assert.Equal(FailureCategory.InternalFailure, synchronous.Failure?.Category);
        Assert.Equal(FailureCategory.InternalFailure, asynchronous.Failure?.Category);
    }

    [Fact]
    public async Task CancellationDuringRetryWait_ReturnsWithoutStartingMoreWork()
    {
        var attempts = 0;
        var firstFailure = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        using var cancellation = new CancellationTokenSource();
        ResiliencePolicy policy = ResiliencePolicy.Create(
            TimeSpan.FromMinutes(1),
            null,
            2,
            [TimeSpan.FromHours(1)]);

        Task<ProviderResult<string>> execution = new ResilienceExecutor().ExecuteAsync(
            _ =>
            {
                attempts++;
                firstFailure.SetResult(true);
                return ValueTask.FromResult(ProviderResult.Failed<string>(new ProviderFailure(
                    ProviderFailureCode.RateLimited,
                    "Rate limited.",
                    IsTransient: true)));
            },
            policy,
            OperationIdempotency.Idempotent,
            cancellationToken: cancellation.Token).AsTask();

        await firstFailure.Task;
        await cancellation.CancelAsync();
        ProviderResult<string> result = await execution;

        Assert.Equal(1, attempts);
        Assert.Equal(FailureCategory.Cancelled, result.Failure?.Category);
    }
}

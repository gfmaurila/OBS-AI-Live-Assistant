using ObsAi.Application.Contracts.Common;
using ObsAi.Application.Queues;
using ObsAi.Application.Resilience;
using Xunit;

namespace ObsAi.Integration.Tests;

public sealed class ResilienceQueueFlowTests
{
    [Fact]
    public async Task BoundedQueue_ProcessesResilientOperationsWithoutRetryingPermanentFailure()
    {
        var executor = new ResilienceExecutor();
        var outcomes = new List<FailureCategory?>();
        var sync = new object();
        ResiliencePolicy policy = ResiliencePolicy.Create(TimeSpan.FromMinutes(1), null, 2, [TimeSpan.Zero]);
        WorkQueueSettings settings = WorkQueueSettings.Create(
            QueueKind.Request,
            capacity: 2,
            maxConcurrency: 1,
            SaturationPolicy.Reject);

        using var runner = new WorkQueueRunner<bool>(settings, async (shouldFail, token) =>
        {
            ProviderResult<string> result = await executor.ExecuteAsync(
                _ => ValueTask.FromResult(shouldFail
                    ? ProviderResult.Failed<string>(new ProviderFailure(
                        ProviderFailureCode.AuthenticationFailed,
                        "Authentication failed."))
                    : ProviderResult.Success("ok")),
                policy,
                OperationIdempotency.Idempotent,
                cancellationToken: token);

            lock (sync)
            {
                outcomes.Add(result.Failure?.Category);
            }
        });

        Assert.True(runner.Buffer.TryEnqueue(false).IsAccepted);
        Assert.True(runner.Buffer.TryEnqueue(true).IsAccepted);
        runner.Start();
        await runner.ShutdownAsync();

        Assert.Equal([null, FailureCategory.PermanentFailure], outcomes);
        Assert.Equal(2, runner.TotalProcessed);
        Assert.Equal(0, runner.TotalItemFailures);
    }
}

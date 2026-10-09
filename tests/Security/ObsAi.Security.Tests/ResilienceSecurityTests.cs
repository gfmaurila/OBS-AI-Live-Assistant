using ObsAi.Application.Contracts.Common;
using ObsAi.Application.Lifecycle;
using ObsAi.Application.Resilience;
using ObsAi.Domain.Context;
using ObsAi.Domain.Profiles;
using ObsAi.Domain.Sessions;
using Xunit;

namespace ObsAi.Security.Tests;

public sealed class ResilienceSecurityTests
{
    private static readonly DateTimeOffset StartTime = new(2026, 10, 8, 12, 0, 0, TimeSpan.Zero);
    private static readonly SessionId Session =
        SessionId.Create(Guid.Parse("bbbbbbbb-0000-4000-8000-000000000001"));
    private static readonly AssistantProfileId Profile =
        AssistantProfileId.Create(Guid.Parse("bbbbbbbb-0000-4000-8000-000000000002"));

    [Fact]
    public async Task NonIdempotentPublication_IsNeverRepeatedAfterTransientFailure()
    {
        var publicationAttempts = 0;

        ProviderResult<string> result = await new ResilienceExecutor().ExecuteAsync(
            _ =>
            {
                publicationAttempts++;
                return ValueTask.FromResult(ProviderResult.Failed<string>(new ProviderFailure(
                    ProviderFailureCode.Unavailable,
                    "Publication unavailable.",
                    IsTransient: true)));
            },
            ResiliencePolicy.Create(
                TimeSpan.FromMinutes(1),
                null,
                5,
                [TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero]),
            OperationIdempotency.NonIdempotent);

        Assert.False(result.IsSuccess);
        Assert.Equal(1, publicationAttempts);
    }

    [Fact]
    public async Task AuthenticationFailureMarkedTransientByAdapter_IsStillNotRetried()
    {
        var attempts = 0;

        ProviderResult<string> result = await new ResilienceExecutor().ExecuteAsync(
            _ =>
            {
                attempts++;
                return ValueTask.FromResult(ProviderResult.Failed<string>(new ProviderFailure(
                    ProviderFailureCode.AuthenticationFailed,
                    "Authentication failed.",
                    IsTransient: true)));
            },
            ResiliencePolicy.Create(TimeSpan.FromMinutes(1), null, 2, [TimeSpan.Zero]),
            OperationIdempotency.Idempotent);

        Assert.Equal(1, attempts);
        Assert.Equal(FailureCategory.PermanentFailure, result.Failure?.Category);
    }

    [Fact]
    public async Task SessionCancellation_RevokesLeaseAndPreventsResultCommit()
    {
        var orchestrator = new AssistantSessionOrchestrator();
        orchestrator.StartSession(Session, Profile, CreateContext(), StartTime);
        OperationLease lease = orchestrator.TryAcquireOperation(Session).Lease!;
        var started = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        var published = new List<string>();

        async ValueTask<ProviderResult<string>> Operation(CancellationToken token)
        {
            started.SetResult(true);
            await Task.Delay(Timeout.InfiniteTimeSpan, token);
            return ProviderResult.Success("late output");
        }

        Task<ProviderResult<string>> execution = new ResilienceExecutor().ExecuteAsync(
            Operation,
            ResiliencePolicy.Create(TimeSpan.FromMinutes(1), null, 1, []),
            OperationIdempotency.Idempotent,
            cancellationToken: lease.CancellationToken).AsTask();

        await started.Task;
        lease.Cancel();
        ProviderResult<string> result = await execution;

        Assert.Equal(FailureCategory.Cancelled, result.Failure?.Category);
        Assert.False(orchestrator.TryCommitResult(lease, "late output", published.Add));
        Assert.Empty(published);
        Assert.Equal(0, orchestrator.InFlightOperationCount);
    }

    [Fact]
    public async Task UnexpectedException_DoesNotExposeSensitiveDetailOrStackTrace()
    {
        const string sentinel = "sensitive-provider-credential";

        ProviderResult<string> result = await new ResilienceExecutor().ExecuteAsync<string>(
            _ => throw new InvalidOperationException(sentinel),
            ResiliencePolicy.Create(TimeSpan.FromMinutes(1), null, 1, []),
            OperationIdempotency.Idempotent);

        Assert.DoesNotContain(sentinel, result.Failure?.SafeMessage, StringComparison.Ordinal);
        Assert.DoesNotContain("stack", result.Failure?.SafeMessage, StringComparison.OrdinalIgnoreCase);
        Assert.Equal("The operation failed internally.", result.Failure?.SafeMessage);
    }

    private static LiveContext CreateContext() =>
        LiveContext.Create(
            Session,
            new Dictionary<LiveContextField, string> { [LiveContextField.LiveTitle] = "Live segura" },
            LiveContextLimits.Create(1, 32, 32));
}

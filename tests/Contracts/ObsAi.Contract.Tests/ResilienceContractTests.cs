using System.Reflection;
using ObsAi.Application.Contracts.Common;
using ObsAi.Application.Resilience;
using Xunit;

namespace ObsAi.Contract.Tests;

public sealed class ResilienceContractTests
{
    [Theory]
    [InlineData(ProviderFailureCode.TimedOut, true, FailureCategory.Timeout, true)]
    [InlineData(ProviderFailureCode.Cancelled, false, FailureCategory.Cancelled, false)]
    [InlineData(ProviderFailureCode.Unavailable, true, FailureCategory.TransientFailure, true)]
    [InlineData(ProviderFailureCode.InvalidRequest, false, FailureCategory.InvalidInput, false)]
    [InlineData(ProviderFailureCode.AuthenticationFailed, false, FailureCategory.PermanentFailure, false)]
    [InlineData(ProviderFailureCode.Unknown, false, FailureCategory.InternalFailure, false)]
    public void ProviderFailure_MapsToStableCategoryAndRetryDecision(
        ProviderFailureCode code,
        bool transient,
        FailureCategory expectedCategory,
        bool expectedRetryable)
    {
        var failure = new ProviderFailure(code, "Safe diagnostic.", transient);

        Assert.Equal(expectedCategory, failure.Category);
        Assert.Equal(expectedRetryable, failure.IsRetryable);
    }

    [Fact]
    public void UnsafeFailureCodeCannotBecomeRetryableByTransientFlagAlone()
    {
        ProviderFailure[] failures =
        [
            new(ProviderFailureCode.InvalidRequest, "Invalid.", true),
            new(ProviderFailureCode.AuthenticationFailed, "Authentication failed.", true),
            new(ProviderFailureCode.AuthorizationFailed, "Authorization failed.", true),
            new(ProviderFailureCode.QuotaExceeded, "Quota exceeded.", true),
            new(ProviderFailureCode.InvalidResponse, "Invalid response.", true),
            new(ProviderFailureCode.Unknown, "Internal failure.", true),
        ];

        Assert.All(failures, failure => Assert.False(failure.IsRetryable));
    }

    [Fact]
    public void AsyncExecutionContract_PropagatesCancellationTokenAsLastParameter()
    {
        MethodInfo operation = typeof(ResilienceExecutor).GetMethod(nameof(ResilienceExecutor.ExecuteAsync))!;
        ParameterInfo[] parameters = operation.GetParameters();

        Assert.Equal(typeof(CancellationToken), parameters[^1].ParameterType);
        Assert.Equal("cancellationToken", parameters[^1].Name);
    }

    [Fact]
    public void PolicyContract_ExposesImmutableLimitsOnly()
    {
        PropertyInfo[] properties = typeof(ResiliencePolicy).GetProperties();

        Assert.All(properties, property => Assert.False(property.CanWrite));
        Assert.Empty(typeof(ResiliencePolicy).GetConstructors(BindingFlags.Public | BindingFlags.Instance));
    }

}

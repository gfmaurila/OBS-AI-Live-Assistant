using System.Reflection;
using ObsAi.Application.Contracts.Ai;
using ObsAi.Application.Contracts.Chat;
using ObsAi.Application.Contracts.Common;
using ObsAi.Application.Contracts.Tts;
using ObsAi.Application.Ports.Ai;
using ObsAi.Application.Ports.Chat;
using ObsAi.Application.Ports.Tts;
using Xunit;

namespace ObsAi.Contract.Tests;

public sealed class ProviderContractTests
{
    [Fact]
    public void BaseProviderContracts_DoNotPromiseOptionalCapabilities()
    {
        Assert.DoesNotContain(typeof(IAiStreamingProvider), typeof(IAiProvider).GetInterfaces());
        Assert.DoesNotContain(typeof(ITtsStreamingProvider), typeof(ITtsProvider).GetInterfaces());
        Assert.DoesNotContain(typeof(IChatPublisher), typeof(IChatProvider).GetInterfaces());
    }

    [Fact]
    public void ProviderOperations_RequireCancellationTokenAsLastParameter()
    {
        var providerContracts = new[]
        {
            typeof(IChatProvider), typeof(IChatPublisher), typeof(IAiProvider),
            typeof(IAiStreamingProvider), typeof(ITtsProvider), typeof(ITtsStreamingProvider),
        };

        var operations = providerContracts.SelectMany(type => type.GetMethods())
            .Where(method => method.Name.EndsWith("Async", StringComparison.Ordinal));

        foreach (var operation in operations)
        {
            Assert.Equal(typeof(CancellationToken), operation.GetParameters()[^1].ParameterType);
        }
    }

    [Fact]
    public void StreamingProviderOperations_EmitNormalizedResults()
    {
        var streamingContracts = new[] { typeof(IChatProvider), typeof(IAiStreamingProvider), typeof(ITtsStreamingProvider) };

        foreach (var contract in streamingContracts)
        {
            var streamOperation = contract.GetMethods().Single(method => method.ReturnType.IsGenericType);
            var streamItemType = streamOperation.ReturnType.GetGenericArguments().Single();

            Assert.True(streamItemType.IsGenericType);
            Assert.Equal(typeof(ProviderResult<>), streamItemType.GetGenericTypeDefinition());
        }
    }

    [Fact]
    public void ProviderRequests_CarryCredentialReferencesInsteadOfSecretValues()
    {
        var requestTypes = new[] { typeof(ChatSubscription), typeof(ChatPublication), typeof(AiRequest), typeof(TtsRequest) };

        Assert.All(requestTypes, requestType =>
        {
            var properties = requestType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            Assert.DoesNotContain(properties, property =>
                property.Name.Contains("Secret", StringComparison.OrdinalIgnoreCase)
                || property.Name.Contains("ApiKey", StringComparison.OrdinalIgnoreCase)
                || property.Name.Equals("Token", StringComparison.OrdinalIgnoreCase));
        });

        Assert.Equal(typeof(CredentialReference), typeof(AiRequest).GetProperty(nameof(AiRequest.CredentialReference))!.PropertyType);
        Assert.Equal(typeof(CredentialReference), typeof(ChatSubscription).GetProperty(nameof(ChatSubscription.CredentialReference))!.PropertyType);
    }

    [Theory]
    [InlineData(ProviderFailureCode.AuthenticationFailed)]
    [InlineData(ProviderFailureCode.QuotaExceeded)]
    [InlineData(ProviderFailureCode.RateLimited)]
    [InlineData(ProviderFailureCode.TimedOut)]
    [InlineData(ProviderFailureCode.Cancelled)]
    [InlineData(ProviderFailureCode.Unavailable)]
    [InlineData(ProviderFailureCode.InvalidResponse)]
    public void ProviderFailures_ExposeRequiredNormalizedClassifications(ProviderFailureCode code)
    {
        Assert.True(Enum.IsDefined(code));
    }
}

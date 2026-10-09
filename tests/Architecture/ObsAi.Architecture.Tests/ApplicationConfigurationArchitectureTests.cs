using System.Reflection;
using ObsAi.Application.Configuration;
using ObsAi.Application.Contracts.Common;
using ObsAi.Application.Queues;
using ObsAi.Application.Validation;
using ObsAi.Domain.Profiles;
using Xunit;

namespace ObsAi.Architecture.Tests;

public sealed class ApplicationConfigurationArchitectureTests
{
    [Fact]
    public void ConfigurationTypes_BelongToApplicationConfigurationBoundary()
    {
        var types = new[]
        {
            typeof(ConfigurationPolicy),
            typeof(ConfigurationDraft),
            typeof(ConfigurationValidator),
            typeof(ConfigurationState),
            typeof(AssistantConfiguration),
            typeof(ProviderConfigurationReference),
        };

        Assert.All(types, type => Assert.Equal("ObsAi.Application.Configuration", type.Namespace));
        Assert.All(types, type => Assert.Equal("ObsAi.Application", type.Assembly.GetName().Name));
    }

    [Fact]
    public void RuntimeConfiguration_UsesExistingDomainAndApplicationContracts()
    {
        var properties = typeof(AssistantConfiguration).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        Assert.Contains(properties, property => property.PropertyType == typeof(AssistantProfileId));
        Assert.Contains(properties, property => property.PropertyType == typeof(InputValidationLimits));
        Assert.Contains(properties, property =>
            property.PropertyType == typeof(IReadOnlyDictionary<QueueKind, WorkQueueSettings>));
    }

    [Fact]
    public void RuntimeConfigurationAndPolicy_ExposeNoWriteableState()
    {
        foreach (var type in new[] { typeof(AssistantConfiguration), typeof(ConfigurationPolicy), typeof(RateLimitSettings) })
        {
            Assert.All(type.GetProperties(BindingFlags.Public | BindingFlags.Instance), property => Assert.False(property.CanWrite));
            Assert.Empty(type.GetFields(BindingFlags.Public | BindingFlags.Instance));
        }
    }

    [Fact]
    public void ProviderConfiguration_CarriesReferencesNotVendorOptionValues()
    {
        var properties = typeof(ProviderSelection).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        Assert.Contains(properties, property => property.PropertyType == typeof(ProviderId));
        Assert.Contains(properties, property => property.PropertyType == typeof(ProviderConfigurationReference));
        Assert.Contains(properties, property => property.PropertyType == typeof(CredentialReference));
        Assert.DoesNotContain(properties, property => property.PropertyType == typeof(Dictionary<string, string>));
        Assert.DoesNotContain(properties, property => property.PropertyType == typeof(IReadOnlyDictionary<string, string>));
    }

    [Fact]
    public void ConfigurationBoundary_HasNoProviderOrInfrastructureImplementationTypes()
    {
        var configurationTypes = typeof(ConfigurationPolicy).Assembly.GetTypes()
            .Where(type => type.Namespace == "ObsAi.Application.Configuration")
            .ToList();

        Assert.NotEmpty(configurationTypes);
        Assert.DoesNotContain(configurationTypes, type => type.FullName!.Contains("Infrastructure", StringComparison.Ordinal));
        Assert.DoesNotContain(configurationTypes, type => type.FullName!.Contains("ObsAi.Providers", StringComparison.Ordinal));
    }
}

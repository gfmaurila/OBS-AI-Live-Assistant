using System.Reflection;
using ObsAi.Application.Resilience;
using Xunit;

namespace ObsAi.Architecture.Tests;

public sealed class ApplicationResilienceArchitectureTests
{
    private static readonly Assembly ApplicationAssembly = typeof(ResilienceExecutor).Assembly;

    [Fact]
    public void ResilienceTypes_AreVendorNeutralAndOwnedByApplication()
    {
        List<Type> types = ApplicationAssembly.ExportedTypes
            .Where(type => type.Namespace == "ObsAi.Application.Resilience")
            .ToList();

        Assert.Contains(typeof(ResilienceExecutor), types);
        Assert.Contains(typeof(ResiliencePolicy), types);
        Assert.Contains(typeof(OperationIdempotency), types);
        Assert.DoesNotContain(types, type =>
            type.FullName?.Contains("OpenAI", StringComparison.OrdinalIgnoreCase) == true
            || type.FullName?.Contains("YouTube", StringComparison.OrdinalIgnoreCase) == true
            || type.FullName?.Contains("ElevenLabs", StringComparison.OrdinalIgnoreCase) == true);
    }

    [Fact]
    public void ResilienceConcreteTypes_AreSealedAndHaveNoAdapterDependencies()
    {
        List<Type> concreteTypes = ApplicationAssembly.ExportedTypes
            .Where(type => type.Namespace == "ObsAi.Application.Resilience" && type.IsClass && !type.IsAbstract)
            .ToList();

        Assert.NotEmpty(concreteTypes);
        Assert.All(concreteTypes, type => Assert.True(type.IsSealed, $"{type.Name} must be sealed."));

        AssemblyName[] references = ApplicationAssembly.GetReferencedAssemblies();
        Assert.DoesNotContain(references, assembly => assembly.Name is
            "ObsAi.Infrastructure" or "ObsAi.Providers" or "ObsAi.ObsIntegration" or "ObsAi.Host");
    }

    [Fact]
    public void Policy_HasNoBuiltInProductLimitOrMutableState()
    {
        Assert.Empty(typeof(ResiliencePolicy).GetFields(BindingFlags.Public | BindingFlags.Static));
        Assert.All(typeof(ResiliencePolicy).GetProperties(), property => Assert.False(property.CanWrite));
        Assert.Empty(typeof(ResiliencePolicy).GetConstructors(BindingFlags.Public | BindingFlags.Instance));
    }
}

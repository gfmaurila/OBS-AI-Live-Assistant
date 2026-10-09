using System.Reflection;
using ObsAi.Application.Authorization;
using ObsAi.Application.Lifecycle;
using Xunit;

namespace ObsAi.Architecture.Tests;

public sealed class ApplicationAuthorizationArchitectureTests
{
    private static readonly Assembly ApplicationAssembly = typeof(AuthorizationGate).Assembly;

    [Fact]
    public void AuthorizationSurface_UsesOnlyOneExplicitAuthorityPort()
    {
        List<Type> authorizationTypes = ApplicationAssembly.ExportedTypes
            .Where(type => type.Namespace == "ObsAi.Application.Authorization")
            .ToList();

        Type[] interfaces = authorizationTypes.Where(type => type.IsInterface).ToArray();
        Assert.Equal([typeof(IAuthorizationAuthority)], interfaces);

        List<Type> concreteTypes = authorizationTypes.Where(type => type.IsClass && !type.IsAbstract).ToList();
        Assert.All(concreteTypes, type => Assert.True(type.IsSealed, $"{type.Name} must be sealed."));
    }

    [Fact]
    public void AuthorizationRequest_RequiresLifecycleLeaseAndCarriesNoConcreteProvider()
    {
        PropertyInfo[] properties = typeof(AuthorizationRequest).GetProperties();

        Assert.Contains(properties, property => property.PropertyType == typeof(OperationLease));
        Assert.DoesNotContain(properties, property =>
            property.PropertyType.Namespace?.StartsWith("ObsAi.Providers", StringComparison.Ordinal) == true
            || property.PropertyType.Namespace?.StartsWith("ObsAi.ObsIntegration", StringComparison.Ordinal) == true
            || property.PropertyType.Namespace?.StartsWith("ObsAi.Infrastructure", StringComparison.Ordinal) == true);
    }

    [Fact]
    public void AuthorizationGate_IsSynchronousAndAddsNoExecutionCapability()
    {
        MethodInfo[] methods = typeof(AuthorizationGate).GetMethods(
            BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

        MethodInfo evaluate = Assert.Single(methods);
        Assert.Equal(nameof(AuthorizationGate.Evaluate), evaluate.Name);
        Assert.Equal(typeof(AuthorizationDecision), evaluate.ReturnType);
        Assert.DoesNotContain(methods, method => method.ReturnType == typeof(Task));
        Assert.DoesNotContain(methods, method =>
            method.Name.Contains("Execute", StringComparison.Ordinal)
            || method.Name.Contains("Publish", StringComparison.Ordinal)
            || method.Name.Contains("Persist", StringComparison.Ordinal));
    }

    [Fact]
    public void AuthorizationChallenge_ContainsOnlyMinimalNonSensitiveScope()
    {
        string[] properties = typeof(AuthorizationChallenge).GetProperties().Select(property => property.Name).ToArray();

        Assert.Equal(["Context", "Capability"], properties);
    }

    [Fact]
    public void ApplicationAssembly_StillReferencesOnlyFrameworkAndDomain()
    {
        AssemblyName[] referenced = ApplicationAssembly.GetReferencedAssemblies();

        Assert.DoesNotContain(referenced, assembly =>
            assembly.Name is not null
            && !assembly.Name.StartsWith("System", StringComparison.OrdinalIgnoreCase)
            && assembly.Name != "netstandard"
            && assembly.Name != "ObsAi.Domain");
    }
}

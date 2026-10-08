using System.Reflection;
using ObsAi.Application.Lifecycle;
using ObsAi.Domain.Sessions;
using Xunit;

namespace ObsAi.Architecture.Tests;

public sealed class ApplicationLifecycleArchitectureTests
{
    private static readonly Assembly ApplicationAssembly = typeof(AssistantSessionOrchestrator).Assembly;

    [Fact]
    public void LifecycleTypes_AreConcreteApplicationServices()
    {
        var lifecycleTypes = ApplicationAssembly.ExportedTypes
            .Where(type => type.Namespace == "ObsAi.Application.Lifecycle")
            .ToList();

        Assert.NotEmpty(lifecycleTypes);
        Assert.Contains(typeof(AssistantSessionOrchestrator), lifecycleTypes);
        Assert.Contains(typeof(OperationLease), lifecycleTypes);
        Assert.DoesNotContain(lifecycleTypes, type => type.IsInterface);
        Assert.DoesNotContain(lifecycleTypes, type => type.IsSubclassOf(typeof(Delegate)));
    }

    [Fact]
    public void SessionAuthority_DoesNotLeakThroughTheLifecycleSurface()
    {
        var orchestratorType = typeof(AssistantSessionOrchestrator);

        Assert.DoesNotContain(orchestratorType.GetProperties(), property => property.PropertyType == typeof(AssistantSession));
        Assert.DoesNotContain(orchestratorType.GetProperties(), property => property.CanWrite);
        Assert.DoesNotContain(
            orchestratorType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            method => method.ReturnType == typeof(AssistantSession));
        Assert.Empty(typeof(OperationLease).GetConstructors(BindingFlags.Public | BindingFlags.Instance));
    }
}

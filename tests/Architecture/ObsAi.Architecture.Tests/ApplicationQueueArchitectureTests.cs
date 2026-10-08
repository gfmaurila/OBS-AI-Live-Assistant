using System.Reflection;
using ObsAi.Application.Queues;
using Xunit;

namespace ObsAi.Architecture.Tests;

public sealed class ApplicationQueueArchitectureTests
{
    private static readonly Assembly ApplicationAssembly = typeof(WorkQueueSettings).Assembly;

    [Fact]
    public void QueueSurface_IsConcreteSealedTypesPlusPorts()
    {
        var queueTypes = ApplicationAssembly.ExportedTypes
            .Where(type => type.Namespace == "ObsAi.Application.Queues")
            .ToList();

        Assert.NotEmpty(queueTypes);
        Assert.Contains(typeof(BoundedWorkBuffer<>), queueTypes);
        Assert.Contains(typeof(WorkQueueRunner<>), queueTypes);
        Assert.Contains(typeof(WorkQueueSettings), queueTypes);
        Assert.Contains(typeof(EnqueueOutcome<>), queueTypes);
        Assert.Contains(typeof(IWorkBuffer), queueTypes);
        Assert.Contains(typeof(IWorkQueueRunner), queueTypes);

        var concreteTypes = queueTypes.Where(type => type.IsClass && !type.IsAbstract).ToList();
        Assert.NotEmpty(concreteTypes);
        Assert.All(concreteTypes, type => Assert.True(type.IsSealed, $"{type.Name} must be sealed."));
    }

    [Fact]
    public void ApplicationAssembly_ReferencesOnlyFrameworkAndDomain()
    {
        var referenced = ApplicationAssembly.GetReferencedAssemblies();

        Assert.Contains(referenced, assembly => assembly.Name == "ObsAi.Domain");
        Assert.DoesNotContain(referenced, assembly =>
            assembly.Name is not null
            && !assembly.Name.StartsWith("System", StringComparison.OrdinalIgnoreCase)
            && assembly.Name != "netstandard"
            && assembly.Name != "ObsAi.Domain");
    }

    [Fact]
    public void QueueTypes_AreSessionAgnosticAndNeverReferenceDomainSessions()
    {
        var queueTypes = ApplicationAssembly.ExportedTypes
            .Where(type => type.Namespace == "ObsAi.Application.Queues")
            .ToList();

        var referencedDomainTypes = queueTypes
            .SelectMany(CollectPublicSignatureTypes)
            .Where(type => type.Namespace?.StartsWith("ObsAi.Domain", StringComparison.Ordinal) == true)
            .ToList();

        Assert.Empty(referencedDomainTypes);
    }

    [Fact]
    public void QueueSettings_ExposeNoWriteableState()
    {
        var settingsType = typeof(WorkQueueSettings);
        Assert.All(settingsType.GetProperties(), property => Assert.False(property.CanWrite));
        Assert.Empty(settingsType.GetFields(BindingFlags.Public | BindingFlags.Instance));
    }

    [Fact]
    public void QueueRunnerSurface_IsCoordinationAndObservabilityOnly()
    {
        var publicMethods = typeof(IWorkQueueRunner).GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

        Assert.Contains(publicMethods, method => method.Name == nameof(IWorkQueueRunner.Start));
        Assert.Contains(publicMethods, method => method.Name == nameof(IWorkQueueRunner.ShutdownAsync));
        Assert.DoesNotContain(publicMethods, method => method.Name.Contains("Buffer", StringComparison.Ordinal));
        Assert.DoesNotContain(publicMethods, method => method.Name.Contains("Enqueue", StringComparison.Ordinal));
        Assert.DoesNotContain(publicMethods, method => method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(BoundedWorkBuffer<>));
    }

    private static IEnumerable<Type> CollectPublicSignatureTypes(Type type)
    {
        var members = type.GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
        foreach (var member in members)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Property:
                    yield return ((PropertyInfo)member).PropertyType;
                    break;
                case MemberTypes.Method:
                    var method = (MethodInfo)member;
                    foreach (var parameter in method.GetParameters())
                    {
                        yield return parameter.ParameterType;
                    }

                    yield return method.ReturnType;
                    break;
            }
        }
    }
}

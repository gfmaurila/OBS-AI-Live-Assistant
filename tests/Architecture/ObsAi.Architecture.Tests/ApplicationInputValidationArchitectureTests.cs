using System.Reflection;
using ObsAi.Application.Validation;
using Xunit;

namespace ObsAi.Architecture.Tests;

public sealed class ApplicationInputValidationArchitectureTests
{
    private static readonly Assembly ApplicationAssembly = typeof(ChatInputValidator).Assembly;

    [Fact]
    public void InputValidationSurface_HasOnlyTheApprovedSealedTypes()
    {
        var validationTypes = ApplicationAssembly.ExportedTypes
            .Where(type => type.Namespace == "ObsAi.Application.Validation")
            .ToList();

        Assert.NotEmpty(validationTypes);
        Assert.Contains(typeof(ChatInputValidator), validationTypes);
        Assert.Contains(typeof(InputValidationLimits), validationTypes);
        Assert.Contains(typeof(InputRejection), validationTypes);
        Assert.Contains(typeof(InputRejectionReason), validationTypes);
        Assert.Contains(typeof(ChatInputValidationResult), validationTypes);
        Assert.Contains(typeof(NormalizedChatMessage), validationTypes);

        var concreteTypes = validationTypes.Where(type => type.IsClass && !type.IsAbstract).ToList();
        Assert.All(concreteTypes, type => Assert.True(type.IsSealed, $"{type.Name} must be sealed."));

        Assert.DoesNotContain(validationTypes, type => type.IsInterface);
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
    public void InputValidationTypes_AreSessionAgnosticAndNeverReferenceDomainSessions()
    {
        var validationTypes = ApplicationAssembly.ExportedTypes
            .Where(type => type.Namespace == "ObsAi.Application.Validation")
            .ToList();

        var referencedDomainTypes = validationTypes
            .SelectMany(CollectPublicSignatureTypes)
            .Where(type => type.Namespace?.StartsWith("ObsAi.Domain", StringComparison.Ordinal) == true)
            .ToList();

        Assert.Empty(referencedDomainTypes);
    }

    [Fact]
    public void InputValidationLimits_ExposeNoWriteableState()
    {
        var settingsType = typeof(InputValidationLimits);
        Assert.All(settingsType.GetProperties(), property => Assert.False(property.CanWrite));
        Assert.Empty(settingsType.GetFields(BindingFlags.Public | BindingFlags.Instance));
    }

    [Fact]
    public void ValidationSurface_IsSynchronousAndAddsNoSinkOrProviderCapability()
    {
        var validationTypes = ApplicationAssembly.ExportedTypes
            .Where(type => type.Namespace == "ObsAi.Application.Validation")
            .ToList();

        var publicMethods = validationTypes.SelectMany(type => type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly));
        Assert.DoesNotContain(publicMethods, method => method.ReturnType == typeof(Task));
        Assert.DoesNotContain(publicMethods, method => method.Name.Contains("Enqueue", StringComparison.Ordinal));
        Assert.DoesNotContain(publicMethods, method => method.Name.Contains("Publish", StringComparison.Ordinal));
        Assert.DoesNotContain(publicMethods, method => method.Name.Contains("Stream", StringComparison.Ordinal));
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

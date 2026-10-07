using System.Reflection;
using ObsAi.Application.Ports.Ai;
using Xunit;

namespace ObsAi.Architecture.Tests;

public sealed class ApplicationContractsArchitectureTests
{
    private static readonly Assembly ApplicationAssembly = typeof(IAiProvider).Assembly;

    [Fact]
    public void ApplicationContracts_AreFreeFromVendorAndImplementationNamespaces()
    {
        var forbiddenFragments = new[]
        {
            "OpenAI", "Anthropic", "Gemini", "ElevenLabs", "YouTube",
            "Sqlite", "ObsWebSocket", "Microsoft.Win32", "System.Net.Http",
        };

        var referencedAssemblies = ApplicationAssembly.GetReferencedAssemblies().Select(reference => reference.Name ?? string.Empty);
        Assert.DoesNotContain(referencedAssemblies, name =>
            forbiddenFragments.Any(fragment => name.Contains(fragment, StringComparison.OrdinalIgnoreCase)));

        var publicTypeNames = ApplicationAssembly.ExportedTypes.Select(type => type.FullName ?? type.Name);
        Assert.DoesNotContain(publicTypeNames, name =>
            forbiddenFragments.Any(fragment => name.Contains(fragment, StringComparison.OrdinalIgnoreCase)));
    }

    [Fact]
    public void Ports_AreInterfacesOwnedByApplication()
    {
        var portTypes = ApplicationAssembly.ExportedTypes
            .Where(type => type.Namespace?.StartsWith("ObsAi.Application.Ports", StringComparison.Ordinal) == true)
            .ToList();

        Assert.NotEmpty(portTypes);
        Assert.All(portTypes, type => Assert.True(type.IsInterface, $"Port '{type.FullName}' must be an interface."));
    }

    [Fact]
    public void PublicAsyncPortOperations_AcceptCancellation()
    {
        var operations = ApplicationAssembly.ExportedTypes
            .Where(type => type.IsInterface && type.Namespace?.StartsWith("ObsAi.Application.Ports", StringComparison.Ordinal) == true)
            .SelectMany(type => type.GetMethods())
            .Where(method => method.Name.EndsWith("Async", StringComparison.Ordinal));

        Assert.All(operations, method =>
            Assert.Contains(method.GetParameters(), parameter => parameter.ParameterType == typeof(CancellationToken)));
    }
}

using ObsAi.Domain.Sessions;
using Xunit;

namespace ObsAi.Architecture.Tests;

public sealed class DomainModelArchitectureTests
{
    [Fact]
    public void DomainModel_IsOwnedByDomainAndFreeFromExternalTechnologyNames()
    {
        var domainAssembly = typeof(AssistantSession).Assembly;
        var forbiddenFragments = new[]
        {
            "Application", "Infrastructure", "Provider", "Sqlite", "ObsWebSocket",
            "OpenAI", "Anthropic", "Gemini", "YouTube", "Microsoft.Win32", "System.Net.Http",
        };

        Assert.All(domainAssembly.ExportedTypes, type =>
            Assert.StartsWith("ObsAi.Domain.", type.Namespace, StringComparison.Ordinal));
        Assert.DoesNotContain(domainAssembly.ExportedTypes.Select(type => type.FullName ?? type.Name), name =>
            forbiddenFragments.Any(fragment => name.Contains(fragment, StringComparison.OrdinalIgnoreCase)));
        Assert.DoesNotContain(domainAssembly.GetReferencedAssemblies().Select(reference => reference.Name ?? string.Empty), name =>
            forbiddenFragments.Any(fragment => name.Contains(fragment, StringComparison.OrdinalIgnoreCase)));
    }

    [Fact]
    public void DomainBaseline_ContainsNoPersistentMemoryType()
    {
        var domainTypes = typeof(AssistantSession).Assembly.ExportedTypes;

        Assert.DoesNotContain(domainTypes, type =>
            type.Name.Contains("PersistentMemory", StringComparison.OrdinalIgnoreCase));
    }
}

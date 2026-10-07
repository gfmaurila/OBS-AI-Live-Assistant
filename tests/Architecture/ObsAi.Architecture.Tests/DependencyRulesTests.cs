using Xunit;

namespace ObsAi.Architecture.Tests;

public sealed class DependencyRulesTests
{
    private static string RepositoryRoot =>
        RepositoryModel.FindRepositoryRoot(AppContext.BaseDirectory);

    [Fact]
    public void ExpectedSourceProjects_Exist()
    {
        foreach (var projectName in DependencyRules.ExpectedSourceProjects)
        {
            var projectDirectory = Path.Combine(RepositoryRoot, "src", projectName);
            Assert.True(Directory.Exists(projectDirectory), $"'{projectName}' must exist under src/.");
            Assert.True(File.Exists(Path.Combine(projectDirectory, $"{projectName}.csproj")), $"'{projectName}.csproj' is missing.");
        }
    }

    [Fact]
    public void AllSourceProjects_AreRegisteredInSolution()
    {
        var registeredPaths = RepositoryModel.LoadSolutionProjectPaths(RepositoryRoot);
        foreach (var projectName in DependencyRules.ExpectedSourceProjects)
        {
            var expectedPath = RepositoryModel.NormalizePath(Path.Combine("src", projectName, $"{projectName}.csproj"));
            Assert.Contains(registeredPaths, path => string.Equals(path, expectedPath, StringComparison.OrdinalIgnoreCase));
        }
    }

    [Fact]
    public void SourceProjects_DoNotReferenceNuGetPackages()
    {
        var sourceProjects = RepositoryModel.LoadProjects(RepositoryRoot).Where(project => project.IsSource).ToList();
        Assert.NotEmpty(sourceProjects);
        foreach (var project in sourceProjects)
        {
            Assert.Empty(project.PackageReferences);
        }
    }

    [Fact]
    public void ProjectReferences_RespectAllowedProjectReferences()
    {
        var projects = RepositoryModel.LoadProjects(RepositoryRoot);
        foreach (var projectName in DependencyRules.ExpectedSourceProjects)
        {
            var project = projects.Single(candidate => candidate.Name == projectName);
            var allowed = DependencyRules.AllowedProjectReferences[projectName];
            var forbidden = project.ReferencedProjects.Except(allowed).ToList();
            Assert.Empty(forbidden);
        }
    }

    [Fact]
    public void Application_DoesNotReferenceImplementations()
    {
        var application = RepositoryModel.LoadProjects(RepositoryRoot).Single(project => project.Name == "ObsAi.Application");
        Assert.DoesNotContain("ObsAi.Infrastructure", application.ReferencedProjects);
        Assert.DoesNotContain("ObsAi.Providers", application.ReferencedProjects);
        Assert.DoesNotContain("ObsAi.ObsIntegration", application.ReferencedProjects);
        Assert.DoesNotContain("ObsAi.Host", application.ReferencedProjects);
    }

    [Fact]
    public void LayerBoundaries_DoNotReferenceHost()
    {
        var projects = RepositoryModel.LoadProjects(RepositoryRoot);
        foreach (var projectName in new[] { "ObsAi.Domain", "ObsAi.Infrastructure", "ObsAi.Providers", "ObsAi.ObsIntegration" })
        {
            var project = projects.Single(candidate => candidate.Name == projectName);
            Assert.DoesNotContain("ObsAi.Host", project.ReferencedProjects);
        }
    }

    [Fact]
    public void Domain_DoesNotReferenceAnyProject()
    {
        var domain = RepositoryModel.LoadProjects(RepositoryRoot).Single(project => project.Name == "ObsAi.Domain");
        Assert.Empty(domain.ReferencedProjects);
    }

    [Fact]
    public void SourceProjects_DoNotReferenceTests()
    {
        var projects = RepositoryModel.LoadProjects(RepositoryRoot);
        var testProjectNames = projects.Where(project => !project.IsSource).Select(project => project.Name).ToHashSet();
        var sourceProjects = projects.Where(project => project.IsSource).ToList();
        Assert.NotEmpty(sourceProjects);
        foreach (var project in sourceProjects)
        {
            var referencedTests = project.ReferencedProjects.Where(name => testProjectNames.Contains(name)).ToList();
            Assert.Empty(referencedTests);
        }
    }

    [Fact]
    public void ReferenceGraph_HasNoCycles()
    {
        var projects = RepositoryModel.LoadProjects(RepositoryRoot)
            .Where(project => project.IsSource)
            .ToDictionary(project => project.Name, project => (IReadOnlyCollection<string>)project.ReferencedProjects);
        var visited = new Dictionary<string, bool>();

        foreach (var projectName in projects.Keys)
        {
            Assert.True(VisitCycle(projectName, projects, visited), $"Cycle detected starting at '{projectName}'.");
        }
    }

    private static bool VisitCycle(string projectName, IReadOnlyDictionary<string, IReadOnlyCollection<string>> graph, IDictionary<string, bool> visited)
    {
        if (visited.TryGetValue(projectName, out var isComplete))
        {
            return isComplete;
        }

        visited[projectName] = false;
        foreach (var referenced in graph[projectName])
        {
            if (!graph.ContainsKey(referenced))
            {
                continue;
            }

            if (!VisitCycle(referenced, graph, visited))
            {
                return false;
            }
        }

        visited[projectName] = true;
        return true;
    }
}

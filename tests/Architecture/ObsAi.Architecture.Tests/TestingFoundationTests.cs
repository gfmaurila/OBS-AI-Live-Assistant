using System.Text.RegularExpressions;
using Xunit;

namespace ObsAi.Architecture.Tests;

public sealed class TestingFoundationTests
{
    private static string RepositoryRoot =>
        RepositoryModel.FindRepositoryRoot(AppContext.BaseDirectory);

    private static string CharterPath =>
        Path.Combine(RepositoryRoot, "docs", "testing", "TEST_ARCHITECTURE.md");

    private static readonly string[] CategoryNames = new[]
    {
        "Unit", "Integration", "Architecture", "Contracts", "Security",
        "FailureIsolation", "ObsCompatibility", "Installer", "Regression/E2E",
    };

    private static readonly string[] ScaffoldedCategoryProjects = new[]
    {
        "tests/Unit/ObsAi.Unit.Tests/ObsAi.Unit.Tests.csproj",
        "tests/Integration/ObsAi.Integration.Tests/ObsAi.Integration.Tests.csproj",
        "tests/Contracts/ObsAi.Contract.Tests/ObsAi.Contract.Tests.csproj",
        "tests/Security/ObsAi.Security.Tests/ObsAi.Security.Tests.csproj",
        "tests/FailureIsolation/ObsAi.FailureIsolation.Tests/ObsAi.FailureIsolation.Tests.csproj",
        "tests/Installer/ObsAi.Installer.Tests/ObsAi.Installer.Tests.csproj",
    };

    private static readonly string[] ScaffoldedProjectNames = new[]
    {
        "ObsAi.Unit.Tests", "ObsAi.Integration.Tests", "ObsAi.Contract.Tests",
        "ObsAi.Security.Tests", "ObsAi.FailureIsolation.Tests", "ObsAi.Installer.Tests",
    };

    private static readonly string[] ApprovedTestPackages = new[]
    {
        "Microsoft.NET.Test.Sdk", "xunit", "xunit.runner.visualstudio",
    };

    private static readonly string[] PermittedGateTokens = new[]
    {
        "EXECUTED", "PARTIAL", "NOT EXECUTED", "NOT CREATED", "NOT APPLICABLE",
    };

    [Fact]
    public void TestingArchitectureCharter_ExistsAtCanonicalLocation()
    {
        Assert.True(File.Exists(CharterPath), "TEST_ARCHITECTURE.md is missing.");
    }

    [Fact]
    public void TestingArchitectureCharter_DeclaresAllCategories()
    {
        var charter = File.ReadAllText(CharterPath);
        foreach (var category in CategoryNames)
        {
            Assert.Contains($"| {category} |", charter);
        }
    }

    [Fact]
    public void TestingArchitectureCharter_DeclaresLocalizationAndGatePerCategory()
    {
        var charter = File.ReadAllText(CharterPath);
        Assert.Contains("Projeto ou harness", charter);

        var rows = charter.Split('\n');
        foreach (var category in CategoryNames)
        {
            var row = rows.FirstOrDefault(line => line.StartsWith($"| {category} |", StringComparison.Ordinal));
            Assert.True(row is not null, $"Charter row for '{category}' not found.");
            Assert.True(PermittedGateTokens.Any(token => row!.Contains(token, StringComparison.Ordinal)), $"Gate token missing for '{category}'.");
        }
    }

    [Fact]
    public void TestingArchitectureCharter_ReferencesRequirementsAndAdrs()
    {
        var charter = File.ReadAllText(CharterPath);
        foreach (var token in new[] { "RNF-019", "RNF-028", "SEC-029", "SEC-032", "ADR-010", "ADR-011", "TASK-004", "TASK-049" })
        {
            Assert.Contains(token, charter);
        }
    }

    [Fact]
    public void ScaffoldedTestProjects_ExistUnderExpectedPaths()
    {
        foreach (var relativePath in ScaffoldedCategoryProjects)
        {
            Assert.True(File.Exists(Path.Combine(RepositoryRoot, relativePath)), $"Scaffold missing: {relativePath}");
        }
    }

    [Fact]
    public void ScaffoldedTestProjects_AreRegisteredInSolution()
    {
        var registeredPaths = RepositoryModel.LoadSolutionProjectPaths(RepositoryRoot);
        foreach (var relativePath in ScaffoldedCategoryProjects)
        {
            var normalized = RepositoryModel.NormalizePath(relativePath);
            Assert.Contains(registeredPaths, path => string.Equals(path, normalized, StringComparison.OrdinalIgnoreCase));
        }
    }

    [Fact]
    public void ScaffoldedTestProjects_HaveNoProjectReferences()
    {
        var projects = RepositoryModel.LoadProjects(RepositoryRoot);
        foreach (var projectName in ScaffoldedProjectNames)
        {
            var project = projects.Single(candidate => candidate.Name == projectName);
            Assert.Empty(project.ReferencedProjects);
        }
    }

    [Fact]
    public void ScaffoldedTestProjects_ReferenceOnlyApprovedTestPackages()
    {
        var projects = RepositoryModel.LoadProjects(RepositoryRoot);
        foreach (var projectName in ScaffoldedProjectNames)
        {
            var project = projects.Single(candidate => candidate.Name == projectName);
            Assert.Equal(ApprovedTestPackages, project.PackageReferences);
        }
    }

    [Fact]
    public void ObsCompatibility_KeepsExistingHarness_WithoutNewProject()
    {
        var charter = File.ReadAllText(CharterPath);
        Assert.Contains("prototypes/compat-sniff", charter);
        Assert.Contains("CompatibilityMatrixTests", charter);
        Assert.Contains("TASK-051", charter);
        Assert.True(Directory.Exists(Path.Combine(RepositoryRoot, "prototypes", "compat-sniff")), "compat-sniff prototype is missing.");

        var registeredPaths = RepositoryModel.LoadSolutionProjectPaths(RepositoryRoot);
        Assert.DoesNotContain(registeredPaths, path => path.Contains("ObsCompatibility", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void TestProjects_ContainNoTrivialAssertions()
    {
        var trivialPatterns = new[]
        {
            new Regex(@"Assert\.True\(\s*true\s*\)", RegexOptions.Compiled | RegexOptions.IgnoreCase),
            new Regex(@"Assert\.False\(\s*false\s*\)", RegexOptions.Compiled | RegexOptions.IgnoreCase),
            new Regex(@"Assert\.Equal\(\s*1,\s*1\s*\)", RegexOptions.Compiled | RegexOptions.IgnoreCase),
            new Regex(@"Assert\.Equal\(\s*0,\s*0\s*\)", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        };
        var violations = new List<string>();
        foreach (var source in Directory.EnumerateFiles(Path.Combine(RepositoryRoot, "tests"), "*.cs", SearchOption.AllDirectories))
        {
            var content = File.ReadAllText(source);
            if (trivialPatterns.Any(pattern => pattern.IsMatch(content)))
            {
                violations.Add(Path.GetRelativePath(RepositoryRoot, source));
            }
        }

        Assert.False(violations.Count > 0, "Trivial/fictitious assertions found in: " + string.Join(", ", violations));
    }

    [Fact]
    public void TestingCommands_AreDocumentedInTestingReadme()
    {
        var readmePath = Path.Combine(RepositoryRoot, "docs", "testing", "README.md");
        Assert.True(File.Exists(readmePath), "docs/testing/README.md is missing.");
        var readme = File.ReadAllText(readmePath);
        Assert.Contains("TEST_ARCHITECTURE.md", readme);
        foreach (var token in new[]
        {
            "dotnet restore",
            "dotnet build --no-restore",
            "dotnet test --no-build",
            "dotnet format --verify-no-changes --no-restore",
            "tooling\\quality-gates.ps1",
        })
        {
            Assert.Contains(token, readme);
        }
    }

    [Fact]
    public void QualityGates_FlagUnavailableTestCategories()
    {
        var gatesPath = Path.Combine(RepositoryRoot, "docs", "governance", "QUALITY_GATES.md");
        Assert.True(File.Exists(gatesPath), "QUALITY_GATES.md is missing.");
        var gates = File.ReadAllText(gatesPath);
        Assert.Contains("NOT APPLICABLE UNTIL IMPLEMENTATION", gates);
        foreach (var token in new[] { "Unit Tests", "Integration Tests", "Installer Tests", "OBS Compatibility Tests", "Regression Tests" })
        {
            Assert.Contains(token, gates);
        }
    }

    [Fact]
    public void ValidationRunner_IsDocumentedInTooling()
    {
        var scriptPath = Path.Combine(RepositoryRoot, "tooling", "quality-gates.ps1");
        Assert.True(File.Exists(scriptPath), "quality-gates.ps1 is missing.");
        var script = File.ReadAllText(scriptPath);
        Assert.Contains("& dotnet", script);
        foreach (var token in new[] { "restore", "build", "test", "format" })
        {
            Assert.Contains(token, script);
        }

        var toolingReadmePath = Path.Combine(RepositoryRoot, "tooling", "README.md");
        Assert.True(File.Exists(toolingReadmePath), "tooling/README.md is missing.");
        var readme = File.ReadAllText(toolingReadmePath);
        Assert.Contains("quality-gates.ps1", readme);
        Assert.Contains("dotnet restore", readme);
        Assert.Contains("dotnet build --no-restore", readme);
        Assert.Contains("dotnet test --no-build", readme);
        Assert.Contains("dotnet format --verify-no-changes --no-restore", readme);
    }
}

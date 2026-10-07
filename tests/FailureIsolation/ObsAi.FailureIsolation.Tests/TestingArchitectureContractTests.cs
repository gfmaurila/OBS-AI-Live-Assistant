using Xunit;

namespace ObsAi.FailureIsolation.Tests;

public sealed class TestingArchitectureContractTests
{
    private static string FindRepositoryRoot(string startDirectory)
    {
        var current = new DirectoryInfo(startDirectory);
        while (current is not null)
        {
            if (File.Exists(Path.Combine(current.FullName, "OBS-AI-Live-Assistant.slnx")))
            {
                return current.FullName;
            }

            current = current.Parent;
        }

        throw new DirectoryNotFoundException("Repository root not found.");
    }

    private static string ReadTestingArchitectureCharter()
    {
        var root = FindRepositoryRoot(AppContext.BaseDirectory);
        return File.ReadAllText(Path.Combine(root, "docs", "testing", "TEST_ARCHITECTURE.md"));
    }

    [Fact]
    public void FailureIsolation_Category_IsDocumentedWithProjectAndGate()
    {
        var charter = ReadTestingArchitectureCharter();

        Assert.Contains("| FailureIsolation |", charter);
        Assert.Contains("tests/FailureIsolation/ObsAi.FailureIsolation.Tests", charter);
        Assert.Contains("NOT EXECUTED", charter);
    }
}

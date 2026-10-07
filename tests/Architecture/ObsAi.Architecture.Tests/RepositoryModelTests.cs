using System.Xml.Linq;
using Xunit;

namespace ObsAi.Architecture.Tests;

public sealed class RepositoryModelTests
{
    private static readonly string[] SampleReferencedProjects = new[] { "ObsAi.Domain" };

    private static readonly string[] SamplePackageReferences = new[] { "xunit" };

    private static readonly string[] SampleSolutionPaths = new[] { RepositoryModel.NormalizePath("src/ObsAi.Domain/ObsAi.Domain.csproj") };

    [Fact]
    public void NormalizePath_ConvertsForwardAndBackwardSeparators()
    {
        Assert.Equal("src" + Path.DirectorySeparatorChar + "ObsAi.Domain", RepositoryModel.NormalizePath("src/ObsAi.Domain"));
        Assert.Equal("tests" + Path.DirectorySeparatorChar + "Architecture", RepositoryModel.NormalizePath(@"tests\Architecture"));
    }

    [Fact]
    public void FindRepositoryRoot_ReturnsDirectoryContainingSolutionMarker()
    {
        var root = RepositoryModel.FindRepositoryRoot(AppContext.BaseDirectory);
        Assert.True(File.Exists(Path.Combine(root, "OBS-AI-Live-Assistant.slnx")));
    }

    [Fact]
    public void FindRepositoryRoot_ThrowsWhenMarkerIsMissing()
    {
        var temporaryDirectory = Path.Combine(Path.GetTempPath(), "obsai-arch-tests-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(temporaryDirectory);
        try
        {
            Assert.Throws<DirectoryNotFoundException>(() => RepositoryModel.FindRepositoryRoot(temporaryDirectory));
        }
        finally
        {
            Directory.Delete(temporaryDirectory, recursive: true);
        }
    }

    [Fact]
    public void ParseProjectDocument_ExtractsReferencesAndPackageNames()
    {
        var repositoryRoot = @"C:/repo";
        var csprojPath = @"C:/repo/src/ObsAi.Sample/ObsAi.Sample.csproj";
        var document = XDocument.Parse(
            """
            <Project Sdk="Microsoft.NET.Sdk">
              <ItemGroup>
                <ProjectReference Include="..\ObsAi.Domain\ObsAi.Domain.csproj" />
                <PackageReference Include="xunit" />
              </ItemGroup>
            </Project>
            """);

        var model = RepositoryModel.ParseProjectDocument(csprojPath, repositoryRoot, document);

        Assert.True(model.IsSource);
        Assert.Equal(SampleReferencedProjects, model.ReferencedProjects);
        Assert.Equal(SamplePackageReferences, model.PackageReferences);
    }

    [Fact]
    public void ParseProjectDocument_FlagsTestsAsNotSource()
    {
        var repositoryRoot = @"C:/repo";
        var csprojPath = @"C:/repo/tests/Architecture/ObsAi.Tests/ObsAi.Tests.csproj";
        var document = XDocument.Parse("<Project Sdk=\"Microsoft.NET.Sdk\" />");

        var model = RepositoryModel.ParseProjectDocument(csprojPath, repositoryRoot, document);

        Assert.False(model.IsSource);
        Assert.Empty(model.ReferencedProjects);
        Assert.Empty(model.PackageReferences);
    }

    [Fact]
    public void LoadSolutionProjectPaths_NormalizesEntries()
    {
        var document = XDocument.Parse(
            """
            <Solution>
              <Folder Name="/src/">
                <Project Path="src/ObsAi.Domain/ObsAi.Domain.csproj" />
              </Folder>
            </Solution>
            """);

        var paths = RepositoryModel.LoadSolutionProjectPathsFromDocument(document);

        Assert.Equal(SampleSolutionPaths, paths);
    }
}

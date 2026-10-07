using System.Xml.Linq;

namespace ObsAi.Architecture.Tests;

internal static class RepositoryModel
{
    public static string FindRepositoryRoot(string startDirectory)
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

        throw new DirectoryNotFoundException($"Repository root was not found starting from '{startDirectory}'.");
    }

    public static IReadOnlyList<ProjectModel> LoadProjects(string repositoryRoot)
    {
        var projects = new List<ProjectModel>();
        foreach (var relativeDirectory in new[] { Path.Combine("src"), Path.Combine("tests") })
        {
            var directory = Path.Combine(repositoryRoot, relativeDirectory);
            foreach (var csprojPath in Directory.EnumerateFiles(directory, "*.csproj", SearchOption.AllDirectories))
            {
                projects.Add(ParseProject(csprojPath, repositoryRoot));
            }
        }

        return projects;
    }

    public static IReadOnlyList<string> LoadSolutionProjectPaths(string repositoryRoot)
    {
        var solutionPath = Path.Combine(repositoryRoot, "OBS-AI-Live-Assistant.slnx");
        return LoadSolutionProjectPathsFromDocument(XDocument.Load(solutionPath));
    }

    public static IReadOnlyList<string> LoadSolutionProjectPathsFromDocument(XDocument document)
    {
        return document.Root!
            .Descendants()
            .Where(element => element.Name.LocalName == "Project")
            .Select(element => (string?)element.Attribute("Path"))
            .Where(path => path is not null)
            .Select(path => NormalizePath(path!))
            .OrderBy(path => path)
            .ToList();
    }

    public static ProjectModel ParseProject(string absoluteCsprojPath, string repositoryRoot)
    {
        return ParseProjectDocument(absoluteCsprojPath, repositoryRoot, XDocument.Load(absoluteCsprojPath));
    }

    public static ProjectModel ParseProjectDocument(string absoluteCsprojPath, string repositoryRoot, XDocument document)
    {
        var relativePath = Path.GetRelativePath(repositoryRoot, absoluteCsprojPath);
        var normalizedRelativePath = NormalizePath(relativePath);
        var isSource = normalizedRelativePath.StartsWith("src" + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase);
        var csprojDirectory = Path.GetDirectoryName(absoluteCsprojPath)!;
        var referencedProjects = document
            .Descendants()
            .Where(element => element.Name.LocalName == "ProjectReference")
            .Select(element => (string?)element.Attribute("Include"))
            .Where(path => path is not null)
            .Select(path => Path.GetFileNameWithoutExtension(Path.GetFullPath(Path.Combine(csprojDirectory, path!))))
            .OrderBy(name => name)
            .ToList();
        var packageReferences = document
            .Descendants()
            .Where(element => element.Name.LocalName == "PackageReference")
            .Select(element => (string?)element.Attribute("Include"))
            .Where(name => name is not null)
            .Select(name => name!)
            .OrderBy(name => name)
            .ToList();

        return new ProjectModel
        {
            Name = Path.GetFileNameWithoutExtension(absoluteCsprojPath),
            AbsolutePath = absoluteCsprojPath,
            IsSource = isSource,
            ReferencedProjects = referencedProjects,
            PackageReferences = packageReferences,
        };
    }

    public static string NormalizePath(string path) =>
        path.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar);
}

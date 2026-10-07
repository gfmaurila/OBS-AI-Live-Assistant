namespace ObsAi.Architecture.Tests;

internal sealed class ProjectModel
{
    public required string Name { get; init; }

    public required string AbsolutePath { get; init; }

    public required bool IsSource { get; init; }

    public required IReadOnlyList<string> ReferencedProjects { get; init; }

    public required IReadOnlyList<string> PackageReferences { get; init; }
}

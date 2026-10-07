namespace ObsAi.Architecture.Tests;

internal static class DependencyRules
{
    public static readonly IReadOnlyList<string> ExpectedSourceProjects = new[]
    {
        "ObsAi.Domain",
        "ObsAi.Application",
        "ObsAi.Infrastructure",
        "ObsAi.Providers",
        "ObsAi.ObsIntegration",
        "ObsAi.Host",
    };

    public static readonly IReadOnlyDictionary<string, IReadOnlyCollection<string>> AllowedProjectReferences =
        new Dictionary<string, IReadOnlyCollection<string>>
        {
            ["ObsAi.Domain"] = Array.Empty<string>(),
            ["ObsAi.Application"] = new[] { "ObsAi.Domain" },
            ["ObsAi.Infrastructure"] = new[] { "ObsAi.Application" },
            ["ObsAi.Providers"] = new[] { "ObsAi.Application" },
            ["ObsAi.ObsIntegration"] = new[] { "ObsAi.Application" },
            ["ObsAi.Host"] = new[] { "ObsAi.Application", "ObsAi.Infrastructure", "ObsAi.Providers", "ObsAi.ObsIntegration" },
        };
}

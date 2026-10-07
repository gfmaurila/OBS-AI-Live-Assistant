using Xunit;

namespace ObsAi.Architecture.Tests;

public sealed class CompatibilityMatrixTests
{
    private static string RepositoryRoot =>
        RepositoryModel.FindRepositoryRoot(AppContext.BaseDirectory);

    private static string CompatibilityMatrixPath => Path.Combine(
        RepositoryRoot,
        "docs",
        "architecture",
        "compatibility",
        "COMPATIBILITY_MATRIX.md");

    private static string Adr010Path => Path.Combine(
        RepositoryRoot,
        "docs",
        "architecture",
        "decisions",
        "ADR-010-compatibility-strategy.md");

    private static string ReadCompatibilityMatrix() =>
        File.ReadAllText(CompatibilityMatrixPath);

    [Fact]
    public void CompatibilityMatrix_Exists_AtCanonicalLocation()
    {
        Assert.True(File.Exists(CompatibilityMatrixPath), "A matriz de compatibilidade canônica é obrigatória (RNF-024).");
    }

    [Fact]
    public void CompatibilityMatrix_DeclaresBaselineAndMinimumVersions()
    {
        var content = ReadCompatibilityMatrix();

        Assert.Contains("Baseline V1", content);
        Assert.Contains("Windows 11", content);
        Assert.Contains("Windows 10", content);
        Assert.Contains("32.1", content);
        Assert.Contains("10.0", content);
        Assert.Contains("2028-11", content);
    }

    [Fact]
    public void CompatibilityMatrix_RegistersTestedCombinationWithEvidence()
    {
        var content = ReadCompatibilityMatrix();

        Assert.Contains("2026-10-07", content);
        Assert.Contains("26200", content);
        Assert.Contains("32.1.2", content);
        Assert.Contains("10.0.12", content);
        Assert.Contains("prototypes/compat-sniff", content);
    }

    [Fact]
    public void CompatibilityMatrix_RequiresFailClosedForUnknownCombinations()
    {
        var content = ReadCompatibilityMatrix();

        Assert.Contains("fail-closed", content);
        Assert.Contains("unknown", content);
        Assert.Contains("unsupported", content);
    }

    [Fact]
    public void CompatibilityMatrix_DoesNotPromiseSupportWithoutTesting()
    {
        var content = ReadCompatibilityMatrix();

        Assert.Contains("não é promessa comercial", content);
        Assert.Contains("nunca declarar suporte sem teste", content);
    }

    [Fact]
    public void CompatibilityMatrix_EncaminhaPoliticaWindows10ENet10()
    {
        var content = ReadCompatibilityMatrix();

        Assert.Contains("19041", content);
        Assert.Contains("LTSC", content);
        Assert.Contains("RNF-024", content);
        Assert.Contains("RNF-020", content);
        Assert.Contains("antes do release", content);
    }

    [Fact]
    public void CompatibilityMatrix_TraceabilityReferencesRequirementsSecurityAndAdrs()
    {
        var content = ReadCompatibilityMatrix();

        foreach (var token in new[] { "RF-032", "RNF-019", "RNF-024", "RNF-025", "SEC-027", "SEC-029", "SEC-032", "ADR-010", "RES-022", "RES-027", "CON-001", "CON-002" })
        {
            Assert.Contains(token, content);
        }
    }

    [Fact]
    public void Adr010_SolutionPublishesAcceptedStatusWithValidationRecord()
    {
        var adr = File.ReadAllText(Adr010Path);
        var decisionMap = File.ReadAllText(Path.Combine(RepositoryRoot, "docs", "architecture", "decisions", "README.md"));

        Assert.Contains("**Status:** ACCEPTED", adr);
        Assert.Contains("Validação executada (TASK-003)", adr);
        Assert.Contains("ADR-010", decisionMap);
        Assert.Contains("| ACCEPTED |", decisionMap);
        Assert.Contains("8 `ACCEPTED`", decisionMap);
    }

    [Fact]
    public void CompatibilityPrototype_ExistsAsIndependentEvidenceTooling()
    {
        var csprojPath = Path.Combine(RepositoryRoot, "prototypes", "compat-sniff", "CompatibilityProbe.csproj");
        var programPath = Path.Combine(RepositoryRoot, "prototypes", "compat-sniff", "Program.cs");

        Assert.True(File.Exists(csprojPath), "O protótipo de evidência precisa existir (OBS Compatibility Gate / smoke).");
        Assert.True(File.Exists(programPath), "O protótipo de evidência precisa ter lógica em Program.cs.");

        var csproj = File.ReadAllText(csprojPath);
        Assert.DoesNotContain("PackageReference", csproj);

        var registeredProjects = RepositoryModel.LoadSolutionProjectPaths(RepositoryRoot);
        Assert.DoesNotContain(
            registeredProjects,
            path => path.Contains("prototypes", StringComparison.OrdinalIgnoreCase));
    }
}

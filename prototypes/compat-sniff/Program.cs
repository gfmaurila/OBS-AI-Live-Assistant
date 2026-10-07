using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.Json;

const int Windows10MinimumBuild = 19041;
const int Windows11MinimumBuild = 22000;
const int NetMajor = 10;
const int ObsMajor = 32;
const int ObsMinimumMinor = 1;

var os = ProbeOperatingSystem();
var runtime = ProbeRuntime();
var obs = ProbeObs();

var osClassification = ClassifyOperatingSystem(os);
var runtimeClassification = ClassifyRuntime(runtime);
var obsClassification = ClassifyObs(obs);

var verdict = Resolve(osClassification, runtimeClassification, obsClassification);

var result = new ProbeResult(
    Label(osClassification),
    Label(runtimeClassification),
    Label(obsClassification),
    Label(verdict),
    os,
    runtime,
    obs);

var jsonOptions = new JsonSerializerOptions
{
    WriteIndented = true,
};
Console.WriteLine(JsonSerializer.Serialize(result, jsonOptions));

return verdict == Classification.Supported ? 0 : 1;

static OsProbe ProbeOperatingSystem()
{
    var version = Environment.OSVersion.Version;
    return new OsProbe(
        Environment.OSVersion.Platform.ToString(),
        OperatingSystem.IsWindows(),
        Environment.Is64BitOperatingSystem,
        RuntimeInformation.OSArchitecture.ToString(),
        Environment.OSVersion.VersionString,
        version.Major,
        version.Minor,
        version.Build,
        OperatingSystem.IsWindowsVersionAtLeast(10, 0, Windows11MinimumBuild));
}

static RuntimeProbe ProbeRuntime()
{
    var assemblyVersion = Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
    return new RuntimeProbe(RuntimeInformation.FrameworkDescription, Environment.Version, assemblyVersion);
}

static ObsProbe? ProbeObs()
{
    var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
    var candidates = new[]
    {
        Path.Combine(programFiles, "obs-studio", "bin", "64bit", "obs64.exe"),
        Path.Combine(programFiles, "obs-studio", "obs64.exe"),
    };

    foreach (var candidate in candidates)
    {
        if (!File.Exists(candidate))
        {
            continue;
        }

        var versionInfo = FileVersionInfo.GetVersionInfo(candidate);
        var obsVersion = ParseVersion(versionInfo.ProductVersion ?? versionInfo.FileVersion);
        var executableDirectory = Path.GetDirectoryName(candidate)!;
        var obsRoot = string.Equals(Path.GetFileName(executableDirectory), "64bit", StringComparison.OrdinalIgnoreCase)
            ? Path.GetDirectoryName(Path.GetDirectoryName(executableDirectory))!
            : executableDirectory;
        var websocketPluginPath = Path.Combine(obsRoot, "obs-plugins", "64bit", "obs-websocket.dll");

        return new ObsProbe(
            candidate,
            versionInfo.FileVersion,
            versionInfo.ProductVersion,
            obsVersion,
            File.Exists(websocketPluginPath),
            websocketPluginPath);
    }

    return null;
}

static System.Version ParseVersion(string? text)
{
    if (string.IsNullOrWhiteSpace(text))
    {
        return new System.Version(0, 0);
    }

    var numeric = new string(text.Where(c => char.IsAsciiDigit(c) || c == '.').ToArray());
    return System.Version.TryParse(numeric, out var parsed) ? parsed : new System.Version(0, 0);
}

static Classification ClassifyOperatingSystem(OsProbe os)
{
    if (!os.IsWindows || !os.Is64Bit)
    {
        return Classification.Unsupported;
    }

    if (os.Build >= Windows11MinimumBuild)
    {
        return Classification.Supported;
    }

    if (os.Build is >= Windows10MinimumBuild and < Windows11MinimumBuild)
    {
        return Classification.Unknown;
    }

    return Classification.Unsupported;
}

static Classification ClassifyRuntime(RuntimeProbe runtime) =>
    runtime.Version.Major == NetMajor
        ? Classification.Supported
        : runtime.Version.Major < NetMajor
            ? Classification.Unsupported
            : Classification.Unknown;

static Classification ClassifyObs(ObsProbe? obs)
{
    if (obs is null)
    {
        return Classification.Unknown;
    }

    if (obs.Version.Major == ObsMajor && obs.Version.Minor >= ObsMinimumMinor)
    {
        return Classification.Supported;
    }

    if (obs.Version.Major == ObsMajor)
    {
        return Classification.Unsupported;
    }

    return obs.Version.Major < ObsMajor
        ? Classification.Unsupported
        : Classification.Unknown;
}

static Classification Resolve(Classification os, Classification runtime, Classification obs)
{
    var classifications = new[] { os, runtime, obs };
    if (classifications.Any(static c => c == Classification.Unsupported))
    {
        return Classification.Unsupported;
    }

    return classifications.Any(static c => c == Classification.Unknown)
        ? Classification.Unknown
        : Classification.Supported;
}

static string Label(Classification classification) =>
    classification switch
    {
        Classification.Supported => "supported",
        Classification.Unknown => "unknown",
        _ => "unsupported",
    };

internal enum Classification
{
    Supported,
    Unknown,
    Unsupported,
}

internal sealed record OsProbe(
    string Platform,
    bool IsWindows,
    bool Is64Bit,
    string OsArchitecture,
    string VersionText,
    int Major,
    int Minor,
    int Build,
    bool IsWindows11OrLater);

internal sealed record RuntimeProbe(
    string FrameworkDescription,
    System.Version Version,
    string? AssemblyInformationalVersion);

internal sealed record ObsProbe(
    string ExecutablePath,
    string? FileVersion,
    string? ProductVersion,
    System.Version Version,
    bool WebsocketPluginPresent,
    string WebsocketPluginPath);

internal sealed record ProbeResult(
    string OS,
    string Runtime,
    string OBS,
    string Verdict,
    OsProbe OSDetails,
    RuntimeProbe RuntimeDetails,
    ObsProbe? OBSDetails);

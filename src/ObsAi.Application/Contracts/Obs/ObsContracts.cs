using ObsAi.Application.Contracts.Common;

namespace ObsAi.Application.Contracts.Obs;

public enum ObsConnectionState
{
    Disconnected,
    Connecting,
    Connected,
    Degraded,
}

public sealed record ObsStatusSnapshot(
    ObsConnectionState ConnectionState,
    IReadOnlySet<string> AvailableCapabilities);

public sealed record ObsTextOutput(
    RequestContext Context,
    string DestinationReference,
    string ApprovedText);

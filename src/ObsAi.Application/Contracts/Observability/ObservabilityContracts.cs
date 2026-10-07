namespace ObsAi.Application.Contracts.Observability;

public enum OperationalSeverity
{
    Trace,
    Information,
    Warning,
    Error,
    Critical,
}

public enum ComponentHealthState
{
    Healthy,
    Degraded,
    Disconnected,
    Saturated,
    Failed,
}

public sealed record OperationalEvent(
    int EventId,
    OperationalSeverity Severity,
    Guid? CorrelationId,
    string SafeMessage);

public sealed record ComponentHealth(
    string Component,
    ComponentHealthState State,
    string SafeSummary);

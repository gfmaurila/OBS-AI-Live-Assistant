namespace ObsAi.Application.Resilience;

/// <summary>
/// Declares whether repeating an operation is explicitly known not to duplicate output, cost or
/// another externally visible side effect.
/// </summary>
public enum OperationIdempotency
{
    NonIdempotent,
    Idempotent,
}

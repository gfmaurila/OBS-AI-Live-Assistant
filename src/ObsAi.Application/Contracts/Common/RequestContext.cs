namespace ObsAi.Application.Contracts.Common;

/// <summary>
/// Carries non-sensitive correlation and session identity across application boundaries.
/// </summary>
public sealed record RequestContext(Guid CorrelationId, Guid SessionId, DateTimeOffset? DeadlineUtc);

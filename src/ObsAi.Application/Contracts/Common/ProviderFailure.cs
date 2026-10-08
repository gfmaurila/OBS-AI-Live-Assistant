namespace ObsAi.Application.Contracts.Common;

/// <summary>
/// A vendor-neutral failure safe to cross the application boundary.
/// </summary>
public sealed record ProviderFailure(
    ProviderFailureCode Code,
    string SafeMessage,
    bool IsTransient = false,
    TimeSpan? RetryAfter = null);

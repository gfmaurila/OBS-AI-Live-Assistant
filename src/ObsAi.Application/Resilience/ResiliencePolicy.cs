using System.Collections.ObjectModel;

namespace ObsAi.Application.Resilience;

/// <summary>
/// Immutable, caller-configured timeout and retry limits. Product-specific values belong to
/// configuration and are intentionally not defined here.
/// </summary>
public sealed class ResiliencePolicy
{
    private static readonly TimeSpan MaximumTimerDuration =
        TimeSpan.FromMilliseconds(uint.MaxValue - 1d);

    private readonly ReadOnlyCollection<TimeSpan> retryDelays;

    private ResiliencePolicy(
        TimeSpan attemptTimeout,
        TimeSpan? totalTimeout,
        int maximumAttempts,
        ReadOnlyCollection<TimeSpan> retryDelays)
    {
        AttemptTimeout = attemptTimeout;
        TotalTimeout = totalTimeout;
        MaximumAttempts = maximumAttempts;
        this.retryDelays = retryDelays;
    }

    public TimeSpan AttemptTimeout { get; }

    public TimeSpan? TotalTimeout { get; }

    public int MaximumAttempts { get; }

    public IReadOnlyList<TimeSpan> RetryDelays => retryDelays;

    /// <summary>
    /// Creates a validated policy. The retry schedule must contain exactly one delay between each
    /// possible pair of attempts; callers may supply already-jittered values when required.
    /// </summary>
    public static ResiliencePolicy Create(
        TimeSpan attemptTimeout,
        TimeSpan? totalTimeout,
        int maximumAttempts,
        IReadOnlyList<TimeSpan> retryDelays)
    {
        ArgumentNullException.ThrowIfNull(retryDelays);

        if (attemptTimeout <= TimeSpan.Zero || attemptTimeout > MaximumTimerDuration)
        {
            throw new ArgumentOutOfRangeException(nameof(attemptTimeout), "Attempt timeout must be positive and supported by the runtime timer.");
        }

        if (totalTimeout is { } configuredTotal
            && (configuredTotal <= TimeSpan.Zero || configuredTotal > MaximumTimerDuration))
        {
            throw new ArgumentOutOfRangeException(nameof(totalTimeout), "Total timeout must be positive and supported by the runtime timer.");
        }

        if (maximumAttempts < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(maximumAttempts), "Maximum attempts must be at least 1.");
        }

        if (retryDelays.Count != maximumAttempts - 1)
        {
            throw new ArgumentException("Retry delays must contain exactly one entry between possible attempts.", nameof(retryDelays));
        }

        var copiedDelays = new TimeSpan[retryDelays.Count];
        for (int index = 0; index < retryDelays.Count; index++)
        {
            TimeSpan delay = retryDelays[index];
            if (delay < TimeSpan.Zero || delay > MaximumTimerDuration)
            {
                throw new ArgumentOutOfRangeException(nameof(retryDelays), "Retry delays must be non-negative and supported by the runtime timer.");
            }

            copiedDelays[index] = delay;
        }

        return new ResiliencePolicy(attemptTimeout, totalTimeout, maximumAttempts, Array.AsReadOnly(copiedDelays));
    }

    internal TimeSpan GetRetryDelay(int completedAttempt, TimeSpan? providerRetryAfter)
    {
        TimeSpan configured = retryDelays[completedAttempt - 1];
        return providerRetryAfter is { } providerDelay
            && providerDelay >= TimeSpan.Zero
            && providerDelay <= MaximumTimerDuration
            && providerDelay > configured
            ? providerDelay
            : configured;
    }
}

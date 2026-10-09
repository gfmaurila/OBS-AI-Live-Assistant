using ObsAi.Application.Queues;

namespace ObsAi.Application.Configuration;

/// <summary>Defines approved limits and defaults for one bounded pipeline queue.</summary>
public sealed record QueueConfigurationBounds
{
    private QueueConfigurationBounds(
        BoundedInt32Setting capacity,
        BoundedInt32Setting maxConcurrency,
        SaturationPolicy defaultSaturationPolicy)
    {
        Capacity = capacity;
        MaxConcurrency = maxConcurrency;
        DefaultSaturationPolicy = defaultSaturationPolicy;
    }

    public BoundedInt32Setting Capacity { get; }

    public BoundedInt32Setting MaxConcurrency { get; }

    public SaturationPolicy DefaultSaturationPolicy { get; }

    public static QueueConfigurationBounds Create(
        BoundedInt32Setting capacity,
        BoundedInt32Setting maxConcurrency,
        SaturationPolicy defaultSaturationPolicy)
    {
        ArgumentNullException.ThrowIfNull(capacity);
        ArgumentNullException.ThrowIfNull(maxConcurrency);

        if (capacity.Minimum < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(capacity), "Queue capacity must always remain positive.");
        }

        if (maxConcurrency.Minimum < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(maxConcurrency), "Queue concurrency must always remain positive.");
        }

        if (!Enum.IsDefined(defaultSaturationPolicy))
        {
            throw new ArgumentOutOfRangeException(nameof(defaultSaturationPolicy), "Unknown saturation policy.");
        }

        return new QueueConfigurationBounds(capacity, maxConcurrency, defaultSaturationPolicy);
    }
}

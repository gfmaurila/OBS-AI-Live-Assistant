namespace ObsAi.Unit.Tests;

internal sealed class ManualTimeProvider(DateTimeOffset initialUtcNow) : TimeProvider
{
    private readonly object sync = new();
    private readonly List<ManualTimer> timers = new();
    private DateTimeOffset utcNow = initialUtcNow;

    public override DateTimeOffset GetUtcNow()
    {
        lock (sync)
        {
            return utcNow;
        }
    }

    public override ITimer CreateTimer(
        TimerCallback callback,
        object? state,
        TimeSpan dueTime,
        TimeSpan period)
    {
        ArgumentNullException.ThrowIfNull(callback);

        lock (sync)
        {
            var timer = new ManualTimer(this, callback, state, utcNow + dueTime, period);
            timers.Add(timer);
            return timer;
        }
    }

    public void Advance(TimeSpan amount)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(amount, TimeSpan.Zero);

        List<ManualTimer> due;
        lock (sync)
        {
            utcNow += amount;
            due = timers.Where(timer => timer.IsDue(utcNow)).ToList();
        }

        foreach (ManualTimer timer in due)
        {
            timer.Fire(utcNow);
        }
    }

    private void Remove(ManualTimer timer)
    {
        lock (sync)
        {
            timers.Remove(timer);
        }
    }

    private sealed class ManualTimer(
        ManualTimeProvider owner,
        TimerCallback callback,
        object? state,
        DateTimeOffset initialDueAt,
        TimeSpan initialPeriod) : ITimer
    {
        private readonly ManualTimeProvider owner = owner;
        private readonly TimerCallback callback = callback;
        private readonly object? state = state;
        private DateTimeOffset dueAt = initialDueAt;
        private TimeSpan period = initialPeriod;
        private bool disposed;

        public bool Change(TimeSpan dueTime, TimeSpan newPeriod)
        {
            if (disposed)
            {
                return false;
            }

            dueAt = owner.GetUtcNow() + dueTime;
            period = newPeriod;
            return true;
        }

        public void Dispose()
        {
            disposed = true;
            owner.Remove(this);
        }

        public ValueTask DisposeAsync()
        {
            Dispose();
            return ValueTask.CompletedTask;
        }

        public bool IsDue(DateTimeOffset now) => !disposed && dueAt <= now;

        public void Fire(DateTimeOffset now)
        {
            if (disposed)
            {
                return;
            }

            if (period == Timeout.InfiniteTimeSpan)
            {
                Dispose();
            }
            else
            {
                dueAt = now + period;
            }

            callback(state);
        }
    }
}

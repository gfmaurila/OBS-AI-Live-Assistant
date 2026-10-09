using ObsAi.Application.Resilience;
using Xunit;

namespace ObsAi.Unit.Tests;

public sealed class ResiliencePolicyTests
{
    [Fact]
    public void Create_CopiesConfiguredValuesWithoutDefiningProductDefaults()
    {
        TimeSpan[] delays = [TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(3)];

        ResiliencePolicy policy = ResiliencePolicy.Create(
            TimeSpan.FromSeconds(5),
            TimeSpan.FromSeconds(20),
            3,
            delays);
        delays[0] = TimeSpan.FromDays(1);

        Assert.Equal(TimeSpan.FromSeconds(5), policy.AttemptTimeout);
        Assert.Equal(TimeSpan.FromSeconds(20), policy.TotalTimeout);
        Assert.Equal(3, policy.MaximumAttempts);
        Assert.Equal([TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(3)], policy.RetryDelays);
        Assert.False(policy.RetryDelays is TimeSpan[]);
        Assert.True(((IList<TimeSpan>)policy.RetryDelays).IsReadOnly);
        Assert.Throws<NotSupportedException>(() => ((IList<TimeSpan>)policy.RetryDelays)[0] = TimeSpan.Zero);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Create_RejectsNonPositiveAttemptTimeout(int ticks)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ResiliencePolicy.Create(TimeSpan.FromTicks(ticks), null, 1, []));
    }

    [Fact]
    public void Create_RejectsRetryScheduleThatDoesNotMatchAttemptLimit()
    {
        Assert.Throws<ArgumentException>(() =>
            ResiliencePolicy.Create(TimeSpan.FromSeconds(1), null, 3, [TimeSpan.Zero]));
    }

    [Fact]
    public void Create_RejectsNegativeRetryDelay()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ResiliencePolicy.Create(TimeSpan.FromSeconds(1), null, 2, [TimeSpan.FromTicks(-1)]));
    }

    [Fact]
    public void Create_RejectsDurationsUnsupportedByRuntimeTimer()
    {
        TimeSpan unsupported = TimeSpan.FromMilliseconds(uint.MaxValue);

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ResiliencePolicy.Create(unsupported, null, 1, []));
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ResiliencePolicy.Create(TimeSpan.FromSeconds(1), unsupported, 1, []));
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ResiliencePolicy.Create(TimeSpan.FromSeconds(1), null, 2, [unsupported]));
    }
}

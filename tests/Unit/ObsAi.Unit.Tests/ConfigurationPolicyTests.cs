using ObsAi.Application.Configuration;
using ObsAi.Application.Queues;
using Xunit;

namespace ObsAi.Unit.Tests;

public sealed class ConfigurationPolicyTests
{
    [Fact]
    public void BoundedSetting_AcceptsMinimumMaximumAndDefaultInsideRange()
    {
        var setting = BoundedInt32Setting.Create(0, 10, 5);

        Assert.Equal(0, setting.Minimum);
        Assert.Equal(10, setting.Maximum);
        Assert.Equal(5, setting.DefaultValue);
    }

    [Theory]
    [InlineData(-1, 10, 5)]
    [InlineData(10, 9, 9)]
    [InlineData(1, 10, 0)]
    [InlineData(1, 10, 11)]
    public void BoundedSetting_RejectsInvalidRanges(int minimum, int maximum, int defaultValue)
    {
        Assert.ThrowsAny<ArgumentOutOfRangeException>(() =>
            BoundedInt32Setting.Create(minimum, maximum, defaultValue));
    }

    [Fact]
    public void QueueBounds_RejectZeroAsApprovedMinimum()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            QueueConfigurationBounds.Create(
                BoundedInt32Setting.Create(0, 10, 1),
                BoundedInt32Setting.Create(1, 2, 1),
                SaturationPolicy.Reject));
    }

    [Fact]
    public void Policy_RequiresBoundsForEveryQueueKind()
    {
        var incomplete = new Dictionary<QueueKind, QueueConfigurationBounds>
        {
            [QueueKind.Request] = QueueConfigurationBounds.Create(
                BoundedInt32Setting.Create(1, 10, 2),
                BoundedInt32Setting.Create(1, 2, 1),
                SaturationPolicy.Reject),
        };

        Assert.Throws<ArgumentException>(() => ConfigurationPolicy.Create(
            2,
            2,
            10,
            ObsAi.Domain.Profiles.AssistantProfileLimits.Create(10, 10, 10, 10),
            BoundedInt32Setting.Create(0, 10, 0),
            BoundedInt32Setting.Create(0, 10, 0),
            BoundedInt32Setting.Create(1, 10, 1),
            BoundedInt32Setting.Create(1, 10, 1),
            BoundedInt32Setting.Create(1, 10, 1),
            incomplete,
            Array.Empty<ObsAi.Application.Contracts.Common.ProviderId>(),
            Array.Empty<ObsAi.Application.Contracts.Common.ProviderId>()));
    }
}

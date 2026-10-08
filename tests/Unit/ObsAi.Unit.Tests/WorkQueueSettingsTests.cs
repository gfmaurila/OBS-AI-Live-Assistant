using ObsAi.Application.Queues;
using Xunit;

namespace ObsAi.Unit.Tests;

public sealed class WorkQueueSettingsTests
{
    [Fact]
    public void Create_WithValidValues_ProducesReadOnlySettings()
    {
        var settings = WorkQueueSettings.Create(QueueKind.Tts, 4, 2, SaturationPolicy.DiscardOldest);

        Assert.Equal(QueueKind.Tts, settings.Kind);
        Assert.Equal(4, settings.Capacity);
        Assert.Equal(2, settings.MaxConcurrency);
        Assert.Equal(SaturationPolicy.DiscardOldest, settings.SaturationPolicy);
        Assert.DoesNotContain(settings.GetType().GetProperties(), property => property.CanWrite);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Create_WithNonPositiveCapacity_IsRejected(int capacity)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            WorkQueueSettings.Create(QueueKind.Request, capacity, 1, SaturationPolicy.Reject));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Create_WithNonPositiveConcurrency_IsRejected(int concurrency)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            WorkQueueSettings.Create(QueueKind.Request, 1, concurrency, SaturationPolicy.Reject));
    }

    [Fact]
    public void Create_WithUndefinedKind_IsRejected()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            WorkQueueSettings.Create((QueueKind)99, 1, 1, SaturationPolicy.Reject));
    }

    [Fact]
    public void Create_WithUndefinedSaturationPolicy_IsRejected()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            WorkQueueSettings.Create(QueueKind.Request, 1, 1, (SaturationPolicy)99));
    }

    [Fact]
    public void Settings_WithTheSameValues_AreEqualByRecordSemantics()
    {
        var first = WorkQueueSettings.Create(QueueKind.Request, 3, 1, SaturationPolicy.Reject);
        var second = WorkQueueSettings.Create(QueueKind.Request, 3, 1, SaturationPolicy.Reject);
        var different = WorkQueueSettings.Create(QueueKind.Request, 4, 1, SaturationPolicy.Reject);

        Assert.Equal(first, second);
        Assert.NotEqual(first, different);
    }
}

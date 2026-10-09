using ObsAi.Application.Configuration;
using Xunit;

namespace ObsAi.Unit.Tests;

public sealed class ConfigurationStateTests
{
    private static readonly IReadOnlyCollection<string> FirstTriggers = new[] { "!first" };
    private static readonly IReadOnlyCollection<string> SecondTriggers = new[] { "!second" };

    [Fact]
    public void Create_InvalidInitialConfigurationFailsClosed()
    {
        var invalid = new ConfigurationDraft(
            Array.Empty<ObsAi.Domain.Profiles.AssistantProfile>(),
            ObsAi.Domain.Profiles.AssistantProfileId.Create(ConfigurationTestData.PrimaryProfileValue),
            null,
            false,
            null,
            null,
            null,
            null,
            null);

        Assert.Throws<ArgumentException>(() =>
            ConfigurationState.Create(invalid, ConfigurationTestData.CreatePolicy()));
    }

    [Fact]
    public void TryApply_InvalidCandidatePreservesLastValidSnapshot()
    {
        var state = ConfigurationState.Create(
            ConfigurationTestData.CreateDraft(rateLimits: new RateLimitDraft(10, 5, 3)),
            ConfigurationTestData.CreatePolicy());
        var before = state.Current;

        var result = state.TryApply(
            ConfigurationTestData.CreateDraft(rateLimits: new RateLimitDraft(999, 5, 3)));

        Assert.False(result.WasApplied);
        Assert.Same(before, result.Configuration);
        Assert.Same(before, state.Current);
        Assert.Equal(10, state.Current.RateLimits.UserCooldownSeconds);
    }

    [Fact]
    public void TryApply_ValidCandidateReplacesSnapshotAtomically()
    {
        var state = ConfigurationState.Create(
            ConfigurationTestData.CreateDraft(rateLimits: new RateLimitDraft(10, 5, 3)),
            ConfigurationTestData.CreatePolicy());

        var result = state.TryApply(
            ConfigurationTestData.CreateDraft(rateLimits: new RateLimitDraft(20, 6, 4)));

        Assert.True(result.WasApplied);
        Assert.Same(result.Configuration, state.Current);
        Assert.Equal(20, state.Current.RateLimits.UserCooldownSeconds);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task IndependentStates_DoNotLeakConfigurationAcrossInstances()
    {
        var first = ConfigurationState.Create(ConfigurationTestData.CreateDraft(), ConfigurationTestData.CreatePolicy());
        var second = ConfigurationState.Create(ConfigurationTestData.CreateDraft(), ConfigurationTestData.CreatePolicy());

        await Task.WhenAll(
            Task.Run(() => first.TryApply(ConfigurationTestData.CreateDraft(triggers: FirstTriggers))),
            Task.Run(() => second.TryApply(ConfigurationTestData.CreateDraft(triggers: SecondTriggers))));

        Assert.Equal("!first", Assert.Single(first.Current.EnabledTriggers));
        Assert.Equal("!second", Assert.Single(second.Current.EnabledTriggers));
    }

    [Fact]
    public async Task ConcurrentValidAndInvalidUpdates_NeverPublishInvalidSnapshot()
    {
        var state = ConfigurationState.Create(ConfigurationTestData.CreateDraft(), ConfigurationTestData.CreatePolicy());

        var results = await Task.WhenAll(
            Enumerable.Range(1, 20).Select(index => Task.Run(() =>
                state.TryApply(ConfigurationTestData.CreateDraft(
                    rateLimits: new RateLimitDraft(index % 2 == 0 ? index : 999, 5, 3))))));

        Assert.Contains(results, result => result.WasApplied);
        Assert.Contains(results, result => !result.WasApplied);
        Assert.InRange(state.Current.RateLimits.UserCooldownSeconds, 0, 300);
    }
}

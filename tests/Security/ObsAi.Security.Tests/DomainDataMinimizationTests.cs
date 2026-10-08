using ObsAi.Domain.Context;
using ObsAi.Domain.Profiles;
using ObsAi.Domain.Sessions;
using Xunit;

namespace ObsAi.Security.Tests;

public sealed class DomainDataMinimizationTests
{
    [Fact]
    public void LiveContext_RejectsUnallowlistedFields()
    {
        var sessionId = SessionId.Create(Guid.Parse("73aa2e39-9fbf-4e9e-9cee-7ba782652260"));
        var fields = new Dictionary<LiveContextField, string> { [(LiveContextField)int.MaxValue] = "untrusted" };

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            LiveContext.Create(sessionId, fields, LiveContextLimits.Create(1, 32, 32)));
    }

    [Fact]
    public void EndingSession_RevokesAndErasesEphemeralContext()
    {
        var sessionId = SessionId.Create(Guid.Parse("b4d100c5-bbef-49c4-ae11-aa06580353dc"));
        var context = LiveContext.Create(
            sessionId,
            new Dictionary<LiveContextField, string> { [LiveContextField.CustomContext] = "ephemeral" },
            LiveContextLimits.Create(1, 32, 32));
        var cachedView = context.Entries;
        var session = AssistantSession.Start(
            sessionId,
            AssistantProfileId.Create(Guid.Parse("d762e6ee-b808-4a7a-bac4-f80df38effe2")),
            context,
            new DateTimeOffset(2026, 10, 8, 12, 0, 0, TimeSpan.Zero));

        session.End(new DateTimeOffset(2026, 10, 8, 13, 0, 0, TimeSpan.Zero));

        Assert.Empty(cachedView);
        Assert.Throws<InvalidOperationException>(() => context.TryGetValue(LiveContextField.CustomContext, out _));
    }

    [Fact]
    public void DomainModel_ExposesNoSecretOrPersistentMemorySurface()
    {
        var forbiddenFragments = new[] { "Secret", "Credential", "Password", "Token", "ApiKey", "PersistentMemory" };
        var exportedSurface = typeof(AssistantSession).Assembly.ExportedTypes
            .SelectMany(type => new[] { type.Name }.Concat(type.GetProperties().Select(property => property.Name)));

        Assert.DoesNotContain(exportedSurface, name =>
            forbiddenFragments.Any(fragment => name.Contains(fragment, StringComparison.OrdinalIgnoreCase)));
    }
}

using ObsAi.Domain.Context;
using ObsAi.Domain.Sessions;
using Xunit;

namespace ObsAi.Unit.Tests;

public sealed class LiveContextTests
{
    private static readonly SessionId SessionId =
        SessionId.Create(Guid.Parse("278199e5-d999-4f0d-912c-f2120eb5c534"));

    [Fact]
    public void Create_KeepsOnlyExplicitAllowlistedFieldsAndNormalizesValues()
    {
        var entries = new Dictionary<LiveContextField, string>
        {
            [LiveContextField.LiveTitle] = "  Live de testes  ",
            [LiveContextField.Game] = "  Jogo seguro  ",
        };

        var context = LiveContext.Create(SessionId, entries, LiveContextLimits.Create(2, 32, 64));

        Assert.Equal(SessionId, context.SessionId);
        Assert.Equal("Live de testes", context.Entries[LiveContextField.LiveTitle]);
        Assert.Equal("Jogo seguro", context.Entries[LiveContextField.Game]);
        Assert.False(context.IsCleared);
    }

    [Fact]
    public void Create_RejectsFieldsOutsideAllowlist()
    {
        var entries = new Dictionary<LiveContextField, string> { [(LiveContextField)999] = "value" };

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            LiveContext.Create(SessionId, entries, LiveContextLimits.Create(1, 16, 16)));
    }

    [Fact]
    public void Create_RejectsFieldCountValueLengthAndTotalLengthOverflow()
    {
        var twoEntries = new Dictionary<LiveContextField, string>
        {
            [LiveContextField.LiveTitle] = "1234",
            [LiveContextField.Game] = "5678",
        };

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            LiveContext.Create(SessionId, twoEntries, LiveContextLimits.Create(1, 8, 16)));
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            LiveContext.Create(SessionId, twoEntries, LiveContextLimits.Create(2, 3, 16)));
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            LiveContext.Create(SessionId, twoEntries, LiveContextLimits.Create(2, 8, 7)));
    }

    [Fact]
    public void Create_RejectsEmptyAndControlCharacterValues()
    {
        var empty = new Dictionary<LiveContextField, string> { [LiveContextField.Game] = " " };
        var control = new Dictionary<LiveContextField, string> { [LiveContextField.Game] = "game\nother" };
        var limits = LiveContextLimits.Create(1, 16, 16);

        Assert.Throws<ArgumentException>(() => LiveContext.Create(SessionId, empty, limits));
        Assert.Throws<ArgumentOutOfRangeException>(() => LiveContext.Create(SessionId, control, limits));
    }

    [Fact]
    public void Limits_RejectInvalidBoundsAndBoundsAboveAllowlistSize()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => LiveContextLimits.Create(0, 1, 1));
        Assert.Throws<ArgumentOutOfRangeException>(() => LiveContextLimits.Create(1, 0, 1));
        Assert.Throws<ArgumentOutOfRangeException>(() => LiveContextLimits.Create(1, 1, 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => LiveContextLimits.Create(7, 1, 1));
    }
}

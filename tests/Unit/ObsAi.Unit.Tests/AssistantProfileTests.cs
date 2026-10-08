using ObsAi.Domain.Profiles;
using Xunit;

namespace ObsAi.Unit.Tests;

public sealed class AssistantProfileTests
{
    private static readonly AssistantProfileLimits Limits = AssistantProfileLimits.Create(32, 64, 64, 128);

    [Fact]
    public void Create_NormalizesAllApprovedProfileFields()
    {
        var id = AssistantProfileId.Create(Guid.Parse("8a04df23-c868-473a-8b50-d67e5abfca2c"));

        var profile = AssistantProfile.Create(
            id,
            "  Principal  ",
            "  Prestativo  ",
            "  Conciso  ",
            "  Responder em português.  ",
            Limits);

        Assert.Equal(id, profile.Id);
        Assert.Equal("Principal", profile.Name);
        Assert.Equal("Prestativo", profile.Personality);
        Assert.Equal("Conciso", profile.ResponseStyle);
        Assert.Equal("Responder em português.", profile.BehaviorInstructions);
    }

    [Fact]
    public void Update_PreservesIdentityAndDoesNotMutatePreviousProfile()
    {
        var id = AssistantProfileId.Create(Guid.Parse("e66fc96f-569b-4e54-8320-c589845cb34d"));
        var original = AssistantProfile.Create(id, "Principal", "Calmo", "Curto", "Seja claro.", Limits);

        var updated = original.Update("Alternativo", "Animado", "Detalhado", "Explique exemplos.", Limits);

        Assert.Equal(id, updated.Id);
        Assert.Equal("Alternativo", updated.Name);
        Assert.Equal("Principal", original.Name);
    }

    [Fact]
    public void Create_RejectsMissingOversizedOrControlCharacterText()
    {
        var id = AssistantProfileId.Create(Guid.Parse("9962e9ba-6ad0-4d25-87c7-cbecd58427d1"));

        Assert.Throws<ArgumentException>(() => AssistantProfile.Create(id, "", "Calmo", "Curto", "Seja claro.", Limits));
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            AssistantProfile.Create(id, new string('a', 33), "Calmo", "Curto", "Seja claro.", Limits));
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            AssistantProfile.Create(id, "Principal", "Calmo\nOculto", "Curto", "Seja claro.", Limits));
    }

    [Fact]
    public void Limits_RejectNonPositiveValues()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => AssistantProfileLimits.Create(0, 1, 1, 1));
        Assert.Throws<ArgumentOutOfRangeException>(() => AssistantProfileLimits.Create(1, -1, 1, 1));
    }
}

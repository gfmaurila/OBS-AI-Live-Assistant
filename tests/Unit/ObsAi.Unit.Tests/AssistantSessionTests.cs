using ObsAi.Domain.Context;
using ObsAi.Domain.Profiles;
using ObsAi.Domain.Sessions;
using Xunit;

namespace ObsAi.Unit.Tests;

public sealed class AssistantSessionTests
{
    private static readonly DateTimeOffset StartTime = new(2026, 10, 8, 12, 0, 0, TimeSpan.Zero);

    [Fact]
    public void Start_CreatesActiveSessionWithMatchingContextAndProfile()
    {
        var sessionId = CreateSessionId("82ae9633-aa0b-43dc-ac8c-56af58692a47");
        var profileId = CreateProfileId("768e5d8c-1ad2-4368-8745-b7a3f6db6e0f");
        var context = CreateContext(sessionId);

        var session = AssistantSession.Start(sessionId, profileId, context, StartTime);

        Assert.Equal(AssistantSessionState.Active, session.State);
        Assert.Equal(sessionId, session.Id);
        Assert.Equal(profileId, session.ProfileId);
        Assert.Same(context, session.LiveContext);
        Assert.Null(session.EndedAtUtc);
    }

    [Fact]
    public void Start_RejectsContextFromAnotherSession()
    {
        var sessionId = CreateSessionId("1e43c76c-3403-4604-971f-fe0445a19264");
        var otherSessionId = CreateSessionId("61ef6a82-6e2c-4786-a833-73faab05025e");

        Assert.Throws<ArgumentException>(() => AssistantSession.Start(
            sessionId,
            CreateProfileId("f542727e-d71f-48ca-a0d6-67464fba3c2d"),
            CreateContext(otherSessionId),
            StartTime));
    }

    [Fact]
    public void End_ClearsContextAndPreventsSessionReuse()
    {
        var sessionId = CreateSessionId("576f7032-2530-4c0a-a7cf-203e06cf6ad6");
        var context = CreateContext(sessionId);
        var cachedEntries = context.Entries;
        var session = AssistantSession.Start(
            sessionId,
            CreateProfileId("9de04029-3f40-4e50-978c-ed330ade2c3b"),
            context,
            StartTime);
        var endTime = StartTime.AddHours(2);

        session.End(endTime);

        Assert.Equal(AssistantSessionState.Ended, session.State);
        Assert.Equal(endTime, session.EndedAtUtc);
        Assert.True(context.IsCleared);
        Assert.Empty(cachedEntries);
        Assert.Throws<InvalidOperationException>(() => session.LiveContext);
        Assert.Throws<InvalidOperationException>(() => context.Entries);
        Assert.Throws<InvalidOperationException>(() => session.End(endTime));
        Assert.Throws<ArgumentException>(() => AssistantSession.Start(
            sessionId,
            CreateProfileId("0e6906a9-31f0-48d7-a440-dc74be06f4ab"),
            context,
            endTime));
    }

    [Fact]
    public void Lifecycle_RejectsNonUtcAndChronologicallyInvalidTimestamps()
    {
        var sessionId = CreateSessionId("537ba194-9200-454f-b72f-1070936cc884");
        var profileId = CreateProfileId("23670851-cbb6-48b1-8a50-0b14e362cd5e");

        Assert.Throws<ArgumentException>(() => AssistantSession.Start(
            sessionId,
            profileId,
            CreateContext(sessionId),
            StartTime.ToOffset(TimeSpan.FromHours(-3))));

        var session = AssistantSession.Start(sessionId, profileId, CreateContext(sessionId), StartTime);
        Assert.Throws<ArgumentOutOfRangeException>(() => session.End(StartTime.AddTicks(-1)));
        Assert.Throws<ArgumentException>(() => session.End(StartTime.ToOffset(TimeSpan.FromHours(-3))));
        Assert.Equal(AssistantSessionState.Active, session.State);
    }

    [Fact]
    public void Identifiers_RejectEmptyValues()
    {
        Assert.Throws<ArgumentException>(() => SessionId.Create(Guid.Empty));
        Assert.Throws<ArgumentException>(() => AssistantProfileId.Create(Guid.Empty));
    }

    private static SessionId CreateSessionId(string value) => SessionId.Create(Guid.Parse(value));

    private static AssistantProfileId CreateProfileId(string value) => AssistantProfileId.Create(Guid.Parse(value));

    private static LiveContext CreateContext(SessionId sessionId) =>
        LiveContext.Create(
            sessionId,
            new Dictionary<LiveContextField, string> { [LiveContextField.LiveTitle] = "Live atual" },
            LiveContextLimits.Create(1, 32, 32));
}

using ObsAi.Domain.Context;
using ObsAi.Domain.Profiles;

namespace ObsAi.Domain.Sessions;

/// <summary>
/// Delimits the profile and ephemeral context that belong to one live session.
/// </summary>
public sealed class AssistantSession
{
    private readonly LiveContext liveContext;

    private AssistantSession(
        SessionId id,
        AssistantProfileId profileId,
        LiveContext liveContext,
        DateTimeOffset startedAtUtc)
    {
        Id = id;
        ProfileId = profileId;
        this.liveContext = liveContext;
        StartedAtUtc = startedAtUtc;
        State = AssistantSessionState.Active;
    }

    public SessionId Id { get; }

    public AssistantProfileId ProfileId { get; }

    public DateTimeOffset StartedAtUtc { get; }

    public DateTimeOffset? EndedAtUtc { get; private set; }

    public AssistantSessionState State { get; private set; }

    public LiveContext LiveContext
    {
        get
        {
            EnsureActive();
            return liveContext;
        }
    }

    public static AssistantSession Start(
        SessionId id,
        AssistantProfileId profileId,
        LiveContext liveContext,
        DateTimeOffset startedAtUtc)
    {
        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNull(profileId);
        ArgumentNullException.ThrowIfNull(liveContext);

        if (liveContext.SessionId != id)
        {
            throw new ArgumentException("Live context must belong to the session being started.", nameof(liveContext));
        }

        if (liveContext.IsCleared)
        {
            throw new ArgumentException("Cleared live context cannot be reused by another session.", nameof(liveContext));
        }

        EnsureUtc(startedAtUtc, nameof(startedAtUtc));
        return new AssistantSession(id, profileId, liveContext, startedAtUtc);
    }

    public void End(DateTimeOffset endedAtUtc)
    {
        EnsureActive();
        EnsureUtc(endedAtUtc, nameof(endedAtUtc));
        if (endedAtUtc < StartedAtUtc)
        {
            throw new ArgumentOutOfRangeException(nameof(endedAtUtc), "Session cannot end before it starts.");
        }

        liveContext.Clear();
        EndedAtUtc = endedAtUtc;
        State = AssistantSessionState.Ended;
    }

    private static void EnsureUtc(DateTimeOffset value, string parameterName)
    {
        if (value.Offset != TimeSpan.Zero)
        {
            throw new ArgumentException("Session timestamps must use UTC.", parameterName);
        }
    }

    private void EnsureActive()
    {
        if (State != AssistantSessionState.Active)
        {
            throw new InvalidOperationException("Ended sessions cannot be reused.");
        }
    }
}

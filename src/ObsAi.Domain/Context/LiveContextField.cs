namespace ObsAi.Domain.Context;

/// <summary>
/// Enumerates the only context fields accepted by the TASK-006 domain baseline.
/// </summary>
public enum LiveContextField
{
    Streamer = 1,
    LiveTitle = 2,
    Platform = 3,
    Game = 4,
    ObsScene = 5,
    CustomContext = 6,
}

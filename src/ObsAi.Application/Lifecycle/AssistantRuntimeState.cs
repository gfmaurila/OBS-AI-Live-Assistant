namespace ObsAi.Application.Lifecycle;

/// <summary>
/// Explicit runtime states of the Assistant Core session lifecycle.
/// </summary>
public enum AssistantRuntimeState
{
    /// <summary>No session is active and no operation can be admitted or committed.</summary>
    Stopped = 1,

    /// <summary>A session is active and new operations are admitted.</summary>
    Running = 2,

    /// <summary>A session is active, new operations are rejected and in-flight work is not cancelled.</summary>
    Paused = 3,

    /// <summary>Ordered shutdown in progress: admission is already closed while work is being cancelled.</summary>
    Stopping = 4,
}

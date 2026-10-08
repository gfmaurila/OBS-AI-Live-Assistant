namespace ObsAi.Application.Queues;

/// <summary>
/// Identifies the pipeline stage a bounded work queue belongs to. Each stage owns its own queue
/// instance, so a slow or failed stage never blocks the others (ADR-003).
/// </summary>
public enum QueueKind
{
    /// <summary>Approved requests waiting to be processed (RF-013).</summary>
    Request,

    /// <summary>Approved responses waiting for text and voice outputs (RF-021).</summary>
    Response,

    /// <summary>Approved speech syntheses waiting to be played (RF-025).</summary>
    Tts,
}

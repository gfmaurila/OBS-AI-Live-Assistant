using ObsAi.Application.Contracts.Common;

namespace ObsAi.Application.Contracts.Ai;

public enum AiMessageRole
{
    User,
    Assistant,
}

public enum AiFinishReason
{
    Completed,
    OutputLimit,
    ContentRejected,
    Unknown,
}

public sealed record AiMessage(AiMessageRole Role, string Content);

public sealed record AiUsage(long? InputUnits, long? OutputUnits);

public sealed record AiRequest(
    RequestContext Context,
    CredentialReference CredentialReference,
    string ModelReference,
    string SystemInstructions,
    string UntrustedInput,
    IReadOnlyList<AiMessage> History,
    int MaximumOutputCharacters);

public sealed record AiResponse(
    string UntrustedText,
    AiFinishReason FinishReason,
    AiUsage? Usage,
    string? ExternalRequestReference);

public sealed record AiTextChunk(string UntrustedText, bool IsFinal);

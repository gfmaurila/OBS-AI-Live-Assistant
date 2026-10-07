using ObsAi.Application.Contracts.Common;

namespace ObsAi.Application.Contracts.Tts;

public sealed record TtsRequest(
    RequestContext Context,
    CredentialReference? CredentialReference,
    string ApprovedText,
    string VoiceReference,
    string Locale,
    string RequestedMediaType);

public sealed record TtsAudio(
    ReadOnlyMemory<byte> Content,
    string MediaType,
    TimeSpan? Duration);

public sealed record TtsAudioChunk(ReadOnlyMemory<byte> Content, bool IsFinal);

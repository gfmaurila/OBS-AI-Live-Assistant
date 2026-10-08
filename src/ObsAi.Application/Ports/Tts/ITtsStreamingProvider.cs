using ObsAi.Application.Contracts.Common;
using ObsAi.Application.Contracts.Tts;

namespace ObsAi.Application.Ports.Tts;

/// <summary>
/// Optional capability implemented only when audio streaming is actually supported.
/// </summary>
public interface ITtsStreamingProvider
{
    IAsyncEnumerable<ProviderResult<TtsAudioChunk>> StreamAsync(
        TtsRequest request,
        CancellationToken cancellationToken);
}

using ObsAi.Application.Contracts.Common;
using ObsAi.Application.Contracts.Tts;

namespace ObsAi.Application.Ports.Tts;

public interface ITtsProvider
{
    ProviderId Id { get; }

    ValueTask<ProviderResult<TtsAudio>> SynthesizeAsync(
        TtsRequest request,
        CancellationToken cancellationToken);
}

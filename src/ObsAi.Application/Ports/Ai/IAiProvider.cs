using ObsAi.Application.Contracts.Ai;
using ObsAi.Application.Contracts.Common;

namespace ObsAi.Application.Ports.Ai;

public interface IAiProvider
{
    ProviderId Id { get; }

    ValueTask<ProviderResult<AiResponse>> GenerateAsync(
        AiRequest request,
        CancellationToken cancellationToken);
}

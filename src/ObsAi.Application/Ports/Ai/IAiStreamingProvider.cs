using ObsAi.Application.Contracts.Ai;
using ObsAi.Application.Contracts.Common;

namespace ObsAi.Application.Ports.Ai;

/// <summary>
/// Optional capability implemented only when incremental output is actually supported.
/// </summary>
public interface IAiStreamingProvider
{
    IAsyncEnumerable<ProviderResult<AiTextChunk>> StreamAsync(
        AiRequest request,
        CancellationToken cancellationToken);
}

using ObsAi.Application.Contracts.Common;
using ObsAi.Application.Contracts.Obs;

namespace ObsAi.Application.Ports.Obs;

/// <summary>
/// Narrow output port. It does not grant generic OBS command authority.
/// </summary>
public interface IObsTextOutput
{
    ValueTask<ProviderResult<string>> PublishAsync(
        ObsTextOutput output,
        CancellationToken cancellationToken);
}

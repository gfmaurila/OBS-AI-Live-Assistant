using ObsAi.Application.Contracts.Obs;

namespace ObsAi.Application.Ports.Obs;

public interface IObsStatusReader
{
    ValueTask<ObsStatusSnapshot> GetStatusAsync(CancellationToken cancellationToken);
}

using ObsAi.Application.Contracts.Observability;

namespace ObsAi.Application.Ports.Observability;

public interface IHealthReporter
{
    ValueTask ReportAsync(ComponentHealth health, CancellationToken cancellationToken);
}

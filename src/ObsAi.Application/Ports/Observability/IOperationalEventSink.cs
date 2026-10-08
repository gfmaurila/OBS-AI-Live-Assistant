using ObsAi.Application.Contracts.Observability;

namespace ObsAi.Application.Ports.Observability;

public interface IOperationalEventSink
{
    ValueTask WriteAsync(OperationalEvent operationalEvent, CancellationToken cancellationToken);
}

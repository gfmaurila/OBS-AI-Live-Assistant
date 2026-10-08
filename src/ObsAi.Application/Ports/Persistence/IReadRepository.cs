namespace ObsAi.Application.Ports.Persistence;

/// <summary>
/// Minimal read port for an application-owned aggregate.
/// </summary>
public interface IReadRepository<TEntity, in TIdentifier>
    where TEntity : class
{
    ValueTask<TEntity?> GetAsync(TIdentifier identifier, CancellationToken cancellationToken);
}

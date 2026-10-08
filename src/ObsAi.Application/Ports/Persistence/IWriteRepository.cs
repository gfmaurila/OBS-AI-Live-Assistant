namespace ObsAi.Application.Ports.Persistence;

/// <summary>
/// Minimal write port; transaction and single-writer behavior belong to the adapter.
/// </summary>
public interface IWriteRepository<TEntity, in TIdentifier>
    where TEntity : class
{
    ValueTask SaveAsync(TEntity entity, CancellationToken cancellationToken);

    ValueTask DeleteAsync(TIdentifier identifier, CancellationToken cancellationToken);
}

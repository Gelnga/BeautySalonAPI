using Base.Contracts.Domain;

namespace Base.Contracts.DAL;
/// <summary>
/// Every method, where a public alternative can exists, has a corresponding overload, which requires a TKey userId type
/// input parameter
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IEntityRepository<TEntity> : IEntityRepository<TEntity, Guid>
    where TEntity : class, IDomainEntityId
{
}

public interface IEntityRepository<TEntity, TKey> : IPublicEntityRepository<TEntity, TKey>
    where TEntity : class, IDomainEntityId<TKey>
    where TKey : IEquatable<TKey>
{
    // sync
    TEntity Remove(TKey id, TKey userId);
    TEntity? FirstOrDefault(TKey id, TKey userId, bool noTracking = true);
    IEnumerable<TEntity> GetAll(TKey userId, bool noTracking = true);

    // async
    Task<TEntity?> FirstOrDefaultAsync(TKey id, TKey userId, bool noTracking = true);
    Task<IEnumerable<TEntity>> GetAllAsync(TKey userId, bool noTracking = true);
    Task<TEntity> RemoveAsync(TKey id, TKey userId);
}
using Base.Contracts.Domain;

namespace Base.Contracts.DAL;

public interface IEntityRepository<TEntity> : IEntityRepository<TEntity, Guid>
    where TEntity : class, IDomainEntityId
{
}

public interface IEntityRepository<TEntity, TKey>
    where TEntity : class, IDomainEntityId<TKey>
    where TKey : IEquatable<TKey>
{
    // sync
    TEntity Add(TEntity entity);
    TEntity Update(TEntity entity);
    TEntity RemovePublic(TEntity entity);
    TEntity RemovePublic(TKey id);
    TEntity Remove(TKey id, TKey userId);
    TEntity? FirstOrDefaultPublic(TKey id, bool noTracking = true);
    TEntity? FirstOrDefault(TKey id, TKey userId, bool noTracking = true);
    IEnumerable<TEntity> GetAllPublic(bool noTracking = true);
    IEnumerable<TEntity> GetAll(TKey userId, bool noTracking = true);
    bool Exists(TKey id);

    // async
    Task<TEntity?> FirstOrDefaultAsyncPublic(TKey id, bool noTracking = true);
    Task<TEntity?> FirstOrDefaultAsync(TKey id, TKey userId, bool noTracking = true);
    Task<IEnumerable<TEntity>> GetAllAsyncPublic(bool noTracking = true);
    Task<IEnumerable<TEntity>> GetAllAsync(TKey userId, bool noTracking = true);
    Task<bool> ExistsAsync(TKey id);
    Task<TEntity> RemoveAsyncPublic(TKey id);
    Task<TEntity> RemoveAsync(TKey id, TKey userId);
}
using Base.Contracts.DAL;
using Base.Contracts.Domain;

namespace Base.Contracts.Base;

public interface IPublicEntityService<TEntity> : IPublicEntityRepository<TEntity>, IPublicEntityService<TEntity, Guid>
    where TEntity: class, IDomainEntityId
{
    
}

public interface IPublicEntityService<TEntity, TKey> : IPublicEntityRepository<TEntity, TKey> 
    where TEntity: class, IDomainEntityId<TKey>
    where TKey : IEquatable<TKey>
{
    
}
using Base.Contracts.Base;
using Base.Contracts.DAL;
using Base.Contracts.Domain;

namespace Base.BLL;

public class
    BaseEntityService<TBllEntity, TDalEntity, TRepository> :
        BaseEntityService<TBllEntity, TDalEntity, TRepository, Guid>, IEntityService<TBllEntity>
    where TDalEntity : class, IDomainEntityId
    where TBllEntity : class, IDomainEntityId
    where TRepository : IEntityRepository<TDalEntity>
{
    public BaseEntityService(TRepository repository, IMapper<TBllEntity, TDalEntity> mapper) : base(repository, mapper)
    {
    }
}

public class BaseEntityService<TBllEntity, TDalEntity, TRepository, TKey> :
    BasePublicEntityService<TBllEntity, TDalEntity, TRepository, TKey>, IEntityService<TBllEntity, TKey>
    where TBllEntity : class, IDomainEntityId<TKey>
    where TRepository : IEntityRepository<TDalEntity, TKey>
    where TKey : IEquatable<TKey>
    where TDalEntity : class, IDomainEntityId<TKey>
{
    public BaseEntityService(TRepository repository, IMapper<TBllEntity, TDalEntity> mapper) : base(repository, mapper)
    {
    }

    public TBllEntity Remove(TKey id, TKey userId)
    {
        return Mapper.Map(Repository.Remove(id, userId))!;
    }

    public TBllEntity Update(TBllEntity entity, TKey userId)
    {
        return Mapper.Map(Repository.Update(Mapper.Map(entity)!, userId))!;
    }

    public TBllEntity? FirstOrDefault(TKey id, TKey userId, bool noTracking = true)
    {
        return Mapper.Map(
            Repository.FirstOrDefault(id, userId, noTracking));
    }

    public IEnumerable<TBllEntity> GetAll(TKey userId, bool noTracking = true)
    {
        return Repository.GetAll(userId, noTracking).Select(x => Mapper.Map(x)!);
    }

    public async Task<TBllEntity?> FirstOrDefaultAsync(TKey id, TKey userId, bool noTracking = true)
    {
        return Mapper.Map(await Repository.FirstOrDefaultAsync(id, userId, noTracking));
    }

    public async Task<IEnumerable<TBllEntity>> GetAllAsync(TKey userId, bool noTracking = true)
    {
        return (await Repository.GetAllAsync(userId, noTracking)).Select(x => Mapper.Map(x)!);
    }

    public async Task<TBllEntity> RemoveAsync(TKey id, TKey userId)
    {
        return Mapper.Map(
            await Repository.RemoveAsync(id, userId))!;
    }
}
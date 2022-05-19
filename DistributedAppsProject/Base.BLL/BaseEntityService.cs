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

public class BaseEntityService<TBllEntity, TDalEntity, TRepository, TKey> : IEntityService<TBllEntity, TKey>
    where TBllEntity : class, IDomainEntityId<TKey>
    where TRepository : IEntityRepository<TDalEntity, TKey>
    where TKey : IEquatable<TKey>
    where TDalEntity : class, IDomainEntityId<TKey>
{
    protected TRepository Repository;
    protected IMapper<TBllEntity, TDalEntity> Mapper;

    public BaseEntityService(TRepository repository, IMapper<TBllEntity, TDalEntity> mapper)
    {
        Repository = repository;
        Mapper = mapper;
    }

    public TBllEntity Add(TBllEntity entity)
    {
        return Mapper.Map(
            Repository.Add(
                Mapper.Map(entity)!))!;
    }

    public TBllEntity Update(TBllEntity entity)
    {
        return Mapper.Map(
            Repository.Update(
                Mapper.Map(entity)!))!;
    }

    public TBllEntity Remove(TBllEntity entity)
    {
        return Mapper.Map(
            Repository.Remove(
                Mapper.Map(entity)!))!;
    }

    public TBllEntity Remove(TKey id)
    {
        return Mapper.Map(
            Repository.Remove(id))!;
    }

    public TBllEntity Remove(TKey id, TKey userId)
    {
        return Mapper.Map(Repository.Remove(id, userId))!;
    }

    public TBllEntity? FirstOrDefault(TKey id, bool noTracking = true)
    {
        return Mapper.Map(
            Repository.FirstOrDefault(id, noTracking))!;
    }

    public TBllEntity? FirstOrDefault(TKey id, TKey userId, bool noTracking = true)
    {
        return Mapper.Map(
            Repository.FirstOrDefault(id, userId, noTracking));
    }

    public IEnumerable<TBllEntity> GetAll(bool noTracking = true)
    {
        return Repository.GetAll(noTracking).Select(x => Mapper.Map(x)!);
    }

    public IEnumerable<TBllEntity> GetAll(TKey userId, bool noTracking = true)
    {
        return Repository.GetAll(userId, noTracking).Select(x => Mapper.Map(x)!);
    }

    public bool Exists(TKey id)
    {
        return Repository.Exists(id);
    }

    public async Task<TBllEntity?> FirstOrDefaultAsync(TKey id, bool noTracking = true)
    {
        return Mapper.Map(await Repository.FirstOrDefaultAsync(id, noTracking));
    }

    public async Task<TBllEntity?> FirstOrDefaultAsync(TKey id, TKey userId, bool noTracking = true)
    {
        return Mapper.Map(await Repository.FirstOrDefaultAsync(id, userId, noTracking));
    }

    public async Task<IEnumerable<TBllEntity>> GetAllAsync(bool noTracking = true)
    {
        return (await Repository.GetAllAsync(noTracking)).Select(x => Mapper.Map(x)!);
    }

    public async Task<IEnumerable<TBllEntity>> GetAllAsync(TKey userId, bool noTracking = true)
    {
        return (await Repository.GetAllAsync(userId, noTracking)).Select(x => Mapper.Map(x)!);
    }

    public Task<bool> ExistsAsync(TKey id)
    {
        return Repository.ExistsAsync(id);
    }

    public async Task<TBllEntity> RemoveAsync(TKey id)
    {
        return Mapper.Map(
            await Repository.RemoveAsync(id))!;
    }

    public async Task<TBllEntity> RemoveAsync(TKey id, TKey userId)
    {
        return Mapper.Map(
            await Repository.RemoveAsync(id, userId))!;
    }
}
using Base.Contracts.Base;
using Base.Contracts.DAL;
using Base.Contracts.Domain;

namespace Base.BLL;

public class BasePublicEntityService<TBllEntity, TDalEntity, TRepository> :
    BasePublicEntityService<TBllEntity, TDalEntity, TRepository, Guid>
    where TBllEntity : class, IDomainEntityId<Guid>
    where TRepository : IPublicEntityRepository<TDalEntity, Guid>
    where TDalEntity : class, IDomainEntityId<Guid>
{
    public BasePublicEntityService(TRepository repository, IMapper<TBllEntity, TDalEntity> mapper) : base(repository, mapper)
    {
    }
}

public class BasePublicEntityService<TBllEntity, TDalEntity, TRepository, TKey> : IPublicEntityService<TBllEntity, TKey>
    where TBllEntity : class, IDomainEntityId<TKey>
    where TRepository : IPublicEntityRepository<TDalEntity, TKey>
    where TKey : IEquatable<TKey>
    where TDalEntity : class, IDomainEntityId<TKey>
{
    protected TRepository Repository;
    protected IMapper<TBllEntity, TDalEntity> Mapper;

    public BasePublicEntityService(TRepository repository, IMapper<TBllEntity, TDalEntity> mapper)
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

    public TBllEntity? FirstOrDefault(TKey id, bool noTracking = true)
    {
        return Mapper.Map(
            Repository.FirstOrDefault(id, noTracking))!;
    }

    public IEnumerable<TBllEntity> GetAll(bool noTracking = true)
    {
        return Repository.GetAll(noTracking).Select(x => Mapper.Map(x)!);
    }

    public bool Exists(TKey id)
    {
        return Repository.Exists(id);
    }

    public async Task<TBllEntity?> FirstOrDefaultAsync(TKey id, bool noTracking = true)
    {
        return Mapper.Map(await Repository.FirstOrDefaultAsync(id, noTracking));
    }

    public async Task<IEnumerable<TBllEntity>> GetAllAsync(bool noTracking = true)
    {
        return (await Repository.GetAllAsync(noTracking)).Select(x => Mapper.Map(x)!);
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
}
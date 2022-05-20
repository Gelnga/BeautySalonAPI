using Base.Contracts.Base;
using Base.Contracts.DAL;
using Base.Contracts.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Base.DAL.EF;

public class BaseEntityRepository<TDalEntity, TDomainEntity, TDbContext, TDalUserEntity, TDomainUserEntity> 
    : BaseEntityRepository<TDalEntity, TDomainEntity, Guid, TDbContext, TDalUserEntity, TDomainUserEntity>
    where TDalEntity : class, IDomainEntityId<Guid>, IDomainEntityUserOwnership<Guid, TDalUserEntity>
    where TDomainEntity : class, IDomainEntityId<Guid>, IDomainEntityUserOwnership<Guid, TDomainUserEntity>
    where TDbContext : DbContext
    where TDalUserEntity : IdentityUser<Guid>, IDomainEntityId<Guid>
    where TDomainUserEntity : IdentityUser<Guid>, IDomainEntityId<Guid>
{
    public BaseEntityRepository(TDbContext dbContext, IMapper<TDalEntity, TDomainEntity> mapper) : base(dbContext, mapper)
    {
    }
}

/// <summary>
/// There are method overloads for operations with inclusion of user ownership in them.
/// It is made so, because I couldn't find a way to generically include TKey types default value for method inputs
/// (Basic idea would be to have no separate method signatures, but to leave, where it is required, an input field
///  which would ask for a userId).
/// 
/// It is not straightforward possible, because TKey may be either a reference or a value type, which default values vary
/// </summary>
/// <typeparam name="TDomainEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TDbContext"></typeparam>
/// <typeparam name="TDalUserEntity"></typeparam>
/// <typeparam name="TDalEntity"></typeparam>
/// <typeparam name="TDomainUserEntity"></typeparam>
public class BaseEntityRepository<TDalEntity, TDomainEntity, TKey, TDbContext, TDalUserEntity, TDomainUserEntity> : 
    BasePublicEntityRepository<TDalEntity, TDomainEntity, TKey, TDbContext>, IEntityRepository<TDalEntity, TKey>
    where TDomainEntity : class, IDomainEntityId<TKey>, IDomainEntityUserOwnership<TKey, TDomainUserEntity>
    where TDalEntity : class, IDomainEntityId<TKey>, IDomainEntityUserOwnership<TKey, TDalUserEntity>
    where TKey : IEquatable<TKey> 
    where TDbContext : DbContext
    where TDalUserEntity : IdentityUser<TKey>, IDomainEntityId<TKey>
    where TDomainUserEntity : IdentityUser<TKey>, IDomainEntityId<TKey>
{

    public BaseEntityRepository(TDbContext dbContext, IMapper<TDalEntity, TDomainEntity> mapper) : base(dbContext, mapper)
    {
    }
    protected virtual IQueryable<TDomainEntity> CreateQuery(TKey userId, bool noTracking = true) 
    {
        var query = CreateQuery(noTracking);
        query = query
            .Include(e => e.AppUser)
            .Where(e => e.AppUserId.Equals(userId));

        return query;
    }
    
    public virtual TDalEntity Remove(TKey id, TKey userId)
    {
        var entity = FirstOrDefault(id, userId);
        if (entity == null)
        {
            throw new NullReferenceException($"Entity {typeof(TDomainEntity).Name} with id {id} was not found");
        }

        return Remove(entity);
    }

    public virtual TDalEntity? FirstOrDefault(TKey id, TKey userId, bool noTracking = true)
    {
        return Mapper.Map(
            CreateQuery(userId, noTracking).FirstOrDefault(a => a.Id.Equals(id))
            );
    }

    public virtual IEnumerable<TDalEntity> GetAll(TKey userId, bool noTracking = true)
    {
        return CreateQuery(userId, noTracking)
            .ToList()
            .Select(x => Mapper.Map(x)!);;
    }

    public virtual async Task<TDalEntity?> FirstOrDefaultAsync(TKey id, TKey userId, bool noTracking = true)
    {
        return Mapper.Map(
            await CreateQuery(userId, noTracking).FirstOrDefaultAsync(a => a.Id.Equals(id))
            );
    }

    public virtual async Task<IEnumerable<TDalEntity>> GetAllAsync(TKey userId, bool noTracking = true)
    {
        return (
                await CreateQuery(userId, noTracking)
                    .ToListAsync()
            )
            .Select(x => Mapper.Map(x)!);
    }

    public virtual async Task<TDalEntity> RemoveAsync(TKey id, TKey userId)
    {
        var entity = await FirstOrDefaultAsync(id, userId);
        if (entity == null)
        {
            throw new NullReferenceException($"Entity {typeof(TDomainEntity).Name} with id {id} was not found");
        }

        return Remove(entity);
    }
}
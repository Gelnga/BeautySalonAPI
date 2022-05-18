using Base.Contracts.DAL;
using Base.Contracts.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Base.DAL.EF;

public class BaseEntityRepository<TEntity, TDbContext, TUserEntity> : BaseEntityRepository<TEntity, Guid, TDbContext, TUserEntity>
    where TEntity : class, IDomainEntityId<Guid>, IDomainEntityUserOwnership<Guid, TUserEntity>
    where TDbContext : DbContext
    where TUserEntity : IdentityUser<Guid>, IDomainEntityId<Guid>
{
    public BaseEntityRepository(TDbContext dbContext) : base(dbContext)
    {
    }
}

public class  BaseEntityRepository<TEntity, TKey, TDbContext, TUserEntity> : IEntityRepository<TEntity, TKey>
    where TEntity : class, IDomainEntityId<TKey>, IDomainEntityUserOwnership<TKey, TUserEntity>
    where TKey : IEquatable<TKey>
    where TDbContext : DbContext
    where TUserEntity : IdentityUser<TKey>, IDomainEntityId<TKey>
{
    protected readonly TDbContext RepoDbContext;
    protected readonly DbSet<TEntity> RepoDbSet;

    public BaseEntityRepository(TDbContext dbContext)
    {
        RepoDbContext = dbContext;
        RepoDbSet = dbContext.Set<TEntity>();
    }

    protected virtual IQueryable<TEntity> CreateQueryPublic(bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable();
        if (noTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }

    protected virtual IQueryable<TEntity> CreateQuery(TKey userId, bool noTracking = true)
    {
        var query = CreateQueryPublic(noTracking);
        query = query
            .Include(e => e.AppUser)
            .Where(e => e.AppUserId.Equals(userId));

        return query;
    }

    public virtual TEntity Add(TEntity entity)
    {
        return RepoDbSet.Add(entity).Entity;
    }

    public virtual TEntity Update(TEntity entity)
    {
        return RepoDbSet.Update(entity).Entity;
    }

    public virtual TEntity RemovePublic(TEntity entity)
    {
        return RepoDbSet.Remove(entity).Entity;
    }

    public virtual TEntity RemovePublic(TKey id)
    {
        var entity = FirstOrDefaultPublic(id);
        if (entity == null)
        {
            throw new NullReferenceException($"Entity {typeof(TEntity).Name} with id {id} was not found");
        }

        return RemovePublic(entity);
    }

    public virtual TEntity Remove(TKey id, TKey userId)
    {
        var entity = FirstOrDefault(id, userId);
        if (entity == null)
        {
            throw new NullReferenceException($"Entity {typeof(TEntity).Name} with id {id} was not found");
        }

        return RemovePublic(entity);
    }

    public virtual TEntity? FirstOrDefaultPublic(TKey id, bool noTracking = true)
    {
        return CreateQueryPublic(noTracking).FirstOrDefault(a => a.Id.Equals(id));
    }

    public virtual TEntity? FirstOrDefault(TKey id, TKey userId, bool noTracking = true)
    {
        return CreateQuery(userId, noTracking).FirstOrDefault(a => a.Id.Equals(id));
    }

    public virtual IEnumerable<TEntity> GetAllPublic(bool noTracking = true)
    {
        return CreateQueryPublic(noTracking).ToList();
    }

    public virtual IEnumerable<TEntity> GetAll(TKey userId, bool noTracking = true)
    {
        return CreateQuery(userId, noTracking).ToList();
    }

    public virtual bool Exists(TKey id)
    {
        return RepoDbSet.Any(a => a.Id.Equals(id));
    }

    public virtual async Task<TEntity?> FirstOrDefaultAsyncPublic(TKey id, bool noTracking = true)
    {
        return await CreateQueryPublic(noTracking).FirstOrDefaultAsync(a => a.Id.Equals(id));
    }

    public virtual async Task<TEntity?> FirstOrDefaultAsync(TKey id, TKey userId, bool noTracking = true)
    {
        return await CreateQuery(userId, noTracking).FirstOrDefaultAsync(a => a.Id.Equals(id));
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsyncPublic(bool noTracking = true)
    {
        return await CreateQueryPublic(noTracking).ToListAsync();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(TKey userId, bool noTracking = true)
    {
        return await CreateQuery(userId, noTracking).ToListAsync();
    }

    public virtual async Task<bool> ExistsAsync(TKey id)
    {
        return await RepoDbSet.AnyAsync(a => a.Id.Equals(id));
    }

    public virtual async Task<TEntity> RemoveAsyncPublic(TKey id)
    {
        var entity = await FirstOrDefaultAsyncPublic(id);
        if (entity == null)
        {
            throw new NullReferenceException($"Entity {typeof(TEntity).Name} with id {id} was not found");
        }

        return RemovePublic(entity);
    }

    public virtual async Task<TEntity> RemoveAsync(TKey id, TKey userId)
    {
        var entity = await FirstOrDefaultAsync(id, userId);
        if (entity == null)
        {
            throw new NullReferenceException($"Entity {typeof(TEntity).Name} with id {id} was not found");
        }

        return RemovePublic(entity);
    }
}
using Microsoft.AspNetCore.Identity;

namespace Base.Contracts.Domain;

public interface IDomainEntityUserOwnership<TUserEntity> : IDomainEntityUserOwnership<Guid, TUserEntity>
    where TUserEntity : IdentityUser<Guid>, IDomainEntityId
{
}

public interface IDomainEntityUserOwnership<TKey, TUserEntity>
    where TKey : IEquatable<TKey>
    where TUserEntity : IdentityUser<TKey>, IDomainEntityId<TKey>
{
    public TKey AppUserId { get; set; }
    public TUserEntity? AppUser { get; set; }
}
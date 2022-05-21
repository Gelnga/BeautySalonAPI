using Base.Contracts.Domain;
using Microsoft.AspNetCore.Identity;

namespace Base.Domain;

public class DomainEntityBaseId<TUserEntity> : DomainEntityBaseId<Guid, TUserEntity>, IDomainEntityId
    where TUserEntity : IdentityUser<Guid>, IDomainEntityId<Guid>
{
}

public class DomainEntityBaseId<TKey, TUserEntity> : DomainEntityId<TKey>, IDomainEntityUserOwnership<TKey, TUserEntity>, IDomainEntityBase
    where TKey : IEquatable<TKey> 
    where TUserEntity : IdentityUser<TKey>, IDomainEntityId<TKey>
{
    public TKey AppUserId { get; set; } = default!;
    public TUserEntity? AppUser { get; set; }
    public string? Commentary { get; set; }
}
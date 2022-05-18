using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;
using Base.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace Base.Domain;

public abstract class DomainEntityBaseMetaId<TUserEntity> : DomainEntityBaseMetaId<Guid, TUserEntity>, IDomainEntityId
    where TUserEntity : IdentityUser<Guid>, IDomainEntityId<Guid>
{
}

public abstract class DomainEntityBaseMetaId<TKey, TUserEntity> : DomainEntityBaseMetaIdPublic<TKey>, IDomainEntityUserOwnership<TKey, TUserEntity>
    where TKey : IEquatable<TKey> 
    where TUserEntity : IdentityUser<TKey>, IDomainEntityId<TKey>
{
    public TKey AppUserId { get; set; } = default!;
    public TUserEntity? AppUser { get; set; }
}
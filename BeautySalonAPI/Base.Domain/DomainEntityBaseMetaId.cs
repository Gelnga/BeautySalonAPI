using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;
using Base.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace Base.Domain;

public abstract class DomainEntityBaseMetaId : DomainEntityBaseMetaId<Guid>, IDomainEntityId
{
}

public abstract class DomainEntityBaseMetaId<TKey> : DomainEntityBaseMetaIdPublic<TKey>, IDomainEntityUserOwnership<TKey>
    where TKey : IEquatable<TKey>
{
    public virtual TKey OwnerId { get; set; } = default!;
}
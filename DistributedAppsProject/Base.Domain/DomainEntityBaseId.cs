using Base.Contracts.Domain;
using Microsoft.AspNetCore.Identity;

namespace Base.Domain;

public class DomainEntityBaseId : DomainEntityBaseId<Guid>, IDomainEntityId
{
}

public class DomainEntityBaseId<TKey> : DomainEntityId<TKey>, IDomainEntityUserOwnership<TKey>, IDomainEntityBase
    where TKey : IEquatable<TKey>
{
    public TKey OwnerId { get; set; } = default!;
    public string? Commentary { get; set; }
}
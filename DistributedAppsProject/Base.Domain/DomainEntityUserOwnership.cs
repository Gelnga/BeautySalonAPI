using System.ComponentModel;
using Base.Contracts.Domain;
using Base.Domain.Identity;

namespace Base.Domain;

public abstract class DomainEntityUserOwnership : DomainEntityUserOwnership<Guid>
{
}

public abstract class DomainEntityUserOwnership<TKey> : IDomainEntityUserOwnership<TKey, BaseUser<TKey>>
    where TKey : IEquatable<TKey>
{
    public TKey AppUserId { get; set; } = default!;
    public BaseUser<TKey>? AppUser { get; set; }
}
using Microsoft.AspNetCore.Identity;

namespace Base.Contracts.Domain;

public interface IDomainEntityUserOwnership : IDomainEntityUserOwnership<Guid>
{
}

public interface IDomainEntityUserOwnership<TKey>
    where TKey : IEquatable<TKey>
{
    public TKey OwnerId { get; set; }
}
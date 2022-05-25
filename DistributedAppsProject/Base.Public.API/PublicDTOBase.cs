using Base.Domain;

namespace Base.Public.API;

public class PublicDTOBase : PublicDTOBase<Guid>
{
}

public class PublicDTOBase<TKey> : DomainEntityId<TKey>
    where TKey : IEquatable<TKey>
{
    public string? Commentary { get; set; }
}
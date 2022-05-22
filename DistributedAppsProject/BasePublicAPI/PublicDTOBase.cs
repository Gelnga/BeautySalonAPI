using Base.Domain;

namespace BasePublicAPI;

public class PublicDTOBase : PublicDTOBase<Guid>
{
}

public class PublicDTOBase<TKey> : DomainEntityId<TKey>
    where TKey : IEquatable<TKey>
{
    public string? Commentary { get; set; }
}
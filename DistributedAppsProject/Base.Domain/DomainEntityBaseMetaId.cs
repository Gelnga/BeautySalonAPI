using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;

namespace Base.Domain;

public abstract class DomainEntityBaseMetaId : DomainEntityBaseMetaId<Guid>, IDomainEntityId
{
}

public abstract class DomainEntityBaseMetaId<TKey> : DomainEntityId<TKey>, IDomainEntityMeta, IDomainEntityBase
    where TKey : IEquatable<TKey>
{
    public string? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    [MaxLength(256)]
    public string? Commentary { get; set; }

}
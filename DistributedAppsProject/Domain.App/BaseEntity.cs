using System.ComponentModel.DataAnnotations;

namespace Domain.App.Identity;

public class BaseEntity
{
    [MaxLength(256)]
    public String? Commentary { get; set; }
}

public class BaseEntityId : BaseEntity
{
    public Guid Id { get; set; }
}
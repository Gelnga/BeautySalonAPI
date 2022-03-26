using System.ComponentModel.DataAnnotations;
using Resources = App.Resources.App.Domain;

namespace Domain.App.Identity;

public class BaseEntity
{
    [MaxLength(256)]
    [Display(ResourceType = typeof(Resources.BaseEntity), Name = nameof(Commentary))]
    public String? Commentary { get; set; }
}

public class BaseEntityId : BaseEntity
{
    public Guid Id { get; set; }
}
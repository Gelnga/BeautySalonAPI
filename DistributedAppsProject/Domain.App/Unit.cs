using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Resources = App.Resources.App.Domain;

namespace Domain.App;

public class Unit : BaseEntityId
{
    [MaxLength(256)]
    [Display(ResourceType = typeof(Resources.Unit), Name = "Name")]
    public String Name { get; set; } = default!;

    public ICollection<SalonService>? SalonServices { get; set; }
}
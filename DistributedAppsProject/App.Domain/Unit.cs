using System.ComponentModel.DataAnnotations;
using Base.Domain;
using Resources = App.Resources.App.Domain;

namespace Domain.App;

public class Unit : DomainEntityBaseMetaId
{
    [MaxLength(256)]
    [Display(ResourceType = typeof(Resources.Unit), Name = "Name")]
    public String Name { get; set; } = default!;

    public ICollection<SalonService>? SalonServices { get; set; }
}
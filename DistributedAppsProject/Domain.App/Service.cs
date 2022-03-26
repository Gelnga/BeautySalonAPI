using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Resources = App.Resources.App.Domain;

namespace Domain.App;

public class Service : BaseEntityId
{
    [MaxLength(256)]
    [Display(ResourceType = typeof(Resources.Service), Name = "Name")]
    public String Name { get; set; } = default!;

    [MaxLength(1024)]
    [Display(ResourceType = typeof(Resources.Service), Name = "Description")]
    public String? Description { get; set; }

    public ICollection<SalonService>? SalonServices { get; set; }
    public ICollection<Appointment>? Appointments { get; set; }
    public ICollection<ImageObject>? ImageObjects { get; set; }
}
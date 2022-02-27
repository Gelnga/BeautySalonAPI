using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;

namespace Domain.App;

public class Service : BaseEntityId
{
    [MaxLength(256)]
    public String Name { get; set; } = default!;

    [MaxLength(1024)]
    public String? Description { get; set; }

    public ICollection<SalonService>? SalonServices { get; set; }
    public ICollection<Appointment>? Appointments { get; set; }
    public ICollection<ImageObject>? ImageObjects { get; set; }
}
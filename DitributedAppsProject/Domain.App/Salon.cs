using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;

namespace Domain.App;

public class Salon : BaseEntityId
{
    public Guid? WorkScheduleId { get; set; }
    public WorkSchedule? WorkSchedule { get; set; }

    [MaxLength(256)]
    public String Name { get; set; } = default!;

    [MaxLength(1024)]
    public String? Description { get; set; }

    [MaxLength(256)]
    public String Address { get; set; } = default!;

    [MaxLength(512)]
    public String? GoogleMapsLink { get; set; }

    [MaxLength(256)]
    public String? Email { get; set; }
    
    [MaxLength(256)] 
    public String? PhoneNumber { get; set; }

    public ICollection<SalonService>? SalonServices { get; set; }
    public ICollection<SalonWorker>? SalonWorkers { get; set; }
    public ICollection<Appointment>? Appointments { get; set; }
    public ICollection<ImageObject>? ImageObjects { get; set; }
}
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Resources = App.Resources.App.Domain;

namespace Domain.App;

public class Salon : BaseEntityId
{
    public Guid? WorkScheduleId { get; set; }
    [Display(ResourceType = typeof(Resources.Salon), Name = "WorkSchedule")]
    public WorkSchedule? WorkSchedule { get; set; }

    [MaxLength(256)]
    [Display(ResourceType = typeof(Resources.Salon), Name = "Name")]
    public String Name { get; set; } = default!;

    [MaxLength(1024)]
    [Display(ResourceType = typeof(Resources.Salon), Name = "Description")]
    public String? Description { get; set; }

    [MaxLength(256)]
    [Display(ResourceType = typeof(Resources.Salon), Name = "Address")]
    public String Address { get; set; } = default!;

    [MaxLength(512)]
    [Display(ResourceType = typeof(Resources.Salon), Name = "GoogleMapsLink")]
    public String? GoogleMapsLink { get; set; }

    [MaxLength(256)]
    [Display(ResourceType = typeof(Resources.Salon), Name = "Email")]
    public String? Email { get; set; }
    
    [MaxLength(256)] 
    [Display(ResourceType = typeof(Resources.Salon), Name = "PhoneNumber")]
    public String? PhoneNumber { get; set; }

    public ICollection<SalonService>? SalonServices { get; set; }
    public ICollection<SalonWorker>? SalonWorkers { get; set; }
    public ICollection<Appointment>? Appointments { get; set; }
    public ICollection<ImageObject>? ImageObjects { get; set; }
}
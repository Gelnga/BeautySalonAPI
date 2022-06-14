using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Salon : DomainEntityBaseMetaId
{
    public Guid? WorkScheduleId { get; set; }
    [Display(ResourceType = typeof(Resources.App.Domain.Salon), Name = "WorkSchedule")]
    public WorkSchedule? WorkSchedule { get; set; }

    [MaxLength(256)]
    [Display(ResourceType = typeof(Resources.App.Domain.Salon), Name = "Name")]
    [Column(TypeName = "jsonb")]
    public LangStr Name { get; set; } = default!;

    [MaxLength(1024)]
    [Display(ResourceType = typeof(Resources.App.Domain.Salon), Name = "Description")]
    [Column(TypeName = "jsonb")]
    public LangStr? Description { get; set; }

    [MaxLength(256)]
    [Display(ResourceType = typeof(Resources.App.Domain.Salon), Name = "Address")]
    public string Address { get; set; } = default!;

    [MaxLength(512)]
    [Display(ResourceType = typeof(Resources.App.Domain.Salon), Name = "GoogleMapsLink")]
    public string? GoogleMapsLink { get; set; }

    [MaxLength(256)]
    [Display(ResourceType = typeof(Resources.App.Domain.Salon), Name = "Email")]
    public string? Email { get; set; }
    
    [MaxLength(256)] 
    [Display(ResourceType = typeof(Resources.App.Domain.Salon), Name = "PhoneNumber")]
    public string? PhoneNumber { get; set; }

    public ICollection<SalonService>? SalonServices { get; set; }
    public ICollection<SalonWorker>? SalonWorkers { get; set; }
    public ICollection<Appointment>? Appointments { get; set; }
    public ICollection<ImageObject>? ImageObjects { get; set; }
}
using System.ComponentModel.DataAnnotations;
using Base.Domain;
using Resources = App.Resources.App.Domain;

namespace Domain.App;

public class Worker : DomainEntityBaseMetaId
{
    public Guid? JobPositionId { get; set; }
    [Display(ResourceType = typeof(Resources.Worker), Name = "JobPosition")]
    public JobPosition? JobPosition { get; set; } = default!;

    public Guid? WorkScheduleId { get; set; }
    [Display(ResourceType = typeof(Resources.Worker), Name = "WorkSchedule")]
    public WorkSchedule? WorkSchedule { get; set; }

    [MaxLength(256)]
    [Display(ResourceType = typeof(Resources.Worker), Name = "FirstName")]
    public String FirstName { get; set; } = default!;
    
    [MaxLength(256)]
    [Display(ResourceType = typeof(Resources.Worker), Name = "LastName")]
    public String LastName { get; set; } = default!;
    
    [MaxLength(256)]
    [Display(ResourceType = typeof(Resources.Worker), Name = "Email")]
    public String Email { get; set; } = default!;
    
    [MaxLength(256)]
    [Display(ResourceType = typeof(Resources.Worker), Name = "PhoneNumber")]
    public String PhoneNumber { get; set; } = default!;

    public ICollection<SalonWorker>? SalonWorkers { get; set; }
    public ICollection<Appointment>? Appointments { get; set; }
    public ICollection<ImageObject>? ImageObjects { get; set; }
    public ICollection<BlogPost>? BlogPosts { get; set; }
}
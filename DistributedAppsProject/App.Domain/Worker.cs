using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domain.Identity;
using Base.Domain;
using WorkSchedule = App.Domain.WorkSchedule;

namespace App.Domain;

public class Worker : DomainEntityBaseMetaId
{
    public Guid? JobPositionId { get; set; }
    [Display(ResourceType = typeof(Resources.App.Domain.Worker), Name = "JobPosition")]
    public JobPosition? JobPosition { get; set; } = default!;

    public Guid? WorkScheduleId { get; set; }
    [Display(ResourceType = typeof(Resources.App.Domain.Worker), Name = "WorkSchedule")]
    public WorkSchedule? WorkSchedule { get; set; }

    [MaxLength(256)]
    [Display(ResourceType = typeof(Resources.App.Domain.Worker), Name = "FirstName")]
    [Column(TypeName = "jsonb")]
    public LangStr FirstName { get; set; } = default!;
    
    [MaxLength(256)]
    [Display(ResourceType = typeof(Resources.App.Domain.Worker), Name = "LastName")]
    [Column(TypeName = "jsonb")]
    public LangStr LastName { get; set; } = default!;
    
    [MaxLength(256)]
    [Display(ResourceType = typeof(Resources.App.Domain.Worker), Name = "Email")]
    public string Email { get; set; } = default!;
    
    [MaxLength(256)]
    [Display(ResourceType = typeof(Resources.App.Domain.Worker), Name = "PhoneNumber")]
    public string PhoneNumber { get; set; } = default!;

    public ICollection<SalonWorker>? SalonWorkers { get; set; }
    public ICollection<Appointment>? Appointments { get; set; }
    public ICollection<BlogPost>? BlogPosts { get; set; }
    public ICollection<AppUser>? AppUsers { get; set; }
}
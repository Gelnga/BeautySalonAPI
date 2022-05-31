using System.ComponentModel.DataAnnotations;
using App.BLL.DTO.Identity;
using Base.Domain;

namespace App.BLL.DTO;

public class Worker : DomainEntityBaseId
{
    public Guid? JobPositionId { get; set; }
    public JobPosition? JobPosition { get; set; } = default!;

    public Guid? WorkScheduleId { get; set; }
    public WorkSchedule? WorkSchedule { get; set; }

    [MaxLength(256)]
    public string FirstName { get; set; } = default!;
    
    [MaxLength(256)]
    public string LastName { get; set; } = default!;
    
    [MaxLength(256)]
    public string Email { get; set; } = default!;
    
    [MaxLength(256)]
    public string PhoneNumber { get; set; } = default!;
    
    public int? Price { get; set; }
    
    public TimeSpan? ServiceDuration { get; set; }

    public string? UnitName { get; set; } = default!;

    public ICollection<SalonWorker>? SalonWorkers { get; set; }
    public ICollection<Appointment>? Appointments { get; set; }
    public ICollection<ImageObject>? ImageObjects { get; set; }
    public ICollection<BlogPost>? BlogPosts { get; set; }
    public ICollection<AppUser>? AppUsers { get; set; }
}
using System.ComponentModel.DataAnnotations;
using App.DAL.DTO.Identity;
using Base.Domain;

namespace App.DAL.DTO;

public class Worker : DomainEntityBaseId<AppUser>
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

    public ICollection<SalonWorker>? SalonWorkers { get; set; }
    public ICollection<Appointment>? Appointments { get; set; }
    public ICollection<ImageObject>? ImageObjects { get; set; }
    public ICollection<BlogPost>? BlogPosts { get; set; }
}
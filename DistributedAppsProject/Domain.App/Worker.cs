using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;

namespace Domain.App;

public class Worker : BaseEntityId
{
    public Guid? JobPositionId { get; set; }
    public JobPosition? JobPosition { get; set; } = default!;

    public Guid? WorkScheduleId { get; set; }
    public WorkSchedule? WorkSchedule { get; set; }

    [MaxLength(256)]
    public String FirstName { get; set; } = default!;
    
    [MaxLength(256)]
    public String LastName { get; set; } = default!;
    
    [MaxLength(256)]
    public String Email { get; set; } = default!;
    
    [MaxLength(256)]
    public String PhoneNumber { get; set; } = default!;

    public ICollection<SalonWorker>? SalonWorkers { get; set; }
    public ICollection<Appointment>? Appointments { get; set; }
    public ICollection<ImageObject>? ImageObjects { get; set; }
    public ICollection<BlogPost>? BlogPosts { get; set; }
}
using App.Enums;
using Base.Domain;
using AppUser = App.DAL.DTO.Identity.AppUser;

namespace App.DAL.DTO;

public class WorkDay : DomainEntityBaseId<AppUser>
{
    public Guid WorkScheduleId { get; set; }
    public WorkSchedule? WorkSchedule { get; set; } = default!;

    public TimeOnly WorkDayStart { get; set; }
    public TimeOnly WorkDayEnd { get; set; }
    public DateOnly? WorkDayDate { get; set; }
    
    public Days? WeekDay { get; set; } 
}
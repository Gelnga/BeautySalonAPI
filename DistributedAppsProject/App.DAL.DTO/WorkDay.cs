using App.Enums;
using Base.Domain;
using AppUser = App.DAL.DTO.Identity.AppUser;

namespace App.DAL.DTO;

public class WorkDay : DomainEntityBaseId
{
    public Guid WorkScheduleId { get; set; }
    public WorkSchedule? WorkSchedule { get; set; } = default!;

    public TimeSpan WorkDayStart { get; set; }
    public TimeSpan WorkDayEnd { get; set; }
    public DateOnly? WorkDayDate { get; set; }
    
    public TimeSpan? LunchBreakStartTime { get; set; }
    public TimeSpan? LunchBreakEndTime { get; set; }
    
    public Days? WeekDay { get; set; } 
}
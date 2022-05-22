using System.ComponentModel.DataAnnotations;
using Base.Domain;
using AppUser = App.DAL.DTO.Identity.AppUser;

namespace App.DAL.DTO;

public class WorkDay : DomainEntityBaseId<AppUser>
{
    public Guid WorkScheduleId { get; set; }
    public WorkSchedule? WorkSchedule { get; set; } = default!;

    public DateTime WorkDayStart { get; set; }
    public DateTime WorkDayEnd { get; set; }

    [MaxLength(16)]
    public string? WeekDay { get; set; } 
}
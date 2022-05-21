using System.ComponentModel.DataAnnotations;
using Base.Domain;
using AppUser = App.DAL.DTO.Identity.AppUser;
using WorkSchedule = App.DAL.DTO.WorkSchedule;

namespace App.DAL.DTO;

public class WorkDay : DomainEntityBaseId<AppUser>
{
    public Guid WorkScheduleId { get; set; }
    [Display(ResourceType = typeof(Resources.App.Domain.WorkDay), Name = "WorkSchedule")]
    public WorkSchedule? WorkSchedule { get; set; } = default!;

    [Display(ResourceType = typeof(Resources.App.Domain.WorkDay), Name = "WorkDayStart")]
    public DateTime WorkDayStart { get; set; }
    [Display(ResourceType = typeof(Resources.App.Domain.WorkDay), Name = "WorkDayEnd")]
    public DateTime WorkDayEnd { get; set; }

    [MaxLength(16)]
    [Display(ResourceType = typeof(Resources.App.Domain.WorkDay), Name = "Weekday")]
    public string? WeekDay { get; set; } 
}
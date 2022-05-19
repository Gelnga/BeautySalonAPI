using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;
using Domain.App.Identity;
using AppUser = App.BLL.DTO.Identity.AppUser;
using WorkSchedule = App.BLL.DTO.WorkSchedule;

namespace App.BLL.DTO;

public class WorkDay : DomainEntityBaseMetaId<AppUser>
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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domain.Identity;
using App.Enums;
using Base.Domain;

namespace App.Domain;

public class WorkDay : DomainEntityBaseMetaId
{
    public Guid WorkScheduleId { get; set; }
    [Display(ResourceType = typeof(Resources.App.Domain.WorkDay), Name = "WorkSchedule")]
    public WorkSchedule? WorkSchedule { get; set; } = default!;

    [Display(ResourceType = typeof(Resources.App.Domain.WorkDay), Name = "WorkDayStart")]
    public TimeSpan WorkDayStart { get; set; }
    [Display(ResourceType = typeof(Resources.App.Domain.WorkDay), Name = "WorkDayEnd")]
    public TimeSpan WorkDayEnd { get; set; }

    public TimeSpan? LunchBreakStartTime { get; set; }
    
    public TimeSpan? LunchBreakEndTime { get; set; }

    public DateOnly? WorkDayDate { get; set; }
    
    [Display(ResourceType = typeof(Resources.App.Domain.WorkDay), Name = "Weekday")]
    [Column(TypeName = "varchar(20)")]
    public Days? WeekDay { get; set; }
}
using System.ComponentModel.DataAnnotations;
using Base.Domain;
using Resources = App.Resources.App.Domain;

namespace Domain.App;

public class WorkDay : DomainEntityBaseMetaId
{
    public Guid WorkScheduleId { get; set; }
    [Display(ResourceType = typeof(Resources.WorkDay), Name = "WorkSchedule")]
    public WorkSchedule? WorkSchedule { get; set; } = default!;

    [Display(ResourceType = typeof(Resources.WorkDay), Name = "WorkDayStart")]
    public DateTime WorkDayStart { get; set; }
    [Display(ResourceType = typeof(Resources.WorkDay), Name = "WorkDayEnd")]
    public DateTime WorkDayEnd { get; set; }

    [MaxLength(16)]
    [Display(ResourceType = typeof(Resources.WorkDay), Name = "Weekday")]
    public String? WeekDay { get; set; }
}
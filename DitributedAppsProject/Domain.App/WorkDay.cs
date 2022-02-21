using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;

namespace Domain.App;

public class WorkDay : BaseEntityId
{
    public Guid WorkScheduleId { get; set; }
    public WorkSchedule? WorkSchedule { get; set; } = default!;

    public DateTime WorkDayStart { get; set; }
    public DateTime WorkDayEnd { get; set; }

    [MaxLength(16)]
    public String? WeekDay { get; set; }
}
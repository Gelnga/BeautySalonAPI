using System.ComponentModel.DataAnnotations;
using Base.Public.API;

namespace App.Public.DTO.v1;

public class WorkDay : PublicDTOBase
{
    public Guid WorkScheduleId { get; set; }

    public TimeOnly WorkDayStart { get; set; }
    public TimeOnly WorkDayEnd { get; set; }
    public DateOnly? WorkDayDate { get; set; }
    
    public TimeOnly LunchBreakStartTime { get; set; }
    public TimeOnly LunchBreakEndTime { get; set; }

    [MaxLength(16)]
    public string? WeekDay { get; set; } 
}
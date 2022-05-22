using System.ComponentModel.DataAnnotations;
using BasePublicAPI;

namespace App.Public.DTO.v1;

public class WorkDay : PublicDTOBase
{
    public Guid WorkScheduleId { get; set; }

    public DateTime WorkDayStart { get; set; }
    public DateTime WorkDayEnd { get; set; }

    [MaxLength(16)]
    public string? WeekDay { get; set; } 
}
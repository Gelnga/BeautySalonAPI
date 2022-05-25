using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Enums;
using Base.Domain;
using Domain.App.Identity;
using AppUser = App.BLL.DTO.Identity.AppUser;
using WorkSchedule = App.BLL.DTO.WorkSchedule;

namespace App.BLL.DTO;

public class WorkDay : DomainEntityBaseId<AppUser>
{
    public Guid WorkScheduleId { get; set; }
    public WorkSchedule? WorkSchedule { get; set; } = default!;

    public TimeOnly WorkDayStart { get; set; }
    public TimeOnly WorkDayEnd { get; set; }
    public DateOnly? WorkDayDate { get; set; }
    
    public Days? WeekDay { get; set; } 
}
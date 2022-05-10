﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;
using WorkSchedule = App.Domain.WorkSchedule;

namespace App.Domain;

public class WorkDay : DomainEntityBaseMetaId
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
    [Column(TypeName = "jsonb")]
    public LangStr? WeekDay { get; set; }
}
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Resources = App.Resources.App.Domain;

namespace Domain.App;

public class WorkSchedule : BaseEntityId
{
    [MaxLength(256)]
    [Display(ResourceType = typeof(Resources.WorkSchedule), Name = "Name")]
    public String Name { get; set; } = default!;
    [Display(ResourceType = typeof(Resources.WorkSchedule), Name = "IsWeek")]
    public bool IsWeek { get; set; }

    public ICollection<WorkDay>? WorkDays { get; set; }
    public ICollection<Salon>? Salons { get; set; }
    public ICollection<Worker>? Workers { get; set; }
}
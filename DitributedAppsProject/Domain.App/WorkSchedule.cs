using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;

namespace Domain.App;

public class WorkSchedule : BaseEntityId
{
    [MaxLength(256)]
    public String Name { get; set; } = default!;
    public bool IsWeek { get; set; }

    public ICollection<WorkDay>? WorkDays { get; set; }
    public ICollection<Salon>? Salons { get; set; }
    public ICollection<Worker>? Workers { get; set; }
}
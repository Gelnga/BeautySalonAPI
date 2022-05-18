using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;
using Domain.App.Identity;
using Salon = App.Domain.Salon;

namespace App.Domain;

public class WorkSchedule : DomainEntityBaseMetaId<AppUser>
{
    [MaxLength(256)]
    [Display(ResourceType = typeof(Resources.App.Domain.WorkSchedule), Name = "Name")]
    [Column(TypeName = "jsonb")]
    public LangStr Name { get; set; } = default!;
    [Display(ResourceType = typeof(Resources.App.Domain.WorkSchedule), Name = "IsWeek")]
    public bool IsWeek { get; set; }

    public ICollection<WorkDay>? WorkDays { get; set; }
    public ICollection<Salon>? Salons { get; set; }
    public ICollection<Worker>? Workers { get; set; }
}
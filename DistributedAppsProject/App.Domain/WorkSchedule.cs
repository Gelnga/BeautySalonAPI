using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;
using Resources = App.Resources.App.Domain;

namespace Domain.App;

public class WorkSchedule : DomainEntityBaseMetaId
{
    [MaxLength(256)]
    [Display(ResourceType = typeof(Resources.WorkSchedule), Name = "Name")]
    [Column(TypeName = "jsonb")]
    public LangStr Name { get; set; } = default!;
    [Display(ResourceType = typeof(Resources.WorkSchedule), Name = "IsWeek")]
    public bool IsWeek { get; set; }

    public ICollection<WorkDay>? WorkDays { get; set; }
    public ICollection<Salon>? Salons { get; set; }
    public ICollection<Worker>? Workers { get; set; }
}
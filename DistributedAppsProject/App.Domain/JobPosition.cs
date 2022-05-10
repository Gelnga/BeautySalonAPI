using System.ComponentModel.DataAnnotations;
using Base.Domain;
using Resources = App.Resources.App.Domain;

namespace Domain.App;

public class JobPosition : DomainEntityBaseMetaId
{
    [MaxLength(256)]
    [Display(ResourceType = typeof(Resources.JobPosition), Name = "Name")]
    public String Name { get; set; } = default!;

    public ICollection<Worker>? Workers { get; set; } = default!;
}
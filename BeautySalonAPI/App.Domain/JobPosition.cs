using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domain.Identity;
using Base.Domain;
using Worker = App.Domain.Worker;

namespace App.Domain;

public class JobPosition : DomainEntityBaseMetaId
{
    [MaxLength(256)]
    [Display(ResourceType = typeof(Resources.App.Domain.JobPosition), Name = "Name")]
    [Column(TypeName = "jsonb")]
    public LangStr Name { get; set; } = default!;

    public ICollection<Worker>? Workers { get; set; } = default!;
}
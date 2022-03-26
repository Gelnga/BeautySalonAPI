using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Resources = App.Resources.App.Domain;

namespace Domain.App;

public class JobPosition : BaseEntityId
{
    [MaxLength(256)]
    [Display(ResourceType = typeof(Resources.JobPosition), Name = "Name")]
    public String Name { get; set; } = default!;

    public ICollection<Worker>? Workers { get; set; } = default!;
}
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;

namespace Domain.App;

public class JobPosition : BaseEntityId
{
    [MaxLength(256)]
    public String Name { get; set; } = default!;

    public ICollection<Worker>? Workers { get; set; } = default!;
}
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;

namespace Domain.App;

public class Unit : BaseEntityId
{
    [MaxLength(256)]
    public String Name { get; set; } = default!;

    public ICollection<SalonService>? SalonServices { get; set; }
}
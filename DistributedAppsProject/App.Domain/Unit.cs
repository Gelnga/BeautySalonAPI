using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;
using Domain.App.Identity;

namespace App.Domain;

public class Unit : DomainEntityBaseMetaId<AppUser>
{
    [MaxLength(256)]
    [Display(ResourceType = typeof(Resources.App.Domain.Unit), Name = "Name")]
    [Column(TypeName = "jsonb")]
    public LangStr Name { get; set; } = default!;

    public ICollection<SalonService>? SalonServices { get; set; }
}
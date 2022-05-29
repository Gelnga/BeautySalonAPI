using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domain.Identity;
using Base.Domain;
using Domain.App.Identity;

namespace App.Domain;

public class Unit : DomainEntityBaseMetaId
{
    [MaxLength(256)]
    [Display(ResourceType = typeof(Resources.App.Domain.Unit), Name = "Name")]
    [Column(TypeName = "jsonb")]
    public LangStr Name { get; set; } = default!;

    [MaxLength(32)]
    public string UnitSymbolCode { get; set; } = default!;

    public ICollection<SalonService>? SalonServices { get; set; }
}
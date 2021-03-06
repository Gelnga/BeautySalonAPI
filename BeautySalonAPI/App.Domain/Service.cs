using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domain.Identity;
using Base.Domain;
using SalonService = App.Domain.SalonService;

namespace App.Domain;

public class Service : DomainEntityBaseMetaId
{
    [MaxLength(256)]
    [Display(ResourceType = typeof(Resources.App.Domain.Service), Name = "Name")]
    [Column(TypeName = "jsonb")]
    public LangStr Name { get; set; } = default!;

    [MaxLength(1024)]
    [Display(ResourceType = typeof(Resources.App.Domain.Service), Name = "Description")]
    [Column(TypeName = "jsonb")]
    public LangStr? Description { get; set; }

    public ICollection<SalonService>? SalonServices { get; set; }
    public ICollection<Appointment>? Appointments { get; set; }
}
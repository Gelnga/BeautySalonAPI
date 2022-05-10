using System.ComponentModel.DataAnnotations;
using Base.Domain;
using Resources = App.Resources.App.Domain;

namespace Domain.App;

public class SalonService : DomainEntityBaseMetaId
{
    public Guid SalonId { get; set; }
    [Display(ResourceType = typeof(Resources.SalonService), Name = "Salon")]
    public Salon? Salon { get; set; } = default!;
    
    public Guid ServiceId { get; set; }
    [Display(ResourceType = typeof(Resources.SalonService), Name = "Service")]
    public Service? Service { get; set; } = default!;

    public Guid UnitId { get; set; }
    [Display(ResourceType = typeof(Resources.SalonService), Name = "Unit")]
    public Unit? Unit { get; set; } = default!;

    [Display(ResourceType = typeof(Resources.SalonService), Name = "Price")]
    public int Price { get; set; }
}
using System.ComponentModel.DataAnnotations;
using Base.Domain;
using Domain.App.Identity;
using Salon = App.Domain.Salon;
using Unit = App.Domain.Unit;

namespace App.Domain;

public class SalonService : DomainEntityBaseMetaId<AppUser>
{
    public Guid SalonId { get; set; }
    [Display(ResourceType = typeof(Resources.App.Domain.SalonService), Name = "Salon")]
    public Salon? Salon { get; set; } = default!;
    
    public Guid ServiceId { get; set; }
    [Display(ResourceType = typeof(Resources.App.Domain.SalonService), Name = "Service")]
    public Service? Service { get; set; } = default!;

    public Guid UnitId { get; set; }
    [Display(ResourceType = typeof(Resources.App.Domain.SalonService), Name = "Unit")]
    public Unit? Unit { get; set; } = default!;

    [Display(ResourceType = typeof(Resources.App.Domain.SalonService), Name = "Price")]
    public int Price { get; set; }
}
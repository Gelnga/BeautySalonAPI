using Domain.App.Identity;

namespace Domain.App;

public class SalonService : BaseEntityId
{
    public Guid SalonId { get; set; }
    public Salon? Salon { get; set; } = default!;
    
    public Guid ServiceId { get; set; }
    public Service? Service { get; set; } = default!;

    public Guid UnitId { get; set; }
    public Unit? Unit { get; set; } = default!;

    public int Price { get; set; }
}
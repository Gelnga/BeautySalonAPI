using Domain.App.Identity;

namespace Domain.App;

public class SalonWorker : BaseEntityId
{
    public Guid SalonId { get; set; }
    public Salon? Salon { get; set; } = default!;

    public Guid WorkerId { get; set; }
    public Worker? Worker { get; set; } = default!;
}
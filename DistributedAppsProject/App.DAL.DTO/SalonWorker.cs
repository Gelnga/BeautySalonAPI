using App.DAL.DTO.Identity;
using Base.Domain;

namespace App.DAL.DTO;

public class SalonWorker : DomainEntityBaseId<AppUser>
{
    public Guid SalonId { get; set; }
    public Salon? Salon { get; set; } = default!;

    public Guid WorkerId { get; set; }
    public Worker? Worker { get; set; } = default!;
}
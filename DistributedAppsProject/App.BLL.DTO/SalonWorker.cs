using App.BLL.DTO.Identity;
using Base.Domain;

namespace App.BLL.DTO;

public class SalonWorker : DomainEntityBaseId
{
    public Guid SalonId { get; set; }
    public Salon? Salon { get; set; } = default!;

    public Guid WorkerId { get; set; }
    public Worker? Worker { get; set; } = default!;
    
    public ICollection<SalonService>? SalonServices { get; set; }
}
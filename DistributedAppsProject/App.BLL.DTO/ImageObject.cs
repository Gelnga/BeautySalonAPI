using App.DAL.DTO.Identity;
using Base.Domain;

namespace App.DAL.DTO;

public class ImageObject : DomainEntityBaseId<AppUser>
{ 
    public Guid ImageId { get; set; }
    public Image? Image { get; set; } = default!;

    public Guid? SalonId { get; set; }
    public Salon? Salon { get; set; }
    
    public Guid? ServiceId { get; set; }
    public Service? Service { get; set; }
    
    public Guid? WorkerId { get; set; }
    public Worker? Worker { get; set; }
}
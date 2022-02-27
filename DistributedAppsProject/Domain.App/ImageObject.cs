using Domain.App.Identity;

namespace Domain.App;

public class ImageObject : BaseEntityId
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
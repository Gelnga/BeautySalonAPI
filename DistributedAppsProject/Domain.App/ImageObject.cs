using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Resources = App.Resources.App.Domain;

namespace Domain.App;

public class ImageObject : BaseEntityId
{ 
    public Guid ImageId { get; set; }
    [Display(ResourceType = typeof(Resources.ImageObject), Name = "Image")]
    public Image? Image { get; set; } = default!;

    public Guid? SalonId { get; set; }
    [Display(ResourceType = typeof(Resources.ImageObject), Name = "Salon")]
    public Salon? Salon { get; set; }
    
    public Guid? ServiceId { get; set; }
    [Display(ResourceType = typeof(Resources.ImageObject), Name = "Service")]
    public Service? Service { get; set; }
    
    public Guid? WorkerId { get; set; }
    [Display(ResourceType = typeof(Resources.ImageObject), Name = "Worker")]
    public Worker? Worker { get; set; }
}
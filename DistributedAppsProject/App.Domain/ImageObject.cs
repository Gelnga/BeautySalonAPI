using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;
using Domain.App.Identity;
using Salon = App.Domain.Salon;
using Worker = App.Domain.Worker;

namespace App.Domain;

public class ImageObject : DomainEntityBaseMetaId
{ 
    public Guid ImageId { get; set; }
    [Display(ResourceType = typeof(Resources.App.Domain.ImageObject), Name = "Image")]
    public Image? Image { get; set; } = default!;

    public Guid? SalonId { get; set; }
    [Display(ResourceType = typeof(Resources.App.Domain.ImageObject), Name = "Salon")]
    public Salon? Salon { get; set; }
    
    public Guid? ServiceId { get; set; }
    [Display(ResourceType = typeof(Resources.App.Domain.ImageObject), Name = "Service")]
    public Service? Service { get; set; }
    
    public Guid? WorkerId { get; set; }
    [Display(ResourceType = typeof(Resources.App.Domain.ImageObject), Name = "Worker")]
    public Worker? Worker { get; set; }
}
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Resources = App.Resources.App.Domain;

namespace Domain.App;

public class Image : BaseEntityId
{
    [MaxLength(512)] 
    [Display(ResourceType = typeof(Resources.Image), Name = "ImageLink")]
    public String ImageLink { get; set; } = default!;
    
    [MaxLength(256)] 
    [Display(ResourceType = typeof(Resources.Image), Name = "Description")]
    public String Description { get; set; } = default!;

    public ICollection<ImageObject>? ImageObjects { get; set; }
}
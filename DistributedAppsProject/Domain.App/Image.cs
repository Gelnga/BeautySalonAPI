using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;

namespace Domain.App;

public class Image : BaseEntityId
{
    [MaxLength(512)] 
    public String ImageLink { get; set; } = default!;
    
    [MaxLength(256)] 
    public String Description { get; set; } = default!;

    public ICollection<ImageObject>? ImageObjects { get; set; }
}
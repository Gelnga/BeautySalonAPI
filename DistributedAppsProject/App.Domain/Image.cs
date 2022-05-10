using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;
using ImageObject = App.Domain.ImageObject;

namespace App.Domain;

public class Image : DomainEntityBaseMetaId

{
    [MaxLength(512)] 
    [Display(ResourceType = typeof(Resources.App.Domain.Image), Name = "ImageLink")]
    public string ImageLink { get; set; } = default!;
    
    [MaxLength(256)] 
    [Display(ResourceType = typeof(Resources.App.Domain.Image), Name = "Description")]
    [Column(TypeName = "jsonb")]
    public LangStr Description { get; set; } = default!;

    public ICollection<ImageObject>? ImageObjects { get; set; }
}
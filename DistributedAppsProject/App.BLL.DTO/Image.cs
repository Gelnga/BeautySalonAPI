using System.ComponentModel.DataAnnotations;
using App.BLL.DTO.Identity;
using Base.Domain;

namespace App.BLL.DTO;

public class Image : DomainEntityBaseId

{
    [MaxLength(512)] 
    public string ImageLink { get; set; } = default!;
    
    [MaxLength(256)] 
    public string Description { get; set; } = default!;

    public ICollection<ImageObject>? ImageObjects { get; set; }
}
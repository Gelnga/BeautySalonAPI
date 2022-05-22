using System.ComponentModel.DataAnnotations;
using App.DAL.DTO.Identity;
using Base.Domain;

namespace App.DAL.DTO;

public class Image : DomainEntityBaseId<AppUser>

{
    [MaxLength(512)] 
    public string ImageLink { get; set; } = default!;
    
    [MaxLength(256)] 
    public string Description { get; set; } = default!;

    public ICollection<ImageObject>? ImageObjects { get; set; }
}
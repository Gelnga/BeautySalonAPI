using System.ComponentModel.DataAnnotations;
using Base.Public.API;

namespace App.Public.DTO.v1;

public class Image : PublicDTOBase

{
    [MaxLength(512)] 
    public string ImageLink { get; set; } = default!;
    
    [MaxLength(256)] 
    public string Description { get; set; } = default!;
}
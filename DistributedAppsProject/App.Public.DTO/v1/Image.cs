using System.ComponentModel.DataAnnotations;
using App.BLL.DTO;
using App.BLL.DTO.Identity;
using Base.Domain;
using BasePublicAPI;

namespace App.Public.DTO.v1;

public class Image : PublicDTOBase

{
    [MaxLength(512)] 
    public string ImageLink { get; set; } = default!;
    
    [MaxLength(256)] 
    public string Description { get; set; } = default!;
}
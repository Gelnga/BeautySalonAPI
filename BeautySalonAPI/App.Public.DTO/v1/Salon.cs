using System.ComponentModel.DataAnnotations;
using Base.Public.API;

namespace App.Public.DTO.v1;

public class Salon : PublicDTOBase
{
    public Guid? WorkScheduleId { get; set; }

    [MaxLength(256)]
    public string Name { get; set; } = default!;

    [MaxLength(1024)]
    public string? Description { get; set; }

    [MaxLength(256)]
    public string Address { get; set; } = default!;

    [MaxLength(512)]
    public string? GoogleMapsLink { get; set; }

    [MaxLength(256)]
    public string? Email { get; set; }
    
    [MaxLength(256)] 
    public string? PhoneNumber { get; set; }
}
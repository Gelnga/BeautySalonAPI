using Base.Public.API;

namespace App.Public.DTO.v1;

public class SalonService : PublicDTOBase
{
    public Guid SalonId { get; set; }
    
    public Guid ServiceId { get; set; }

    public Guid UnitId { get; set; }
    
    public Guid SalonWorkerId { get; set; }

    public string ServiceDuration { get; set; } = default!;

    public int Price { get; set; }
    
}
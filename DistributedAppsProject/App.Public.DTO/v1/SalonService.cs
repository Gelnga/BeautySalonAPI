using App.BLL.DTO;
using App.BLL.DTO.Identity;
using Base.Domain;
using BasePublicAPI;

namespace App.Public.DTO.v1;

public class SalonService : PublicDTOBase
{
    public Guid SalonId { get; set; }
    
    public Guid ServiceId { get; set; }

    public Guid UnitId { get; set; }

    public int Price { get; set; }
}
using App.BLL.DTO;
using App.BLL.DTO.Identity;
using Base.Domain;
using BasePublicAPI;

namespace App.Public.DTO.v1;

public class SalonWorker : PublicDTOBase
{
    public Guid SalonId { get; set; }

    public Guid WorkerId { get; set; }
}
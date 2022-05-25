using Base.Public.API;

namespace App.Public.DTO.v1;

public class SalonWorker : PublicDTOBase
{
    public Guid SalonId { get; set; }

    public Guid WorkerId { get; set; }
}
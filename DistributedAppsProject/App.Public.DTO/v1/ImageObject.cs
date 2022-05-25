using Base.Public.API;

namespace App.Public.DTO.v1;

public class ImageObject : PublicDTOBase
{ 
    public Guid ImageId { get; set; }

    public Guid? SalonId { get; set; }
    
    public Guid? ServiceId { get; set; }
    
    public Guid? WorkerId { get; set; }
}
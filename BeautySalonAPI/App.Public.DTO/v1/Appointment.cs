using App.BLL.DTO;
using App.BLL.DTO.Identity;
using Base.Domain;
using Base.Public.API;

namespace App.Public.DTO.v1;

public class Appointment : PublicDTOBase
{
    public Guid SalonId { get; set; }
    
    public Guid ServiceId { get; set; }
    
    public Guid WorkerId { get; set; }

    public string AppointmentDate { get; set; } = default!;
    public string AppointmentStart { get; set; } = default!;
    public string AppointmentEnd { get; set; } = default!;
    public string Price { get; set; } = default!;
}
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

    public DateTime DateRegistered { get; set; }
    public DateTime DateAppointmentStart { get; set; }
    public DateTime DateAppointmentEnd { get; set; }
}
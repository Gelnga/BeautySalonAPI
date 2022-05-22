using App.BLL.DTO.Identity;
using Base.Domain;

namespace App.BLL.DTO;

public class Appointment : DomainEntityBaseId<AppUser>
{
    public Guid SalonId { get; set; }
    public Salon? Salon { get; set; } = default!;
    
    public Guid ServiceId { get; set; }
    public Service? Service { get; set; } = default!;
    
    public Guid WorkerId { get; set; }
    public Worker? Worker { get; set; } = default!;

    public DateTime DateRegistered { get; set; }
    public DateTime DateAppointmentStart { get; set; }
    public DateTime DateAppointmentEnd { get; set; }
}
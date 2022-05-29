using App.DAL.DTO.Identity;
using Base.Domain;

namespace App.DAL.DTO;

public class Appointment : DomainEntityBaseId
{
    public Guid SalonId { get; set; }
    public Salon? Salon { get; set; } = default!;
    
    public Guid ServiceId { get; set; }
    public Service? Service { get; set; } = default!;
    
    public Guid WorkerId { get; set; }
    public Worker? Worker { get; set; } = default!;

    public DateOnly AppointmentDate { get; set; }
    public TimeOnly AppointmentStart { get; set; }
    public TimeOnly AppointmentEnd { get; set; }
}
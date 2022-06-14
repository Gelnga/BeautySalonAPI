using App.BLL.DTO.Identity;
using Base.Domain;

namespace App.BLL.DTO;

public class Appointment : DomainEntityBaseId
{
    public Guid SalonId { get; set; }
    public Salon? Salon { get; set; } = default!;
    
    public Guid ServiceId { get; set; }
    public Service? Service { get; set; } = default!;
    
    public Guid WorkerId { get; set; }
    public Worker? Worker { get; set; } = default!;

    public DateOnly AppointmentDate { get; set; }
    public TimeSpan AppointmentStart { get; set; }
    public TimeSpan AppointmentEnd { get; set; }
    public string Price { get; set; } = default!;
}
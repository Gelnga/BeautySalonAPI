using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;
using Domain.App.Identity;
using Salon = App.Domain.Salon;
using Service = App.Domain.Service;
using Worker = App.Domain.Worker;

namespace App.Domain;

public class Appointment : DomainEntityBaseMetaId
{
    public Guid SalonId { get; set; }
    [Display(ResourceType = typeof(Resources.App.Domain.Appointment), Name = "Salon")]
    public Salon? Salon { get; set; } = default!;
    
    public Guid ServiceId { get; set; }
    [Display(ResourceType = typeof(Resources.App.Domain.Appointment), Name = "Service")]
    public Service? Service { get; set; } = default!;
    
    public Guid WorkerId { get; set; }
    [Display(ResourceType = typeof(Resources.App.Domain.Appointment), Name = "Worker")]
    public Worker? Worker { get; set; } = default!;

    [Display(ResourceType = typeof(Resources.App.Domain.Appointment), Name = "DateRegistered")]
    public DateOnly AppointmentDate { get; set; }
    
    [Display(ResourceType = typeof(Resources.App.Domain.Appointment), Name = "DateAppointmentStart")]
    public TimeOnly AppointmentStart { get; set; }
    
    [Display(ResourceType = typeof(Resources.App.Domain.Appointment), Name = "DateAppointmentEnd")]
    public TimeOnly AppointmentEnd { get; set; }
}
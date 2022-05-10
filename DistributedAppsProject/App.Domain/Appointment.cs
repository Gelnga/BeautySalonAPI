using System.ComponentModel.DataAnnotations;
using Base.Domain;
using Domain.App.Identity;
using Salon = Domain.App.Salon;
using Service = App.Domain.Service;
using Worker = App.Domain.Worker;

namespace App.Domain;

public class Appointment : DomainEntityBaseMetaId
{
    [Display(ResourceType = typeof(Resources.App.Domain.Appointment), Name = "User")]
    public Guid UserId { get; set; }
    public AppUser? AppUser { get; set; } = default!;
    
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
    public DateTime DateRegistered { get; set; }
    
    [Display(ResourceType = typeof(Resources.App.Domain.Appointment), Name = "DateAppointmentStart")]
    public DateTime DateAppointmentStart { get; set; }
    
    [Display(ResourceType = typeof(Resources.App.Domain.Appointment), Name = "DateAppointmentEnd")]
    public DateTime DateAppointmentEnd { get; set; }
}
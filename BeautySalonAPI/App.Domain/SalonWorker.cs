using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;
using Salon = App.Domain.Salon;
using Worker = App.Domain.Worker;

namespace App.Domain;

public class SalonWorker : DomainEntityBaseMetaId
{
    public Guid SalonId { get; set; }
    [Display(ResourceType = typeof(Resources.App.Domain.SalonWorker), Name = "Salon")]
    public Salon? Salon { get; set; } = default!;

    public Guid WorkerId { get; set; }
    [Display(ResourceType = typeof(Resources.App.Domain.SalonWorker), Name = "Worker")]
    public Worker? Worker { get; set; } = default!;
    
    public ICollection<SalonService>? SalonServices { get; set; }
}
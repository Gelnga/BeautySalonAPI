using System.ComponentModel.DataAnnotations;
using Base.Domain;
using Salon = Domain.App.Salon;
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
}
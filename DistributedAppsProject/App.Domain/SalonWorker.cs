using System.ComponentModel.DataAnnotations;
using Base.Domain;
using Resources = App.Resources.App.Domain;

namespace Domain.App;

public class SalonWorker : DomainEntityBaseMetaId
{
    public Guid SalonId { get; set; }
    [Display(ResourceType = typeof(Resources.SalonWorker), Name = "Salon")]
    public Salon? Salon { get; set; } = default!;

    public Guid WorkerId { get; set; }
    [Display(ResourceType = typeof(Resources.SalonWorker), Name = "Worker")]
    public Worker? Worker { get; set; } = default!;
}
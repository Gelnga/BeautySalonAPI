using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Resources = App.Resources.App.Domain;

namespace Domain.App;

public class SalonWorker : BaseEntityId
{
    public Guid SalonId { get; set; }
    [Display(ResourceType = typeof(Resources.SalonWorker), Name = "Salon")]
    public Salon? Salon { get; set; } = default!;

    public Guid WorkerId { get; set; }
    [Display(ResourceType = typeof(Resources.SalonWorker), Name = "Worker")]
    public Worker? Worker { get; set; } = default!;
}
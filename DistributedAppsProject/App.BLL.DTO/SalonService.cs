using App.BLL.DTO.Identity;
using Base.Domain;

namespace App.BLL.DTO;

public class SalonService : DomainEntityBaseId
{
    public Guid SalonId { get; set; }
    public Salon? Salon { get; set; }
    
    public Guid ServiceId { get; set; }
    public Service? Service { get; set; }
    
    public Guid SalonWorkerId { get; set; }
    public SalonWorker? SalonWorker { get; set; }

    public Guid UnitId { get; set; }
    public Unit? Unit { get; set; }

    public int Price { get; set; }
    public TimeSpan ServiceDuration { get; set; }
    
    public ICollection<SalonService>? SalonServices { get; set; }
}
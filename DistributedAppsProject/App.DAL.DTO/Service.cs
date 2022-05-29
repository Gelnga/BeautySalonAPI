using System.ComponentModel.DataAnnotations;
using App.DAL.DTO.Identity;
using Base.Domain;

namespace App.DAL.DTO;

public class Service : DomainEntityBaseId
{
    [MaxLength(256)]
    public string Name { get; set; } = default!;

    [MaxLength(1024)]
    public string? Description { get; set; }
    
    public int? ServiceDurationInHours { get; set; }
    
    public int? Price { get; set; }

    public string? UnitName { get; set; } = default!;

    public ICollection<SalonService>? SalonServices { get; set; }
    public ICollection<Appointment>? Appointments { get; set; }
    public ICollection<ImageObject>? ImageObjects { get; set; }
}
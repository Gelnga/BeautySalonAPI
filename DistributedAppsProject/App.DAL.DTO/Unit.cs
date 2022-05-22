using System.ComponentModel.DataAnnotations;
using App.DAL.DTO.Identity;
using Base.Domain;

namespace App.DAL.DTO;

public class Unit : DomainEntityBaseId<AppUser>
{
    [MaxLength(256)]
    public string Name { get; set; } = default!;

    public ICollection<SalonService>? SalonServices { get; set; }
}
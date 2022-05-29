using System.ComponentModel.DataAnnotations;
using App.BLL.DTO.Identity;
using Base.Domain;

namespace App.BLL.DTO;

public class Unit : DomainEntityBaseId
{
    [MaxLength(256)]
    public string Name { get; set; } = default!;
    
    [MaxLength(32)]
    public string UnitSymbolCode { get; set; } = default!;

    public ICollection<SalonService>? SalonServices { get; set; }
}
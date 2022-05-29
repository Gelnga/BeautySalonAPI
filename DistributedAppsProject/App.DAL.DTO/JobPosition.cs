using System.ComponentModel.DataAnnotations;
using App.DAL.DTO.Identity;
using Base.Domain;

namespace App.DAL.DTO;

public class JobPosition : DomainEntityBaseId
{
    [MaxLength(256)]
    public string Name { get; set; } = default!;

    public ICollection<Worker>? Workers { get; set; } = default!;
}
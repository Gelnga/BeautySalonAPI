using System.ComponentModel.DataAnnotations;
using Base.Domain;
using AppUser = App.BLL.DTO.Identity.AppUser;
using Worker = App.Domain.Worker;

namespace App.BLL.DTO;

public class JobPosition : DomainEntityBaseId<AppUser>
{
    [MaxLength(256)]
    public string Name { get; set; } = default!;

    public ICollection<Worker>? Workers { get; set; } = default!;
}
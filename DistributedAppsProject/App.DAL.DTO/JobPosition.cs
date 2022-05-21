using System.ComponentModel.DataAnnotations;
using Base.Domain;
using AppUser = App.DAL.DTO.Identity.AppUser;
using Worker = App.Domain.Worker;

namespace App.DAL.DTO;

public class JobPosition : DomainEntityBaseId<AppUser>
{
    [MaxLength(256)]
    [Display(ResourceType = typeof(Resources.App.Domain.JobPosition), Name = "Name")]
    public string Name { get; set; } = default!;

    public ICollection<Worker>? Workers { get; set; } = default!;
}
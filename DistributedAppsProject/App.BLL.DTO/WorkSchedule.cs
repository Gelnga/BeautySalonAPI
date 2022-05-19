using System.ComponentModel.DataAnnotations;
using App.Domain;
using Base.Domain;
using AppUser = App.BLL.DTO.Identity.AppUser;
using Salon = App.Domain.Salon;

namespace App.BLL.DTO;

public class WorkSchedule : DomainEntityBaseMetaId<AppUser>
{
    [MaxLength(256)]
    [Display(ResourceType = typeof(Resources.App.Domain.WorkSchedule), Name = "Name")]
    public string Name { get; set; } = default!;
    [Display(ResourceType = typeof(Resources.App.Domain.WorkSchedule), Name = "IsWeek")]
    public bool IsWeek { get; set; }

    public ICollection<BLL.DTO.WorkDay>? WorkDays { get; set; }
    public ICollection<Salon>? Salons { get; set; }
    public ICollection<Worker>? Workers { get; set; }
}
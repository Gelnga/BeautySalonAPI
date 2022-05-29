using System.ComponentModel.DataAnnotations;
using App.Domain;
using Base.Domain;
using AppUser = App.BLL.DTO.Identity.AppUser;
using Salon = App.Domain.Salon;

namespace App.BLL.DTO;

public class WorkSchedule : DomainEntityBaseId
{
    [MaxLength(256)]
    public string Name { get; set; } = default!;
    public bool IsWeek { get; set; }

    public ICollection<BLL.DTO.WorkDay>? WorkDays { get; set; }
    public ICollection<Salon>? Salons { get; set; }
    public ICollection<Worker>? Workers { get; set; }
}
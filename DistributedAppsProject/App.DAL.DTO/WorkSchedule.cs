using System.ComponentModel.DataAnnotations;
using App.DAL.DTO.Identity;
using Base.Domain;

namespace App.DAL.DTO;

public class WorkSchedule : DomainEntityBaseId<AppUser>
{
    [MaxLength(256)]
    public string Name { get; set; } = default!;
    public bool IsWeek { get; set; }

    public ICollection<WorkDay>? WorkDays { get; set; }
    public ICollection<Salon>? Salons { get; set; }
    public ICollection<Worker>? Workers { get; set; }
}
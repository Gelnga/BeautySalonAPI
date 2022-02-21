using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.App.Identity;

public class AppUser : IdentityUser<Guid>
{
    [MaxLength(128)]
    public String? FirstName { get; set; } = default!;
    
    [MaxLength(128)]
    public String? LastName { get; set; } = default!;

    public int? RegisteredAppointments { get; set; }

    public ICollection<Appointment>? Appointments { get; set; }
}
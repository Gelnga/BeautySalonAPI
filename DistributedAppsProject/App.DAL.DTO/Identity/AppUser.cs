using App.Domain;
using App.Domain.Identity;
using Base.Domain.Identity;

namespace App.DAL.DTO.Identity;

public class AppUser : BaseUser
{
    public Guid? WorkerId { get; set; }
    public Worker? Worker { get; set; }
    
    public int? RegisteredAppointments { get; set; }

    public ICollection<Appointment>? Appointments { get; set; }
    
    public ICollection<RefreshToken>? RefreshTokens { get; set; }
}
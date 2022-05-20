using Base.Domain.Identity;

namespace App.Domain.Identity;

public class AppUser : BaseUser
{
    public int? RegisteredAppointments { get; set; }

    public ICollection<Appointment>? Appointments { get; set; }
    public ICollection<RefreshToken>? RefreshTokens { get; set; }
}
using Base.Domain.Identity;

namespace App.Public.DTO.v1.Identity;

public class AppUser : BaseUser
{
    public int? RegisteredAppointments { get; set; }
}
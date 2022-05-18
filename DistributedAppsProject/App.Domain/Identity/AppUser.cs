using System.ComponentModel.DataAnnotations;
using App.Domain;
using App.Domain.Identity;
using Base.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace Domain.App.Identity;

public class AppUser : BaseUser
{
    public int? RegisteredAppointments { get; set; }

    public ICollection<Appointment>? Appointments { get; set; }
    
    public ICollection<RefreshToken>? RefreshTokens { get; set; }
}
using System.ComponentModel.DataAnnotations;
using Base.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace Domain.App.Identity;

public class AppUser : BaseUser
{
    public int? RegisteredAppointments { get; set; }

    public ICollection<Appointment>? Appointments { get; set; }
}
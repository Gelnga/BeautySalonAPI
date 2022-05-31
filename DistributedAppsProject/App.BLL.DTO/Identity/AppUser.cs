using System.ComponentModel.DataAnnotations.Schema;
using App.Domain;
using App.Domain.Identity;
using Base.Domain;
using Base.Domain.Identity;

namespace App.BLL.DTO.Identity;

public class AppUser : BaseUser
{
    public Guid? WorkerId { get; set; }
    public Worker? Worker { get; set; }
    
    public string? FirstName { get; set; } = default!;

    public string? LastName { get; set; } = default!;

    public ICollection<Appointment>? Appointments { get; set; }
    
    public ICollection<RefreshToken>? RefreshTokens { get; set; }
}
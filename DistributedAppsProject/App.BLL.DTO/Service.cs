﻿using System.ComponentModel.DataAnnotations;
using App.BLL.DTO.Identity;
using Base.Domain;

namespace App.BLL.DTO;

public class Service : DomainEntityBaseId<AppUser>
{
    [MaxLength(256)]
    public string Name { get; set; } = default!;

    [MaxLength(1024)]
    public string? Description { get; set; }

    public ICollection<SalonService>? SalonServices { get; set; }
    public ICollection<Appointment>? Appointments { get; set; }
    public ICollection<ImageObject>? ImageObjects { get; set; }
}
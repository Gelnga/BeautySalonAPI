﻿using System.ComponentModel.DataAnnotations;
using App.DAL.DTO.Identity;
using Base.Domain;

namespace App.DAL.DTO;

public class Salon : DomainEntityBaseId<AppUser>
{
    public Guid? WorkScheduleId { get; set; }
    public WorkSchedule? WorkSchedule { get; set; }

    [MaxLength(256)]
    public string Name { get; set; } = default!;

    [MaxLength(1024)]
    public string? Description { get; set; }

    [MaxLength(256)]
    public string Address { get; set; } = default!;

    [MaxLength(512)]
    public string? GoogleMapsLink { get; set; }

    [MaxLength(256)]
    public string? Email { get; set; }
    
    [MaxLength(256)] 
    public string? PhoneNumber { get; set; }

    public ICollection<SalonService>? SalonServices { get; set; }
    public ICollection<SalonWorker>? SalonWorkers { get; set; }
    public ICollection<Appointment>? Appointments { get; set; }
    public ICollection<ImageObject>? ImageObjects { get; set; }
}
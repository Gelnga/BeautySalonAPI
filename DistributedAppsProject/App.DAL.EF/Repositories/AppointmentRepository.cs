using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using App.DAL.DTO.Identity;
using Base.Contracts.Base;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class AppointmentRepository :
    BaseEntityRepository<Appointment, App.Domain.Appointment, ApplicationDbContext, AppUser,
        App.Domain.Identity.AppUser>, IAppointmentRepository
{
    public AppointmentRepository(ApplicationDbContext dbContext, IMapper<Appointment, App.Domain.Appointment> mapper) :
        base(dbContext, mapper)
    {
    }
}
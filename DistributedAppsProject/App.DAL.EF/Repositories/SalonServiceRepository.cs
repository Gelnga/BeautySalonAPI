using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using App.DAL.DTO.Identity;
using Base.Contracts.Base;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class SalonServiceRepository :
    BaseEntityRepository<SalonService, App.Domain.SalonService, ApplicationDbContext, AppUser,
        App.Domain.Identity.AppUser>, ISalonServiceRepository
{
    public SalonServiceRepository(ApplicationDbContext dbContext, IMapper<SalonService, Domain.SalonService> mapper) :
        base(dbContext, mapper)
    {
    }
}
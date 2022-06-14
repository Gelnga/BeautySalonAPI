using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using App.DAL.DTO.Identity;
using Base.Contracts.Base;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class SalonRepository :
    BaseEntityRepository<Salon, App.Domain.Salon, ApplicationDbContext>, ISalonRepository
{
    public SalonRepository(ApplicationDbContext dbContext, IMapper<Salon, Domain.Salon> mapper) : base(dbContext, mapper)
    {
    }
}
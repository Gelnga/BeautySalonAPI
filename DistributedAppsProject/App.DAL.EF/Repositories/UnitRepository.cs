using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using App.DAL.DTO.Identity;
using Base.Contracts.Base;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class UnitRepository :
    BaseEntityRepository<Unit, App.Domain.Unit, ApplicationDbContext, AppUser,
        App.Domain.Identity.AppUser>, IUnitRepository
{
    public UnitRepository(ApplicationDbContext dbContext, IMapper<Unit, Domain.Unit> mapper) : base(dbContext, mapper)
    {
    }
}
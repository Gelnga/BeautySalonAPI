using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using App.DAL.DTO.Identity;
using Base.Contracts.Base;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class ServiceRepository :
    BaseEntityRepository<Service, App.Domain.Service, ApplicationDbContext, AppUser,
        App.Domain.Identity.AppUser>, IServiceRepository
{
    public ServiceRepository(ApplicationDbContext dbContext, IMapper<Service, Domain.Service> mapper) : base(dbContext,
        mapper)
    {
    }
}
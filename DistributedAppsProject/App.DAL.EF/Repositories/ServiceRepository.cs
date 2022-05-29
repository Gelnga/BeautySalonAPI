using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using App.DAL.DTO.Identity;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ServiceRepository :
    BaseEntityRepository<Service, App.Domain.Service, ApplicationDbContext>, IServiceRepository
{
    public ServiceRepository(ApplicationDbContext dbContext, IMapper<Service, Domain.Service> mapper) : base(dbContext,
        mapper)
    {
    }

    public async Task<ICollection<Service>> GetServicesBySalonId(Guid id)
    {
        var res = await RepoDbContext.SalonServices
            .Include(e => e.Unit)
            .Include(e => e.Service)
            .Where(e => e.SalonId == id)
            .Select(e => MapService(e, Mapper))
            .ToListAsync();

        return res;
    }

    private static Service MapService(App.Domain.SalonService e, IMapper<Service, Domain.Service> mapper)
    {
        var mapped = mapper.Map(e.Service)!;
        mapped.Price = e.Price;
        mapped.UnitName = e.Unit!.Name;
        return mapped;
    }
}
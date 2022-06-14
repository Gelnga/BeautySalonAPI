using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class ServiceService : BaseEntityService<App.BLL.DTO.Service, App.DAL.DTO.Service, IServiceRepository>,
    IServiceService
{
    public ServiceService(IServiceRepository repository, IMapper<Service, DAL.DTO.Service> mapper) : base(repository,
        mapper)
    {
    }

    public async Task<ICollection<Service>> GetServicesBySalonId(Guid id)
    {
        var res = await Repository.GetServicesBySalonId(id);
        return res
            .Select(e => Mapper.Map(e)!).ToList();
    }
}
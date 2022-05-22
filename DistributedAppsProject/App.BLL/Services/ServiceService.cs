using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
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
}
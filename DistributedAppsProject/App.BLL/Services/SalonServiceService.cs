using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class SalonServiceService :
    BaseEntityService<App.BLL.DTO.SalonService, App.DAL.DTO.SalonService, ISalonServiceRepository>, ISalonServiceService
{
    public SalonServiceService(ISalonServiceRepository repository,
        IMapper<DTO.SalonService, DAL.DTO.SalonService> mapper) : base(repository, mapper)
    {
    }
}
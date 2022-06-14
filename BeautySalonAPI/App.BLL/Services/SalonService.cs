using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class SalonService : BaseEntityService<App.BLL.DTO.Salon, App.DAL.DTO.Salon, ISalonRepository>, ISalonService
{
    public SalonService(ISalonRepository repository, IMapper<Salon, DAL.DTO.Salon> mapper) : base(repository, mapper)
    {
    }
}
using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class SalonWorkerService :
    BaseEntityService<App.BLL.DTO.SalonWorker, App.DAL.DTO.SalonWorker, ISalonWorkerRepository>, ISalonWorkerService
{
    public SalonWorkerService(ISalonWorkerRepository repository, IMapper<SalonWorker, DAL.DTO.SalonWorker> mapper) :
        base(repository, mapper)
    {
    }
}
using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class WorkerService : BaseEntityService<App.BLL.DTO.Worker, App.DAL.DTO.Worker, IWorkerRepository>,
    IWorkerService
{
    public WorkerService(IWorkerRepository repository, IMapper<Worker, DAL.DTO.Worker> mapper) : base(repository,
        mapper)
    {
    }
}
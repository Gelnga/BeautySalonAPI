using App.BLL.DTO;
using App.Contracts.BLL.Services;
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

    public async Task<ICollection<Worker>> GetWorkersBySalonIdAndServiceId(Guid salonId, Guid serviceId)
    {
        var workers = await Repository.GetWorkersWithSalonServices();
        return workers.Where(w => w.SalonWorkers!
                .Any(sw => sw.SalonServices!
                    .Any(ss => ss.SalonId == salonId && ss.ServiceId == serviceId)))
            .Select(w => MapWorker(w, Mapper, salonId, serviceId))
            .ToList();
    }

    private static Worker MapWorker(App.DAL.DTO.Worker worker, IMapper<Worker, DAL.DTO.Worker> mapper, Guid salonId, Guid serviceId)
    {
        var mapped = mapper.Map(worker)!;
        var salonServiceData = worker.SalonWorkers!
            .Select(e => e.SalonServices
                !.First(ss => ss.SalonId == salonId && ss.ServiceId == serviceId))
            .First(e => true);

        mapped.Price = salonServiceData.Price;
        mapped.UnitName = salonServiceData.Unit!.Name;
        return mapped;
    }
}
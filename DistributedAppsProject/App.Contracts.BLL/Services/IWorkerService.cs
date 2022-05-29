using Base.Contracts.Base;

namespace App.Contracts.BLL.Services;

public interface IWorkerService : IEntityService<App.BLL.DTO.Worker>
{
    public Task<ICollection<App.BLL.DTO.Worker>> GetWorkersBySalonIdAndServiceId(Guid salonId, Guid workerId);
}
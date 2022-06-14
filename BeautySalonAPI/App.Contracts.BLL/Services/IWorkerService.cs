using Base.Contracts.Base;

namespace App.Contracts.BLL.Services;

public interface IWorkerService : IEntityService<App.BLL.DTO.Worker>
{
    public Task<ICollection<App.BLL.DTO.Worker>> GetWorkersBySalonIdAndServiceId(Guid salonId, Guid workerId);

    public Task<List<Dictionary<string, TimeSpan>>> GetWorkerAvailableTimes(Guid id, DateOnly date,
        TimeSpan serviceDuration);

    public Task<ICollection<App.BLL.DTO.Appointment>> GetWorkerAppointments(Guid workerId);
}
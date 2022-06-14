using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IWorkerRepository : IEntityRepository<Worker>
{
    public Task<ICollection<Worker>> GetWorkersWithSalonServices();

    public Task<Worker> GetWorkerWithAppointmentsAndSchedule(Guid id);

    public Task<Worker> GetWorkerWithAppointments(Guid id);
}
using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class WorkerRepository :
    BaseEntityRepository<Worker, App.Domain.Worker, ApplicationDbContext>, IWorkerRepository
{
    public WorkerRepository(ApplicationDbContext dbContext, IMapper<Worker, Domain.Worker> mapper) : base(dbContext,
        mapper)
    {
    }

    public async Task<ICollection<Worker>> GetWorkersWithSalonServices()
    {
        return await RepoDbContext.Workers
            .Include(e => e.SalonWorkers)!
            .ThenInclude(e => e.SalonServices)!
            .ThenInclude(e => e.Unit)
            .Select(e => Mapper.Map(e)!)
            .ToListAsync();
    }

    public async Task<Worker> GetWorkerWithAppointmentsAndSchedule(Guid id)
    {
        var res = RepoDbContext.Workers
            .Include(e => e.Appointments)
            .Include(e => e.WorkSchedule)
            .ThenInclude(e => e!.WorkDays);
        var res1 = await res.ToListAsync();
        var res2 = res1.First(e => e.Id == id);
        return Mapper.Map(res2)!;
    }

    public async Task<Worker> GetWorkerWithAppointments(Guid id)
    {
        var res = RepoDbContext.Workers
            .Include(e => e.Appointments);
        var res1 = await res.ToListAsync();
        return Mapper.Map(res1.First(e => e.Id == id))!;
    }
}
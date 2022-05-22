using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using App.DAL.DTO.Identity;
using Base.Contracts.Base;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class WorkerRepository :
    BaseEntityRepository<Worker, App.Domain.Worker, ApplicationDbContext, AppUser,
        App.Domain.Identity.AppUser>, IWorkerRepository
{
    public WorkerRepository(ApplicationDbContext dbContext, IMapper<Worker, Domain.Worker> mapper) : base(dbContext,
        mapper)
    {
    }
}
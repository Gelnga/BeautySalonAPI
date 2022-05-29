using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using App.DAL.DTO.Identity;
using Base.Contracts.Base;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class SalonWorkerRepository :
    BaseEntityRepository<SalonWorker, App.Domain.SalonWorker, ApplicationDbContext>, ISalonWorkerRepository
{
    public SalonWorkerRepository(ApplicationDbContext dbContext, IMapper<SalonWorker, Domain.SalonWorker> mapper) :
        base(dbContext, mapper)
    {
    }
}
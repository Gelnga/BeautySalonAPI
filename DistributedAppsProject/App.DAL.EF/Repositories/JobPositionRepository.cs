using App.Contracts.DAL;
using Base.DAL.EF;
using App.DAL.DTO.Identity;
using Base.Contracts.Base;
using JobPosition = App.DAL.DTO.JobPosition;

namespace App.DAL.EF.Repositories;

public class JobPositionRepository :
    BaseEntityRepository<JobPosition, App.Domain.JobPosition, ApplicationDbContext, AppUser,
        App.Domain.Identity.AppUser>, IJobPositionRepository
{
    public JobPositionRepository(ApplicationDbContext dbContext, IMapper<JobPosition, App.Domain.JobPosition> mapper) :
        base(dbContext, mapper)
    {
    }
}
using App.Contracts.DAL;
using Base.DAL.EF;
using JobPosition = App.Domain.JobPosition;

namespace App.DAL.EF.Repositories;

public class JobPositionRepository : BaseEntityRepository<JobPosition, ApplicationDbContext>, IJobPositionRepository
{
    public JobPositionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
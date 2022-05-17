using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class WorkDayRepository : BaseEntityRepository<WorkDay, ApplicationDbContext>, IWorkDayRepository
{
    public WorkDayRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
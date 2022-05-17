using App.Contracts.DAL;
using Base.DAL.EF;
using WorkSchedule = App.Domain.WorkSchedule;

namespace App.DAL.EF.Repositories;

public class WorkScheduleRepository : BaseEntityRepository<WorkSchedule, ApplicationDbContext>, IWorkScheduleRepository
{
    public WorkScheduleRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
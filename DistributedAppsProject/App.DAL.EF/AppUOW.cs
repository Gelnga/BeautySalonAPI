using App.Contracts.DAL;
using App.DAL.EF.Repositories;
using Base.DAL.EF;

namespace App.DAL.EF;

public class AppUOW : BaseUOW<ApplicationDbContext>, IAppUnitOfWork
{
    public AppUOW(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    private IJobPositionRepository? _jobPositions;
    public virtual IJobPositionRepository JobPositions =>
        _jobPositions ??= new JobPositionRepository(UOWDbContext);

    private IWorkDayRepository? _workDays;
    public virtual IWorkDayRepository WorkDays =>
        _workDays ??= new WorkDayRepository(UOWDbContext);

    private IWorkScheduleRepository? _workSchedules;
    public virtual IWorkScheduleRepository WorkSchedules =>
        _workSchedules ??= new WorkScheduleRepository(UOWDbContext);
}
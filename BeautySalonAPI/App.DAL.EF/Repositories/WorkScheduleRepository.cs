using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DAL.DTO.Identity;
using Base.Contracts.Base;
using Base.DAL.EF;
using WorkSchedule = App.DAL.DTO.WorkSchedule;

namespace App.DAL.EF.Repositories;

public class WorkScheduleRepository :
    BaseEntityRepository<WorkSchedule, App.Domain.WorkSchedule, ApplicationDbContext>, IWorkScheduleRepository
{
    public WorkScheduleRepository(ApplicationDbContext dbContext, IMapper<WorkSchedule, App.Domain.WorkSchedule> mapper)
        : base(dbContext, mapper)
    {
    }
}
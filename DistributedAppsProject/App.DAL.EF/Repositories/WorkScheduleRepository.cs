using App.Contracts.DAL;
using Base.DAL.EF;
using Domain.App.Identity;
using Microsoft.EntityFrameworkCore;
using WorkSchedule = App.Domain.WorkSchedule;

namespace App.DAL.EF.Repositories;

public class WorkScheduleRepository : BaseEntityRepository<WorkSchedule, ApplicationDbContext, AppUser>, IWorkScheduleRepository
{
    public WorkScheduleRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
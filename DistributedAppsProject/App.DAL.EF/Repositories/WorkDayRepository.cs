using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;
using Domain.App.Identity;

namespace App.DAL.EF.Repositories;

public class WorkDayRepository : BaseEntityRepository<WorkDay, ApplicationDbContext, AppUser>, IWorkDayRepository
{
    public WorkDayRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
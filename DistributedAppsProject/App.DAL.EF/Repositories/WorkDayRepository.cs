using App.Contracts.DAL;
using Base.DAL.EF;
using App.DAL.DTO.Identity;
using Base.Contracts.Base;
using WorkDay = App.DAL.DTO.WorkDay;

namespace App.DAL.EF.Repositories;

public class WorkDayRepository : BaseEntityRepository<WorkDay, App.Domain.WorkDay, ApplicationDbContext, AppUser, App.Domain.Identity.AppUser>, IWorkDayRepository
{
    public WorkDayRepository(ApplicationDbContext dbContext, IMapper<WorkDay, App.Domain.WorkDay> mapper) :
        base(dbContext, mapper)
    {
    }
}
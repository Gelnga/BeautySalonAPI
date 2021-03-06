using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using Base.DAL.EF;
using App.DAL.DTO.Identity;
using Base.Contracts.Base;
using WorkDay = App.DAL.DTO.WorkDay;

namespace App.DAL.EF.Repositories;

public class WorkDayRepository :
    BaseEntityRepository<WorkDay, App.Domain.WorkDay, ApplicationDbContext>,
    IWorkDayRepository
{
    public WorkDayRepository(ApplicationDbContext dbContext, IMapper<WorkDay, App.Domain.WorkDay> mapper) :
        base(dbContext, mapper)
    {
    }
}
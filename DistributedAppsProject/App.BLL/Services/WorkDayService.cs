using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class WorkDayService : BaseEntityService<App.BLL.DTO.WorkDay, App.DAL.DTO.WorkDay, IWorkDayRepository>, IWorkDayService
{
    public WorkDayService(IWorkDayRepository repository, IMapper<WorkDay, DAL.DTO.WorkDay> mapper) : base(repository, mapper)
    {
    }
}
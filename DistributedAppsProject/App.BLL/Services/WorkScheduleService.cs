using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class WorkScheduleService : BaseEntityService<App.BLL.DTO.WorkSchedule, App.DAL.DTO.WorkSchedule, IWorkScheduleRepository>, IWorkScheduleService
{
    public WorkScheduleService(IWorkScheduleRepository repository, IMapper<WorkSchedule, DAL.DTO.WorkSchedule> mapper) : base(repository, mapper)
    {
    }
}
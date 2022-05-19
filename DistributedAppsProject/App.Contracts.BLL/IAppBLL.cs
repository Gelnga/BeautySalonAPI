using App.Contracts.BLL.Services;
using Base.Contracts.Base;

namespace App.Contracts.BLL;

public interface IAppBLL : IBLL
{
    IJobPositionService JobPositions { get; }
    IWorkDayService WorkDays { get; }
    IWorkScheduleService WorkSchedules { get; }
}
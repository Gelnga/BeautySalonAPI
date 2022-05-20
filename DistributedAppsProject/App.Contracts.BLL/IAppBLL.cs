using App.Contracts.BLL.Services;
using App.Contracts.BLL.Services.Identity;
using Base.Contracts.Base;

namespace App.Contracts.BLL;

public interface IAppBLL : IBLL
{
    IJobPositionService JobPositions { get; }
    IWorkDayService WorkDays { get; }
    IWorkScheduleService WorkSchedules { get; }
    IRefreshTokenService RefreshTokens { get; }
    IAppUserService AppUsers { get; }
}
using App.Contracts.BLL.Services;
using App.Contracts.BLL.Services.Identity;
using Base.Contracts.Base;

namespace App.Contracts.BLL;

public interface IAppBLL : IBLL
{
    IAppointmentService Appointments { get; }
    IBlogPostService BlogPosts { get; }
    IJobPositionService JobPositions { get; }
    ISalonService Salons { get; }
    ISalonServiceService SalonServices { get; }

    ISalonWorkerService SalonWorkers { get; }
    IServiceService Services { get; }
    IUnitService Units { get; }
    IWorkDayService WorkDays { get; }
    IWorkerService Workers { get; }
    IWorkScheduleService WorkSchedules { get; }
    IRefreshTokenService RefreshTokens { get; }
    IAppUserService AppUsers { get; }
}
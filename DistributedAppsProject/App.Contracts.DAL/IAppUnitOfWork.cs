using App.Contracts.DAL.Identity;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IAppUnitOfWork : IUnitOfWork
{
    IJobPositionRepository JobPositions { get; }
    IWorkDayRepository WorkDays { get; }
    IWorkScheduleRepository WorkSchedules { get; }
    IRefreshTokenRepository RefreshTokens { get; }
    IAppUserRepository AppUsers { get; }
}
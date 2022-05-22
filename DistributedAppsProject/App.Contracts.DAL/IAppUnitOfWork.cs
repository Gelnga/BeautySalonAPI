using App.Contracts.DAL.Repositories;
using App.Contracts.DAL.Repositories.Identity;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IAppUnitOfWork : IUnitOfWork
{
    IAppointmentRepository Appointments { get; }
    IBlogPostRepository BlogPosts { get; }
    IImageObjectRepository ImageObjects { get; }
    IImageRepository Images { get; }
    IJobPositionRepository JobPositions { get; }
    ISalonRepository Salons { get; }
    ISalonServiceRepository SalonServices { get; }
    ISalonWorkerRepository SalonWorkers { get; }
    IServiceRepository Services { get; }
    IUnitRepository Units { get; }
    IWorkDayRepository WorkDays { get; }
    IWorkerRepository Workers { get; }
    IWorkScheduleRepository WorkSchedules { get; }
    IRefreshTokenRepository RefreshTokens { get; }
    IAppUserRepository AppUsers { get; }
}
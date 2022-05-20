using App.Contracts.DAL;
using App.Contracts.DAL.Identity;
using App.DAL.EF.Mappers;
using App.DAL.EF.Mappers.Identity;
using App.DAL.EF.Migrations;
using App.DAL.EF.Repositories;
using App.DAL.EF.Repositories.Identity;
using Base.DAL.EF;

namespace App.DAL.EF;

public class AppUOW : BaseUOW<ApplicationDbContext>, IAppUnitOfWork
{
    private readonly AutoMapper.IMapper _mapper;
    public AppUOW(ApplicationDbContext dbContext, AutoMapper.IMapper mapper ) : base(dbContext)
    {
        _mapper = mapper;
    }

    private IJobPositionRepository? _jobPositions;

    public virtual IJobPositionRepository JobPositions =>
        _jobPositions ??= new JobPositionRepository(UOWDbContext, new JobPositionMapper(_mapper));

    private IWorkDayRepository? _workDays;

    public virtual IWorkDayRepository WorkDays =>
        _workDays ??= new WorkDayRepository(UOWDbContext, new WorkDayMapper(_mapper));

    private IWorkScheduleRepository? _workSchedules;

    public virtual IWorkScheduleRepository WorkSchedules =>
        _workSchedules ??= new WorkScheduleRepository(UOWDbContext, new WorkScheduleMapper(_mapper));

    private IRefreshTokenRepository? _refreshTokens;

    public virtual IRefreshTokenRepository RefreshTokens =>
        _refreshTokens ??= new RefreshTokenRepository(UOWDbContext, new RefreshTokenMapper(_mapper));

    private IAppUserRepository? _appUsers;

    public IAppUserRepository AppUsers =>
        _appUsers ??= new AppUserRepository(UOWDbContext, new AppUserMapper(_mapper));
}
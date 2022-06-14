using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.Contracts.DAL.Repositories.Identity;
using App.DAL.EF.Mappers;
using App.DAL.EF.Mappers.Identity;
using App.DAL.EF.Repositories;
using App.DAL.EF.Repositories.Identity;
using Base.DAL.EF;

namespace App.DAL.EF;

public class AppUOW : BaseUOW<ApplicationDbContext>, IAppUnitOfWork
{
    private readonly AutoMapper.IMapper _mapper;

    public AppUOW(ApplicationDbContext dbContext, AutoMapper.IMapper mapper) : base(dbContext)
    {
        _mapper = mapper;
    }

    private IAppointmentRepository? _appointments;

    public IAppointmentRepository Appointments =>
        _appointments ??= new AppointmentRepository(UOWDbContext, new AppointmentMapper(_mapper));

    private IBlogPostRepository? _blogPosts;

    public IBlogPostRepository BlogPosts =>
        _blogPosts ??= new BlogPostRepository(UOWDbContext, new BlogPostMapper(_mapper));

    private IJobPositionRepository? _jobPositions;

    public virtual IJobPositionRepository JobPositions =>
        _jobPositions ??= new JobPositionRepository(UOWDbContext, new JobPositionMapper(_mapper));

    private ISalonRepository? _salons;

    public ISalonRepository Salons =>
        _salons ??= new SalonRepository(UOWDbContext, new SalonMapper(_mapper));


    private ISalonServiceRepository? _salonServices;

    public ISalonServiceRepository SalonServices =>
        _salonServices ??= new SalonServiceRepository(UOWDbContext, new SalonServiceMapper(_mapper));

    private ISalonWorkerRepository? _salonWorkers;

    public ISalonWorkerRepository SalonWorkers =>
        _salonWorkers ??= new SalonWorkerRepository(UOWDbContext, new SalonWorkerMapper(_mapper));

    private IServiceRepository? _services;

    public IServiceRepository Services =>
        _services ??= new ServiceRepository(UOWDbContext, new ServiceMapper(_mapper));

    private IUnitRepository? _units;

    public IUnitRepository Units =>
        _units ??= new UnitRepository(UOWDbContext, new UnitMapper(_mapper));

    private IWorkDayRepository? _workDays;

    public virtual IWorkDayRepository WorkDays =>
        _workDays ??= new WorkDayRepository(UOWDbContext, new WorkDayMapper(_mapper));

    private IWorkerRepository? _workers;

    public IWorkerRepository Workers =>
        _workers ??= new WorkerRepository(UOWDbContext, new WorkerMapper(_mapper));

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
using App.BLL.Mappers;
using App.BLL.Mappers.Identity;
using App.BLL.Services;
using App.BLL.Services.Identity;
using App.Contracts.BLL;
using App.Contracts.BLL.Services;
using App.Contracts.BLL.Services.Identity;
using App.Contracts.DAL;
using AutoMapper;
using Base.BLL;

namespace App.BLL;

public class AppBLL : BaseBll<IAppUnitOfWork>, IAppBLL
{
    protected IAppUnitOfWork UnitOfWork;
    private readonly AutoMapper.IMapper _mapper;

    public AppBLL(IAppUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public override async Task<int> SaveChangesAsync()
    {
        return await UnitOfWork.SaveChangesAsync();
    }

    public override int SaveChanges()
    {
        return UnitOfWork.SaveChanges();
    }

    private IAppointmentService? _appointments;
    public IAppointmentService Appointments => 
        _appointments ??= new AppointmentService(UnitOfWork.Appointments, new AppointmentMapper(_mapper));

    private IBlogPostService? _blogPosts;
    public IBlogPostService BlogPosts => 
        _blogPosts ??= new BlogPostService(UnitOfWork.BlogPosts, new BlogPostMapper(_mapper));

    private IJobPositionService? _jobPositions;
    public IJobPositionService JobPositions =>
        _jobPositions ??= new JobPositionService(UnitOfWork.JobPositions, new JopPositionMapper(_mapper));

    private ISalonService? _salons;
    public ISalonService Salons => 
        _salons ??= new SalonService(UnitOfWork.Salons, new SalonMapper(_mapper));

    private ISalonServiceService? _salonServices;
    public ISalonServiceService SalonServices => 
        _salonServices ??= new SalonServiceService(UnitOfWork.SalonServices, new SalonServiceMapper(_mapper));

    private ISalonWorkerService? _salonWorkers;
    public ISalonWorkerService SalonWorkers => 
        _salonWorkers ??= new SalonWorkerService(UnitOfWork.SalonWorkers, new SalonWorkerMapper(_mapper));

    private IServiceService? _services;
    public IServiceService Services => 
        _services ??= new ServiceService(UnitOfWork.Services, new ServiceMapper(_mapper));

    private IUnitService? _units;
    public IUnitService Units => 
        _units ??= new UnitService(UnitOfWork.Units, new UnitMapper(_mapper));

    private IWorkDayService? _workDays;
    public IWorkDayService WorkDays =>
        _workDays ??= new WorkDayService(UnitOfWork.WorkDays, new WorkDayMapper(_mapper));

    private IWorkerService? _workers;
    public IWorkerService Workers => 
        _workers ??= new WorkerService(UnitOfWork.Workers, new WorkerMapper(_mapper));

    private IWorkScheduleService? _workSchedules;
    public IWorkScheduleService WorkSchedules =>
        _workSchedules ??= new WorkScheduleService(UnitOfWork.WorkSchedules, new WorkScheduleMapper(_mapper));

    private IRefreshTokenService? _refreshTokens;
    public IRefreshTokenService RefreshTokens =>
        _refreshTokens ??= new RefreshTokenService(UnitOfWork.RefreshTokens, new RefreshTokenMapper(_mapper));

    private IAppUserService? _appUsers;

    public IAppUserService AppUsers =>
        _appUsers ??= new AppUserService(UnitOfWork.AppUsers, new AppUserMapper(_mapper));
}
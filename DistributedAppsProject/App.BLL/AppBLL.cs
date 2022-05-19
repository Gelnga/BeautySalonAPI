using App.BLL.Mappers;
using App.BLL.Services;
using App.Contracts.BLL;
using App.Contracts.BLL.Services;
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
    public async override Task<int> SaveChangesAsync()
    {
        return await UnitOfWork.SaveChangesAsync();
    }

    public override int SaveChanges()
    {
        return UnitOfWork.SaveChanges();
    }

    private IJobPositionService? _jobPositions;
    public IJobPositionService JobPositions =>
        _jobPositions ??= new JobPositionService(UnitOfWork.JobPositions, new JopPositionMapper(_mapper));
    
    private IWorkDayService? _workDays;
    public IWorkDayService WorkDays =>
        _workDays ??= new WorkDayService(UnitOfWork.WorkDays, new WorkDayMapper(_mapper));

    private IWorkScheduleService? _workSchedules;
    public IWorkScheduleService WorkSchedules =>
        _workSchedules ??= new WorkScheduleService(UnitOfWork.WorkSchedules, new WorkScheduleMapper(_mapper));

}
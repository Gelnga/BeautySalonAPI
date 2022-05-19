using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class WorkScheduleMapper : BaseMapper<WorkSchedule, App.Domain.WorkSchedule>
{
    public WorkScheduleMapper(IMapper mapper) : base(mapper)
    {
    }
}
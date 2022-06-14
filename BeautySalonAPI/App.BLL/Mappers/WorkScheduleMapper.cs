using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class WorkScheduleMapper : BaseMapper<App.BLL.DTO.WorkSchedule, App.DAL.DTO.WorkSchedule>
{
    public WorkScheduleMapper(IMapper mapper) : base(mapper)
    {
    }
}
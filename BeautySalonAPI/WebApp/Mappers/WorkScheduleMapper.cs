using AutoMapper;
using Base.DAL;

namespace WebApp.Mappers;

public class WorkScheduleMapper : BaseMapper<App.Public.DTO.v1.WorkSchedule, App.BLL.DTO.WorkSchedule>
{
    public WorkScheduleMapper(IMapper mapper) : base(mapper)
    {
    }
}
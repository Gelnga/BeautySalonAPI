using AutoMapper;
using Base.DAL;

namespace WebApp.Mappers;

public class WorkDayMapper : BaseMapper<App.Public.DTO.v1.WorkDay, App.BLL.DTO.WorkDay>
{
    public WorkDayMapper(IMapper mapper) : base(mapper)
    {
    }
}
using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class WorkDayMapper : BaseMapper<App.BLL.DTO.WorkDay, App.DAL.DTO.WorkDay>
{
    public WorkDayMapper(IMapper mapper) : base(mapper)
    {
    }
}
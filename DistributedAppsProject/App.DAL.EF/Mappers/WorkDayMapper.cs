using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class WorkDayMapper : BaseMapper<WorkDay, App.Domain.WorkDay>
{
    public WorkDayMapper(IMapper mapper) : base(mapper)
    {
    }
}
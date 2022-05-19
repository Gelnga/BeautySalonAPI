using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class JobPositionMapper : BaseMapper<JobPosition, App.Domain.JobPosition>
{
    public JobPositionMapper(IMapper mapper) : base(mapper)
    {
    }
}
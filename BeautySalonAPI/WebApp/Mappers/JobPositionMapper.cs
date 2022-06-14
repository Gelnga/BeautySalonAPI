using AutoMapper;
using Base.DAL;

namespace WebApp.Mappers;

public class JobPositionMapper : BaseMapper<App.Public.DTO.v1.JobPosition, App.BLL.DTO.JobPosition>
{
    public JobPositionMapper(IMapper mapper) : base(mapper)
    {
    }
}
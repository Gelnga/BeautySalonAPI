using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class JopPositionMapper : BaseMapper<App.BLL.DTO.JobPosition, App.DAL.DTO.JobPosition>
{
    public JopPositionMapper(IMapper mapper) : base(mapper)
    {
    }
}
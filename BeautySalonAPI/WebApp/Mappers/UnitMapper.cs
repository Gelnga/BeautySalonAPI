using AutoMapper;
using Base.DAL;

namespace WebApp.Mappers;

public class UnitMapper : BaseMapper<App.Public.DTO.v1.Unit, App.BLL.DTO.Unit>
{
    public UnitMapper(IMapper mapper) : base(mapper)
    {
    }
}
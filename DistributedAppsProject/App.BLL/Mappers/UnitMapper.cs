using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class UnitMapper : BaseMapper<App.BLL.DTO.Unit, App.DAL.DTO.Unit>
{
    public UnitMapper(IMapper mapper) : base(mapper)
    {
    }
}
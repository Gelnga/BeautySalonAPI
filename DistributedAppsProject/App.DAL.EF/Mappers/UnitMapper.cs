using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class UnitMapper : BaseMapper<Unit, App.Domain.Unit>
{
    public UnitMapper(IMapper mapper) : base(mapper)
    {
    }
}
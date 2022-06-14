using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class SalonMapper : BaseMapper<Salon, App.Domain.Salon>
{
    public SalonMapper(IMapper mapper) : base(mapper)
    {
    }
}
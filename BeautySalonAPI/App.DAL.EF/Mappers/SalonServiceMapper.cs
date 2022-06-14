using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class SalonServiceMapper : BaseMapper<SalonService, App.Domain.SalonService>
{
    public SalonServiceMapper(IMapper mapper) : base(mapper)
    {
    }
}
using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class SalonServiceMapper : BaseMapper<App.BLL.DTO.SalonService, App.DAL.DTO.SalonService>
{
    public SalonServiceMapper(IMapper mapper) : base(mapper)
    {
    }
}
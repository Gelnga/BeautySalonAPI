using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class SalonMapper : BaseMapper<App.BLL.DTO.Salon, App.DAL.DTO.Salon>
{
    public SalonMapper(IMapper mapper) : base(mapper)
    {
    }
}
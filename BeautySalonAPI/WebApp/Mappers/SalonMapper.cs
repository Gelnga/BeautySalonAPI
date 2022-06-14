using AutoMapper;
using Base.DAL;

namespace WebApp.Mappers;

public class SalonMapper : BaseMapper<App.Public.DTO.v1.Salon, App.BLL.DTO.Salon>
{
    public SalonMapper(IMapper mapper) : base(mapper)
    {
    }
}
using AutoMapper;
using Base.DAL;

namespace WebApp.Mappers;

public class SalonServiceMapper : BaseMapper<App.Public.DTO.v1.SalonService, App.BLL.DTO.SalonService>
{
    public SalonServiceMapper(IMapper mapper) : base(mapper)
    {
    }
}
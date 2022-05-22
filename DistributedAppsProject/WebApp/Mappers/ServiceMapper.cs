using AutoMapper;
using Base.DAL;

namespace WebApp.Mappers;

public class ServiceMapper : BaseMapper<App.Public.DTO.v1.Service, App.BLL.DTO.Service>
{
    public ServiceMapper(IMapper mapper) : base(mapper)
    {
    }
}
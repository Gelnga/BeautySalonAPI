using AutoMapper;
using Base.DAL;

namespace WebApp.Mappers;

public class ServiceWithSalonServiceDataMapper : BaseMapper<App.Public.DTO.v1.ServiceWithSalonServiceData, App.BLL.DTO.Service>
{
    public ServiceWithSalonServiceDataMapper(IMapper mapper) : base(mapper)
    {
    }
}
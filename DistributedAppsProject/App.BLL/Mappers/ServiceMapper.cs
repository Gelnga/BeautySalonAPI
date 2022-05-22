using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class ServiceMapper : BaseMapper<App.BLL.DTO.Service, App.DAL.DTO.Service>
{
    public ServiceMapper(IMapper mapper) : base(mapper)
    {
    }
}
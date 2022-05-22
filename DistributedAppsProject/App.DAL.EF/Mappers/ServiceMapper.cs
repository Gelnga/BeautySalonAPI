using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class ServiceMapper : BaseMapper<Service, App.Domain.Service>
{
    public ServiceMapper(IMapper mapper) : base(mapper)
    {
    }
}
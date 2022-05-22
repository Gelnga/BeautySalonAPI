using AutoMapper;
using Base.DAL;

namespace WebApp.Mappers;

public class SalonWorkerMapper : BaseMapper<App.Public.DTO.v1.SalonWorker, App.BLL.DTO.SalonWorker>
{
    public SalonWorkerMapper(IMapper mapper) : base(mapper)
    {
    }
}
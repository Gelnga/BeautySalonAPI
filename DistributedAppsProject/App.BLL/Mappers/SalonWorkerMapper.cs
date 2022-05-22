using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class SalonWorkerMapper : BaseMapper<App.BLL.DTO.SalonWorker, App.DAL.DTO.SalonWorker>
{
    public SalonWorkerMapper(IMapper mapper) : base(mapper)
    {
    }
}
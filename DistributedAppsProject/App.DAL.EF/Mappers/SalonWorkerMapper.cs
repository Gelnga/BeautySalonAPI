using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class SalonWorkerMapper : BaseMapper<SalonWorker, App.Domain.SalonWorker>
{
    public SalonWorkerMapper(IMapper mapper) : base(mapper)
    {
    }
}
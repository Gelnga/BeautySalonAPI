using AutoMapper;
using Base.DAL;

namespace WebApp.Mappers;

public class WorkerWithSalonServiceDataMapper : BaseMapper<App.Public.DTO.v1.WorkerWithSalonServiceData, App.BLL.DTO.Worker>
{
    public WorkerWithSalonServiceDataMapper(IMapper mapper) : base(mapper)
    {
    }
}
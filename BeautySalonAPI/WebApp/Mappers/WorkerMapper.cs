using AutoMapper;
using Base.DAL;

namespace WebApp.Mappers;

public class WorkerMapper : BaseMapper<App.Public.DTO.v1.Worker, App.BLL.DTO.Worker>
{
    public WorkerMapper(IMapper mapper) : base(mapper)
    {
    }
}
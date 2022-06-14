using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class WorkerMapper : BaseMapper<App.BLL.DTO.Worker, App.DAL.DTO.Worker>
{
    public WorkerMapper(IMapper mapper) : base(mapper)
    {
    }
}
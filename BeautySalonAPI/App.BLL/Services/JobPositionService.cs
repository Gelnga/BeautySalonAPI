using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class JobPositionService : BaseEntityService<App.BLL.DTO.JobPosition, App.DAL.DTO.JobPosition, IJobPositionRepository>, IJobPositionService
{
    public JobPositionService(IJobPositionRepository repository, IMapper<JobPosition, DAL.DTO.JobPosition> mapper) : base(repository, mapper)
    {
    }
}
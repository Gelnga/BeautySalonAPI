using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class UnitService : BaseEntityService<App.BLL.DTO.Unit, App.DAL.DTO.Unit, IUnitRepository>, IUnitService
{
    public UnitService(IUnitRepository repository, IMapper<Unit, DAL.DTO.Unit> mapper) : base(repository, mapper)
    {
    }
}
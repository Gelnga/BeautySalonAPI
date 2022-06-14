using App.DAL.DTO;
using Base.Contracts.Base;

namespace App.Contracts.BLL.Services;

public interface IServiceService : IEntityService<App.BLL.DTO.Service>
{
    public Task<ICollection<App.BLL.DTO.Service>> GetServicesBySalonId(Guid id);
}
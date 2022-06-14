using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IServiceRepository : IEntityRepository<Service>
{
    public Task<ICollection<Service>> GetServicesBySalonId(Guid id);
}
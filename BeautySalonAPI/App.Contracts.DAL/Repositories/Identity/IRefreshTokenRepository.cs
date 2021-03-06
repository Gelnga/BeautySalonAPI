using App.DAL.DTO.Identity;
using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories.Identity;

public interface IRefreshTokenRepository : IEntityRepository<RefreshToken>
{
    public Task AddRefreshTokenToUser(Guid userId, RefreshToken refreshToken);
}
using App.DAL.DTO.Identity;
using Base.Contracts.DAL;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace App.Contracts.DAL.Identity;

public interface IRefreshTokenRepository : IEntityRepository<RefreshToken>
{
    public void AddRefreshTokenToUser(Guid userId, RefreshToken refreshToken);
}
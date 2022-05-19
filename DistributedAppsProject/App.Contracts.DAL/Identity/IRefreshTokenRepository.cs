using App.Domain.Identity;
using Base.Contracts.DAL;
using Domain.App.Identity;

namespace App.Contracts.DAL.Identity;

public interface IRefreshTokenRepository : IEntityRepository<RefreshToken>
{
}
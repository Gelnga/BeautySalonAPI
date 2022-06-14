using App.BLL.DTO.Identity;
using Base.Contracts.Base;

namespace App.Contracts.BLL.Services.Identity;

public interface IRefreshTokenService: IEntityService<App.BLL.DTO.Identity.RefreshToken>
{
    public Task AddRefreshTokenToUser(Guid userId, RefreshToken refreshToken);
}
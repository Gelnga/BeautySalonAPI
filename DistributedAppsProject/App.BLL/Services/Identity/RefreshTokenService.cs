using App.BLL.DTO.Identity;
using App.Contracts.BLL.Services.Identity;
using App.Contracts.DAL.Repositories.Identity;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services.Identity;

public class RefreshTokenService : BaseEntityService<App.BLL.DTO.Identity.RefreshToken,
    App.DAL.DTO.Identity.RefreshToken, IRefreshTokenRepository>, IRefreshTokenService
{
    public RefreshTokenService(IRefreshTokenRepository repository, IMapper<RefreshToken, DAL.DTO.Identity.RefreshToken> mapper) : base(repository, mapper)
    {
    }

    public void AddRefreshTokenToUser(Guid userId, RefreshToken refreshToken)
    {
        throw new NotImplementedException();
    }
}
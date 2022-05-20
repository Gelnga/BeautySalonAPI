using App.BLL.DTO.Identity;
using Base.Contracts.Base;

namespace App.Contracts.BLL.Services.Identity;

public interface IAppUserService : IPublicEntityService<App.BLL.DTO.Identity.AppUser>
{
    void LoadValidUserRefreshTokens(App.Domain.Identity.AppUser appUser, string givenToken);

    void LoadAllUserRefreshTokens(App.Domain.Identity.AppUser appUser);
}
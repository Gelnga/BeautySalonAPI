using App.DAL.DTO.Identity;
using Base.Contracts.DAL;

namespace App.Contracts.DAL.Identity;

public interface IAppUserRepository : IPublicEntityRepository<AppUser>
{
    void LoadValidUserRefreshTokens(App.Domain.Identity.AppUser appUser, string givenToken);
    
    void LoadAllUserRefreshTokens(App.Domain.Identity.AppUser appUser);
}
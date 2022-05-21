using App.DAL.DTO.Identity;
using Base.Contracts.DAL;

namespace App.Contracts.DAL.Identity;

public interface IAppUserRepository : IPublicEntityRepository<AppUser>
{
    Task LoadValidUserRefreshTokens(App.Domain.Identity.AppUser appUser, string givenToken);
    
    Task LoadAllUserRefreshTokens(App.Domain.Identity.AppUser appUser);
}
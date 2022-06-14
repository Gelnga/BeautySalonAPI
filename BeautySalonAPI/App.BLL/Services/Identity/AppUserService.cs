using App.BLL.DTO.Identity;
using App.BLL.Mappers.Identity;
using App.Contracts.BLL.Services.Identity;
using App.Contracts.DAL.Repositories.Identity;
using Base.BLL;
using Base.Contracts.Base;
using Base.DAL;

namespace App.BLL.Services.Identity;

public class AppUserService : BasePublicEntityService<App.BLL.DTO.Identity.AppUser, App.DAL.DTO.Identity.AppUser,
        IAppUserRepository>,
    IAppUserService
{
    public AppUserService(IAppUserRepository repository, IMapper<AppUser, DAL.DTO.Identity.AppUser> mapper) : base(
        repository, mapper)
    {
    }

    public async Task LoadValidUserRefreshTokens(App.Domain.Identity.AppUser appUser, string givenToken)
    {
        await Repository.LoadValidUserRefreshTokens(appUser, givenToken);
    }

    public async Task LoadAllUserRefreshTokens(Domain.Identity.AppUser appUser)
    {
        await Repository.LoadAllUserRefreshTokens(appUser);
    }
}
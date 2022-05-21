using App.Contracts.DAL.Identity;
using App.DAL.DTO.Identity;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories.Identity;

public class AppUserRepository : BasePublicEntityRepository<AppUser, App.Domain.Identity.AppUser, ApplicationDbContext>,
    IAppUserRepository
{
    public AppUserRepository(ApplicationDbContext dbContext, IMapper<AppUser, Domain.Identity.AppUser> mapper) : base(
        dbContext, mapper)
    {
    }

    public async Task LoadValidUserRefreshTokens(App.Domain.Identity.AppUser appUser, string givenToken)
    {
        await RepoDbContext.Entry(appUser)
            .Collection(u => u.RefreshTokens!)
            .Query()
            .Where(t =>
                (t.Token == givenToken && t.TokenExpirationDateTime > DateTime.UtcNow) ||
                t.PreviousToken == givenToken && t.PreviousTokenExpirationDateTime > DateTime.UtcNow)
            .ToListAsync();
    }

    public async Task LoadAllUserRefreshTokens(App.Domain.Identity.AppUser appUser)
    {
        await RepoDbContext.Entry(appUser)
            .Collection(u => u.RefreshTokens!)
            .LoadAsync();
    }
}
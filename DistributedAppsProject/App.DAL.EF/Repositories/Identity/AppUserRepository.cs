using App.Contracts.DAL.Identity;
using App.Domain.Identity;
using Base.DAL.EF;
using Domain.App.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace App.DAL.EF.Repositories.Identity;

public class AppUserRepository : IAppUserRepository
{
    private readonly ApplicationDbContext _repoDbContext;

    public AppUserRepository(ApplicationDbContext repoDbContext)
    {
        _repoDbContext = repoDbContext;
    }

    public CollectionEntry<AppUser, RefreshToken> GetAppUserRefreshTokens(AppUser appUser)
    {
        return _repoDbContext.Entry(appUser).Collection(u => u.RefreshTokens!);
    }
}
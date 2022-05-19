using App.Domain.Identity;
using Domain.App.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace App.Contracts.DAL.Identity;

public interface IAppUserRepository
{
    CollectionEntry<AppUser, RefreshToken> GetAppUserRefreshTokens(AppUser appUser);
}
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories.Identity;
using App.Domain.Identity;
using Base.Contracts.Base;
using Base.DAL.EF;
using Domain.App.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using AppUser = App.DAL.DTO.Identity.AppUser;
using RefreshToken = App.DAL.DTO.Identity.RefreshToken;

namespace App.DAL.EF.Repositories.Identity;

public class RefreshTokenRepository : BaseEntityRepository<RefreshToken, App.Domain.Identity.RefreshToken,
        ApplicationDbContext>,
    IRefreshTokenRepository
{
    public RefreshTokenRepository(ApplicationDbContext dbContext,
        IMapper<RefreshToken, Domain.Identity.RefreshToken> mapper) : base(dbContext, mapper)
    {
    }

    public async Task AddRefreshTokenToUser(Guid userId, RefreshToken refreshToken)
    {
        var mappedToken = Mapper.Map(refreshToken)!;
        var user = await RepoDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        await RepoDbContext.Entry(user!).Collection(u => u!.RefreshTokens!).LoadAsync();
        if (user!.RefreshTokens == null)
        {
            user.RefreshTokens = new List<Domain.Identity.RefreshToken>
            {
                mappedToken
            };
        }
        else
        {
            user.RefreshTokens.Add(mappedToken);
        }

        RepoDbContext.Update(user);
    }
}
using App.Contracts.DAL;
using App.Contracts.DAL.Identity;
using App.Domain.Identity;
using Base.DAL.EF;
using Domain.App.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace App.DAL.EF.Repositories.Identity;

// public class RefreshTokenRepository : BaseEntityRepository<RefreshToken, ApplicationDbContext, AppUser>,
//     IRefreshTokenRepository
// {
//     public RefreshTokenRepository(ApplicationDbContext dbContext) : base(dbContext)
//     {
//     }
// }
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using App.DAL.DTO.Identity;
using Base.Contracts.Base;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class ImageRepository :
    BaseEntityRepository<Image, App.Domain.Image, ApplicationDbContext, AppUser,
        App.Domain.Identity.AppUser>, IImageRepository
{
    public ImageRepository(ApplicationDbContext dbContext, IMapper<Image, Domain.Image> mapper) : base(dbContext,
        mapper)
    {
    }
}
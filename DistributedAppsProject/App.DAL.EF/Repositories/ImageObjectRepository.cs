using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using App.DAL.DTO.Identity;
using Base.Contracts.Base;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class ImageObjectRepository :
    BaseEntityRepository<ImageObject, App.Domain.ImageObject, ApplicationDbContext, AppUser,
        App.Domain.Identity.AppUser>, IImageObjectRepository
{
    public ImageObjectRepository(ApplicationDbContext dbContext, IMapper<ImageObject, Domain.ImageObject> mapper) :
        base(dbContext, mapper)
    {
    }
}
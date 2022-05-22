using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class ImageService : BaseEntityService<App.BLL.DTO.Image, App.DAL.DTO.Image, IImageRepository>, IImageService
{
    public ImageService(IImageRepository repository, IMapper<Image, DAL.DTO.Image> mapper) : base(repository, mapper)
    {
    }
}
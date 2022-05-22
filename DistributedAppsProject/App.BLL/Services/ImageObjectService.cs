using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class ImageObjectService :
    BaseEntityService<App.BLL.DTO.ImageObject, App.DAL.DTO.ImageObject, IImageObjectRepository>, IImageObjectService
{
    public ImageObjectService(IImageObjectRepository repository, IMapper<ImageObject, DAL.DTO.ImageObject> mapper) :
        base(repository, mapper)
    {
    }
}
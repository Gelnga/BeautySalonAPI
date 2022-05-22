using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class ImageObjectMapper : BaseMapper<App.BLL.DTO.ImageObject, App.DAL.DTO.ImageObject>
{
    public ImageObjectMapper(IMapper mapper) : base(mapper)
    {
    }
}
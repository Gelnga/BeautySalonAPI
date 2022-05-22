using AutoMapper;
using Base.DAL;

namespace WebApp.Mappers;

public class ImageObjectMapper : BaseMapper<App.Public.DTO.v1.ImageObject, App.BLL.DTO.ImageObject>
{
    public ImageObjectMapper(IMapper mapper) : base(mapper)
    {
    }
}
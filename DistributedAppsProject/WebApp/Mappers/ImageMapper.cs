using AutoMapper;
using Base.DAL;

namespace WebApp.Mappers;
 
public class ImageMapper : BaseMapper<App.Public.DTO.v1.Image, App.BLL.DTO.Image>
{
    public ImageMapper(IMapper mapper) : base(mapper)
    {
    }
}
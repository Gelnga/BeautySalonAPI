using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class ImageMapper : BaseMapper<App.BLL.DTO.Image, App.DAL.DTO.Image>
{
    public ImageMapper(IMapper mapper) : base(mapper)
    {
    }
}
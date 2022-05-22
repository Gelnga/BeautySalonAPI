using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class ImageMapper : BaseMapper<Image, App.Domain.Image>
{
    public ImageMapper(IMapper mapper) : base(mapper)
    {
    }
}
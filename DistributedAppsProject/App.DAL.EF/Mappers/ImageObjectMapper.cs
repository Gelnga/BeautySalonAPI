using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class ImageObjectMapper : BaseMapper<ImageObject, App.Domain.ImageObject>
{
    public ImageObjectMapper(IMapper mapper) : base(mapper)
    {
    }
}
using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class BlogPostMapper : BaseMapper<App.BLL.DTO.BlogPost, App.DAL.DTO.BlogPost>
{
    public BlogPostMapper(IMapper mapper) : base(mapper)
    {
    }
}
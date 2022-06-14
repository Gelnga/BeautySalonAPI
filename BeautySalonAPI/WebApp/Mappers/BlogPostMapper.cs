using AutoMapper;
using Base.DAL;

namespace WebApp.Mappers;

public class BlogPostMapper : BaseMapper<App.Public.DTO.v1.BlogPost, App.BLL.DTO.BlogPost>
{
    public BlogPostMapper(IMapper mapper) : base(mapper)
    {
    }
}
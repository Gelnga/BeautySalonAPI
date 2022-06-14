using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class BlogPostMapper : BaseMapper<BlogPost, App.Domain.BlogPost>
{
    public BlogPostMapper(IMapper mapper) : base(mapper)
    {
    }
}
using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class BlogPostService : BaseEntityService<App.BLL.DTO.BlogPost, App.DAL.DTO.BlogPost, IBlogPostRepository>,
    IBlogPostService
{
    public BlogPostService(IBlogPostRepository repository, IMapper<BlogPost, DAL.DTO.BlogPost> mapper) : base(
        repository, mapper)
    {
    }
}
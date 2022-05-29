using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using App.DAL.DTO.Identity;
using Base.Contracts.Base;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class BlogPostRepository :
    BaseEntityRepository<BlogPost, App.Domain.BlogPost, ApplicationDbContext>, IBlogPostRepository
{
    public BlogPostRepository(ApplicationDbContext dbContext, IMapper<BlogPost, Domain.BlogPost> mapper) : base(
        dbContext, mapper)
    {
    }
}
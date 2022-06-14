using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class BlogPostRepository :
    BaseEntityRepository<BlogPost, App.Domain.BlogPost, ApplicationDbContext>, IBlogPostRepository
{
    public BlogPostRepository(ApplicationDbContext dbContext, IMapper<BlogPost, Domain.BlogPost> mapper) : base(
        dbContext, mapper)
    {
    }

    public override IQueryable<Domain.BlogPost> CreateQuery(bool noTracking = true)
    {
        return base.CreateQuery(noTracking).Include(e => e.Worker);
    }

    public override async Task<BlogPost?> FirstOrDefaultAsync(Guid id, Guid userId, bool noTracking = true)
    {
        RepoDbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        var res = await base.FirstOrDefaultAsync(id, userId, noTracking);
        res!.WorkerName = res.Worker!.FirstName + " " + res.Worker!.LastName;
        res.Worker = null;
        RepoDbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
        return res;
    }

    public override async Task<IEnumerable<BlogPost>> GetAllAsync(bool noTracking = true)
    {
        RepoDbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        var res = await base.GetAllAsync(noTracking);
        var blogPosts = res.ToList();
        foreach (var blogPost in blogPosts)
        {
            blogPost.WorkerName = blogPost.Worker!.FirstName + " " + blogPost.Worker.LastName;
            blogPost.Worker = null;
        }
        RepoDbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
        return blogPosts;
    }

    public override async Task<IEnumerable<BlogPost>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        RepoDbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        var res = await base.GetAllAsync(userId, noTracking);
        var blogPosts = res.ToList();
        foreach (var blogPost in blogPosts)
        {
            blogPost.WorkerName = blogPost.Worker!.FirstName + " " + blogPost.Worker.LastName;
            blogPost.Worker = null;
        }
        RepoDbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
        return blogPosts;
    }
}
#nullable disable
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Public.DTO.v1;
using AutoMapper;
using Base.Extensions;
using WebApp.Mappers;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly BlogPostMapper _mapper;

        public BlogPostsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new BlogPostMapper(mapper);
        }

        // GET: api/BlogPosts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogPost>>> GetBlogPosts()
        {
            var res = (await _bll.BlogPosts.GetAllAsync())
                .Select(e => _mapper.Map(e));
            return Ok(res);
        }

        // GET: api/BlogPosts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPost>> GetBlogPost(Guid id)
        {
            var blogPost = await _bll.BlogPosts.FirstOrDefaultAsync(id);

            if (blogPost == null)
            {
                return NotFound();
            }

            return _mapper.Map(blogPost);
        }

        // PUT: api/BlogPosts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlogPost(Guid id, BlogPost blogPostDTO)
        {
            var blogPost = _mapper.Map(blogPostDTO)!;
            if (id != blogPost.Id)
            {
                return BadRequest();
            }

            _bll.BlogPosts.Update(blogPost, User.GetUserId());

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await BlogPostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BlogPosts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BlogPost>> PostBlogPost(BlogPost blogPostDTO)
        {
            var blogPost = _mapper.Map(blogPostDTO)!;
            var added = _bll.BlogPosts.Add(blogPost, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetBlogPost", new { id = added.Id }, _mapper.Map(added));
        }

        // DELETE: api/BlogPosts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogPost(Guid id)
        {
            var blogPost = await _bll.BlogPosts.FirstOrDefaultAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }

            _bll.BlogPosts.Remove(blogPost);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> BlogPostExists(Guid id)
        {
            return await _bll.BlogPosts.ExistsAsync(id);
        }
    }
}

#nullable disable
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Public.DTO.v1;
using AutoMapper;
using Base.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using WebApp.Mappers;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = "admin")]
    public class BlogPostsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly BlogPostMapper _mapper;

        public BlogPostsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new BlogPostMapper(mapper);
        }

        /// <summary>
        /// Get all blogposts
        /// </summary>
        /// <returns></returns>
        // GET: api/BlogPosts
        [HttpGet]
        [AllowAnonymous]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.BlogPost>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<BlogPost>>> GetBlogPosts()
        {
            var res = (await _bll.BlogPosts.GetAllAsync())
                .Select(e => _mapper.Map(e));
            return Ok(res);
        }

        /// <summary>
        /// Get currently logged in user blog posts (requires user to be in a admin or worker group)
        /// </summary>
        /// <returns></returns>
        [HttpGet("private")]
        [Authorize(Roles = "admin, worker")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.BlogPost>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<BlogPost>>> GetBlogPostsPrivate()
        {
            var res = (await _bll.BlogPosts.GetAllAsync(User.GetUserId()))
                .Select(e => _mapper.Map(e));
            return Ok(res);
        }

        /// <summary>
        /// Get single blog post
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/BlogPosts/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin, worker")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.BlogPost), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<BlogPost>> GetBlogPost(Guid id)
        {
            var blogPost = await _bll.BlogPosts.FirstOrDefaultAsync(id, User.GetUserId());

            if (blogPost == null)
            {
                return NotFound();
            }

            return _mapper.Map(blogPost);
        }

        /// <summary>
        /// Update single blog post by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="blogPostDTO"></param>
        /// <returns></returns>
        // PUT: api/BlogPosts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "admin, worker")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PutBlogPost(Guid id, BlogPost blogPostDTO)
        {
            var blogPost = _mapper.Map(blogPostDTO)!;
            var idFrom = blogPost.Id;
            var asfaaf = blogPost.Id == id;
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
            }

            return NoContent();
        }

        /// <summary>
        /// Create blog post
        /// </summary>
        /// <param name="blogPostDTO"></param>
        /// <returns></returns>
        // POST: api/BlogPosts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "admin, worker")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.BlogPost), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<BlogPost>> PostBlogPost(BlogPost blogPostDTO)
        {
            var blogPost = _mapper.Map(blogPostDTO)!;
            var added = _bll.BlogPosts.Add(blogPost, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetBlogPost", new {id = added.Id}, _mapper.Map(added));
        }

        /// <summary>
        /// Delete blog post by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/BlogPosts/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin, worker")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteBlogPost(Guid id)
        {
            var blogPost = await _bll.BlogPosts.FirstOrDefaultAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }

            await _bll.BlogPosts.RemoveAsync(blogPost.Id, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> BlogPostExists(Guid id)
        {
            return await _bll.BlogPosts.ExistsAsync(id);
        }
    }
}
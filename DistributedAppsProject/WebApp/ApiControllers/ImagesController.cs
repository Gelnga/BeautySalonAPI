#nullable disable
using App.Contracts.BLL;
using App.Domain.Identity;
using App.Public.DTO.v1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Base.Extensions;
using WebApp.Mappers;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ImageMapper _mapper;

        public ImagesController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new ImageMapper(mapper);
        }

        // GET: api/Images
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Image>>> GetImages()
        {
            var res = (await _bll.Images.GetAllAsync())
                .Select(e => _mapper.Map(e));
            return Ok(res);
        }

        // GET: api/Images/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Image>> GetImage(Guid id)
        {
            var image = await _bll.Images.FirstOrDefaultAsync(id);

            if (image == null)
            {
                return NotFound();
            }

            return _mapper.Map(image);
        }

        // PUT: api/Images/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImage(Guid id, Image imageDTO)
        {
            var image = _mapper.Map(imageDTO)!;
            if (id != image.Id)
            {
                return BadRequest();
            }

            _bll.Images.Update(image, User.GetUserId());

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ImageExists(id))
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

        // POST: api/Images
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Image>> PostImage(Image imageDTO)
        {
            var image = _mapper.Map(imageDTO)!;
            var added = _bll.Images.Add(image, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetImage", new { id = added.Id }, _mapper.Map(added));
        }

        // DELETE: api/Images/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(Guid id)
        {
            var image = await _bll.Images.FirstOrDefaultAsync(id);
            if (image == null)
            {
                return NotFound();
            }

            _bll.Images.Remove(image);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> ImageExists(Guid id)
        {
            return await _bll.Images.ExistsAsync(id);
        }
    }
}

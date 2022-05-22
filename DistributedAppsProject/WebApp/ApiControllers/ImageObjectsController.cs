#nullable disable
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.DAL.EF.Repositories.Identity;
using App.Public.DTO.v1;
using AutoMapper;
using Base.Extensions;
using WebApp.Mappers;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageObjectsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ImageObjectMapper _mapper;

        public ImageObjectsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new ImageObjectMapper(mapper);
        }

        // GET: api/ImageObjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImageObject>>> GetImageObjects()
        {
            var res = (await _bll.ImageObjects.GetAllAsync())
                .Select(e => _mapper.Map(e));
            return Ok(res);
        }

        // GET: api/ImageObjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImageObject>> GetImageObject(Guid id)
        {
            var imageObject = await _bll.ImageObjects.FirstOrDefaultAsync(id);

            if (imageObject == null)
            {
                return NotFound();
            }

            return _mapper.Map(imageObject);
        }

        // PUT: api/ImageObjects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImageObject(Guid id, ImageObject imageObjectDTO)
        {
            var imageObject = _mapper.Map(imageObjectDTO)!;
            if (id != imageObject.Id)
            {
                return BadRequest();
            }

            _bll.ImageObjects.Update(imageObject, User.GetUserId());

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ImageObjectExists(id))
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

        // POST: api/ImageObjects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ImageObject>> PostImageObject(ImageObject imageObjectDTO)
        {
            var imageObject = _mapper.Map(imageObjectDTO)!;
            var added = _bll.ImageObjects.Add(imageObject, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetImageObject", new { id = added.Id }, _mapper.Map(added));
        }

        // DELETE: api/ImageObjects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImageObject(Guid id)
        {
            var imageObject = await _bll.ImageObjects.FirstOrDefaultAsync(id);
            if (imageObject == null)
            {
                return NotFound();
            }

            _bll.ImageObjects.Remove(imageObject);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> ImageObjectExists(Guid id)
        {
            return await _bll.ImageObjects.ExistsAsync(id);
        }
    }
}

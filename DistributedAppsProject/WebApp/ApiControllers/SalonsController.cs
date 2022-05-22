#nullable disable
using App.Contracts.BLL;
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
    public class SalonsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly SalonMapper _mapper;

        public SalonsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new SalonMapper(mapper);
        }

        // GET: api/Salons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Salon>>> GetSalons()
        {
            var res = (await _bll.Salons.GetAllAsync())
                .Select(e => _mapper.Map(e));
            return Ok(res);
        }

        // GET: api/Salons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Salon>> GetSalon(Guid id)
        {
            var salon = await _bll.Salons.FirstOrDefaultAsync(id);

            if (salon == null)
            {
                return NotFound();
            }

            return _mapper.Map(salon);
        }

        // PUT: api/Salons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalon(Guid id, Salon salonDTO)
        {
            var salon = _mapper.Map(salonDTO)!;
            if (id != salon.Id)
            {
                return BadRequest();
            }

            _bll.Salons.Update(salon, User.GetUserId());

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await SalonExists(id))
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

        // POST: api/Salons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Salon>> PostSalon(Salon salonDTO)
        {
            var salon = _mapper.Map(salonDTO)!;
            var added = _bll.Salons.Add(salon, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetSalon", new { id = added.Id }, _mapper.Map(added));
        }

        // DELETE: api/Salons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalon(Guid id)
        {
            var salon = await _bll.Salons.FirstOrDefaultAsync(id);
            if (salon == null)
            {
                return NotFound();
            }

            _bll.Salons.Remove(salon);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> SalonExists(Guid id)
        {
            return await _bll.Salons.ExistsAsync(id);
        }
    }
}

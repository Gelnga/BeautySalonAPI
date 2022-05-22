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
    public class UnitsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UnitMapper _mapper;

        public UnitsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new UnitMapper(mapper);
        }

        // GET: api/Units
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Unit>>> GetUnits()
        {
            var res = (await _bll.Units.GetAllAsync())
                .Select(e => _mapper.Map(e));
            return Ok(res);
        }

        // GET: api/Units/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Unit>> GetUnit(Guid id)
        {
            var unit = await _bll.Units.FirstOrDefaultAsync(id);

            if (unit == null)
            {
                return NotFound();
            }

            return _mapper.Map(unit);
        }

        // PUT: api/Units/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnit(Guid id, Unit unitDTO)
        {
            var unit = _mapper.Map(unitDTO)!;
            if (id != unit.Id)
            {
                return BadRequest();
            }

            _bll.Units.Update(unit, User.GetUserId());

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await UnitExists(id))
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

        // POST: api/Units
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Unit>> PostUnit(Unit unitDTO)
        {
            var unit = _mapper.Map(unitDTO)!;
            var added = _bll.Units.Add(unit, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetUnit", new { id = added.Id }, added);
        }

        // DELETE: api/Units/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnit(Guid id)
        {
            var unit = await _bll.Units.FirstOrDefaultAsync(id);
            if (unit == null)
            {
                return NotFound();
            }

            _bll.Units.Remove(unit);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> UnitExists(Guid id)
        {
            return await _bll.Units.ExistsAsync(id);
        }
    }
}

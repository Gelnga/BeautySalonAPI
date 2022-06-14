#nullable disable
using App.Contracts.BLL;
using App.Public.DTO.v1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class UnitsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UnitMapper _mapper;

        public UnitsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new UnitMapper(mapper);
        }

        /// <summary>
        /// Get all units
        /// </summary>
        /// <returns></returns>
        // GET: api/Units
        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Unit>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<Unit>>> GetUnits()
        {
            var res = (await _bll.Units.GetAllAsync())
                .Select(e => _mapper.Map(e));
            return Ok(res);
        }

        /// <summary>
        /// Get single unit by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Units/5
        [HttpGet("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.Unit), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Unit>> GetUnit(Guid id)
        {
            var unit = await _bll.Units.FirstOrDefaultAsync(id);

            if (unit == null)
            {
                return NotFound();
            }

            return _mapper.Map(unit);
        }

        /// <summary>
        /// Update single unit by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="unitDTO"></param>
        /// <returns></returns>
        // PUT: api/Units/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
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

        /// <summary>
        /// Create a unit
        /// </summary>
        /// <param name="unitDTO"></param>
        /// <returns></returns>
        // POST: api/Units
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.Unit), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Unit>> PostUnit(Unit unitDTO)
        {
            var unit = _mapper.Map(unitDTO)!;
            var added = _bll.Units.Add(unit, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetUnit", new {id = added.Id}, added);
        }

        /// <summary>
        /// Delete single unit by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Units/5
        [HttpDelete("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
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
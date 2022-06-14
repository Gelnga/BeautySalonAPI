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
    public class SalonServicesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly SalonServiceMapper _mapper;

        public SalonServicesController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new SalonServiceMapper(mapper);
        }

        /// <summary>
        /// Get all salon services
        /// </summary>
        /// <returns></returns>
        // GET: api/SalonServices
        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.SalonService>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<SalonService>>> GetSalonServices()
        {
            var res = (await _bll.SalonServices.GetAllAsync())
                .Select(e => _mapper.Map(e));
            return Ok(res);
        }

        /// <summary>
        /// Get single salon service by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/SalonServices/5
        [HttpGet("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.SalonService), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<SalonService>> GetSalonService(Guid id)
        {
            var salonService = await _bll.SalonServices.FirstOrDefaultAsync(id);

            if (salonService == null)
            {
                return NotFound();
            }

            return _mapper.Map(salonService);
        }

        /// <summary>
        /// Update salon service by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="salonServiceDTO"></param>
        /// <returns></returns>
        // PUT: api/SalonServices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PutSalonService(Guid id, SalonService salonServiceDTO)
        {
            var salonService = _mapper.Map(salonServiceDTO)!;
            if (id != salonService.Id)
            {
                return BadRequest();
            }

            _bll.SalonServices.Update(salonService, User.GetUserId());

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await SalonServiceExists(id))
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
        /// Create salon service
        /// </summary>
        /// <param name="salonServiceDTO"></param>
        /// <returns></returns>
        // POST: api/SalonServices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.SalonService), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<SalonService>> PostSalonService(SalonService salonServiceDTO)
        {
            var salonService = _mapper.Map(salonServiceDTO)!;
            var added = _bll.SalonServices.Add(salonService, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetSalonService", new { id = added.Id }, _mapper.Map(added));
        }

        /// <summary>
        /// Delete salon service by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/SalonServices/5
        [HttpDelete("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteSalonService(Guid id)
        {
            var salonService = await _bll.SalonServices.FirstOrDefaultAsync(id);
            if (salonService == null)
            {
                return NotFound();
            }

            _bll.SalonServices.Remove(salonService);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> SalonServiceExists(Guid id)
        {
            return await _bll.SalonServices.ExistsAsync(id);
        }
    }
}

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
    public class SalonsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly SalonMapper _salonMapper;
        private readonly ServiceWithSalonServiceDataMapper _serviceWithSalonServiceDataMapper;
        private readonly WorkerWithSalonServiceDataMapper _workerWithSalonServiceDataMapper;

        public SalonsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _salonMapper = new SalonMapper(mapper);
            _serviceWithSalonServiceDataMapper = new ServiceWithSalonServiceDataMapper(mapper);
            _workerWithSalonServiceDataMapper = new WorkerWithSalonServiceDataMapper(mapper);
        }

        /// <summary>
        /// Get all salons
        /// </summary>
        /// <returns></returns>
        // GET: api/Salons
        [HttpGet]
        [AllowAnonymous]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Salon>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<Salon>>> GetSalons()
        {
            var res = (await _bll.Salons.GetAllAsync())
                .Select(e => _salonMapper.Map(e));
            return Ok(res);
        }

        /// <summary>
        /// Get single salon by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Salons/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.Salon), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Salon>> GetSalon(Guid id)
        {
            var salon = await _bll.Salons.FirstOrDefaultAsync(id);

            if (salon == null)
            {
                return NotFound();
            }

            return _salonMapper.Map(salon);
        }

        /// <summary>
        /// Update salon by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="salonDTO"></param>
        /// <returns></returns>
        // PUT: api/Salons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PutSalon(Guid id, Salon salonDTO)
        {
            var salon = _salonMapper.Map(salonDTO)!;
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

        /// <summary>
        /// Create salon
        /// </summary>
        /// <param name="salonDTO"></param>
        /// <returns></returns>
        // POST: api/Salons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "admin")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.Salon), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Salon>> PostSalon(Salon salonDTO)
        {
            var salon = _salonMapper.Map(salonDTO)!;
            var added = _bll.Salons.Add(salon, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetSalon", new {id = added.Id}, _salonMapper.Map(added));
        }

        /// <summary>
        /// Delete salon by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Salons/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
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

        /// <summary>
        /// Get salon service by salon id. Required for appointment booking
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/salonServices")]
        [AllowAnonymous]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.ServiceWithSalonServiceData>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetSalonServicesBySalonId(Guid id)
        {
            var res = (await _bll.Services.GetServicesBySalonId(id))
                .Select(e => _serviceWithSalonServiceDataMapper.Map(e));
            return Ok(res);
        }

        /// <summary>
        /// Get salon workers by salon id and service id. Required for appointment booking
        /// </summary>
        /// <param name="salonId"></param>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        [HttpGet("{salonId}/workers")]
        [AllowAnonymous]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.WorkerWithSalonServiceData>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetWorkersBySalonIdAndServiceId(Guid salonId, [FromQuery] Guid serviceId)
        {
            var res = (await _bll.Workers.GetWorkersBySalonIdAndServiceId(salonId, serviceId))
                .Select(e => _workerWithSalonServiceDataMapper.Map(e));
            return Ok(res);
        }
    }
}
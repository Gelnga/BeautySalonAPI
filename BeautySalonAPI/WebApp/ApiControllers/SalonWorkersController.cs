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
    public class SalonWorkersController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly SalonWorkerMapper _mapper;

        public SalonWorkersController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new SalonWorkerMapper(mapper);
        }

        /// <summary>
        /// Get all salon workers
        /// </summary>
        /// <returns></returns>
        // GET: api/SalonWorkers
        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.SalonWorker>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<SalonWorker>>> GetSalonWorkers()
        {
            var res = (await _bll.SalonWorkers.GetAllAsync())
                .Select(e => _mapper.Map(e));
            return Ok(res);
        }

        /// <summary>
        /// Get single salon worker by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/SalonWorkers/5
        [HttpGet("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.SalonWorker), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<SalonWorker>> GetSalonWorker(Guid id)
        {
            var salonWorker = await _bll.SalonWorkers.FirstOrDefaultAsync(id);

            if (salonWorker == null)
            {
                return NotFound();
            }

            return _mapper.Map(salonWorker);
        }

        /// <summary>
        /// Update salon worker by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="salonWorkerDTO"></param>
        /// <returns></returns>
        // PUT: api/SalonWorkers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PutSalonWorker(Guid id, SalonWorker salonWorkerDTO)
        {
            var salonWorker = _mapper.Map(salonWorkerDTO)!;
            if (id != salonWorker.Id)
            {
                return BadRequest();
            }

            _bll.SalonWorkers.Update(salonWorker, User.GetUserId());

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await SalonWorkerExists(id))
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
        /// Create salon worker
        /// </summary>
        /// <param name="salonWorkerDTO"></param>
        /// <returns></returns>
        // POST: api/SalonWorkers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.SalonWorker), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<SalonWorker>> PostSalonWorker(SalonWorker salonWorkerDTO)
        {
            var salonWorker = _mapper.Map(salonWorkerDTO)!;
            var added = _bll.SalonWorkers.Add(salonWorker, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetSalonWorker", new {id = added.Id}, _mapper.Map(added));
        }

        /// <summary>
        /// Delete salon worker by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/SalonWorkers/5
        [HttpDelete("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteSalonWorker(Guid id)
        {
            var salonWorker = await _bll.SalonWorkers.FirstOrDefaultAsync(id);
            if (salonWorker == null)
            {
                return NotFound();
            }

            _bll.SalonWorkers.Remove(salonWorker);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> SalonWorkerExists(Guid id)
        {
            return await _bll.SalonWorkers.ExistsAsync(id);
        }
    }
}
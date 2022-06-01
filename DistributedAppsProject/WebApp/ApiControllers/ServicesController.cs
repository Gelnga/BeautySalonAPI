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
    public class ServicesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ServiceMapper _mapper;

        public ServicesController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new ServiceMapper(mapper);
        }

        /// <summary>
        /// Get all services
        /// </summary>
        /// <returns></returns>
        // GET: api/Services
        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Service>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<Service>>> GetServices()
        {
            var res = (await _bll.Services.GetAllAsync())
                .Select(e => _mapper.Map(e));
            return Ok(res);
        }

        /// <summary>
        /// Get single service by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Services/5
        [HttpGet("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.Service), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Service>> GetService(Guid id)
        {
            var service = await _bll.Services.FirstOrDefaultAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            return _mapper.Map(service);
        }

        /// <summary>
        /// Update single service by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="serviceDTO"></param>
        /// <returns></returns>
        // PUT: api/Services/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PutService(Guid id, Service serviceDTO)
        {
            var service = _mapper.Map(serviceDTO)!;
            if (id != service.Id)
            {
                return BadRequest();
            }

            _bll.Services.Update(service, User.GetUserId());

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ServiceExists(id))
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
        /// Create a service
        /// </summary>
        /// <param name="serviceDTO"></param>
        /// <returns></returns>
        // POST: api/Services
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "admin")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.Service), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Service>> PostService(Service serviceDTO)
        {
            var service = _mapper.Map(serviceDTO)!;
            var added = _bll.Services.Add(service);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetService", new {id = added.Id}, _mapper.Map(added));
        }

        /// <summary>
        /// Delete single service by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Services/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteService(Guid id)
        {
            var service = await _bll.Services.FirstOrDefaultAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            _bll.Services.Remove(service);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> ServiceExists(Guid id)
        {
            return await _bll.Services.ExistsAsync(id);
        }
    }
}
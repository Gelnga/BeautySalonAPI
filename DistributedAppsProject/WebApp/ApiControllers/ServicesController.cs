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
    public class ServicesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ServiceMapper _mapper;

        public ServicesController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new ServiceMapper(mapper);
        }

        // GET: api/Services
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetServices()
        {
            var res = (await _bll.Services.GetAllAsync())
                .Select(e => _mapper.Map(e));
            return Ok(res);
        }

        // GET: api/Services/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetService(Guid id)
        {
            var service = await _bll.Services.FirstOrDefaultAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            return _mapper.Map(service);
        }

        // PUT: api/Services/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
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

        // POST: api/Services
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Service>> PostService(Service serviceDTO)
        {
            var service = _mapper.Map(serviceDTO)!;
            var added = _bll.Services.Add(service);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetService", new { id = added.Id }, _mapper.Map(added));
        }

        // DELETE: api/Services/5
        [HttpDelete("{id}")]
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

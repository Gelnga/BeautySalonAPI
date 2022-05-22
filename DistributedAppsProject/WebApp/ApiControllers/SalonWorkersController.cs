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
    public class SalonWorkersController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly SalonWorkerMapper _mapper;

        public SalonWorkersController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new SalonWorkerMapper(mapper);
        }

        // GET: api/SalonWorkers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalonWorker>>> GetSalonWorkers()
        {
            var res = (await _bll.SalonWorkers.GetAllAsync())
                .Select(e => _mapper.Map(e));
            return Ok(res);
        }

        // GET: api/SalonWorkers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalonWorker>> GetSalonWorker(Guid id)
        {
            var salonWorker = await _bll.SalonWorkers.FirstOrDefaultAsync(id);

            if (salonWorker == null)
            {
                return NotFound();
            }

            return _mapper.Map(salonWorker);
        }

        // PUT: api/SalonWorkers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
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

        // POST: api/SalonWorkers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SalonWorker>> PostSalonWorker(SalonWorker salonWorkerDTO)
        {
            var salonWorker = _mapper.Map(salonWorkerDTO)!;
            var added = _bll.SalonWorkers.Add(salonWorker, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetSalonWorker", new { id = added.Id }, _mapper.Map(added));
        }

        // DELETE: api/SalonWorkers/5
        [HttpDelete("{id}")]
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

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
    public class WorkersController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly WorkerMapper _mapper;

        public WorkersController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new WorkerMapper(mapper);
        }

        // GET: api/Workers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Worker>>> GetWorkers()
        {
            var res = (await _bll.Workers.GetAllAsync())
                .Select(e => _mapper.Map(e));
            return Ok(res);
        }

        // GET: api/Workers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Worker>> GetWorker(Guid id)
        {
            var worker = await _bll.Workers.FirstOrDefaultAsync(id);

            if (worker == null)
            {
                return NotFound();
            }

            return _mapper.Map(worker);
        }

        // PUT: api/Workers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorker(Guid id, Worker workerDTO)
        {
            var worker = _mapper.Map(workerDTO)!;
            if (id != worker.Id)
            {
                return BadRequest();
            }

            _bll.Workers.Update(worker, User.GetUserId());

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await WorkerExists(id))
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

        // POST: api/Workers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Worker>> PostWorker(Worker workerDTO)
        {
            var worker = _mapper.Map(workerDTO)!;
            var added = _bll.Workers.Add(worker, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetWorker", new { id = added.Id }, added);
        }

        // DELETE: api/Workers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorker(Guid id)
        {
            var worker = await _bll.Workers.FirstOrDefaultAsync(id);
            if (worker == null)
            {
                return NotFound();
            }

            _bll.Workers.Remove(worker);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> WorkerExists(Guid id)
        {
            return await _bll.Workers.ExistsAsync(id);
        }
    }
}

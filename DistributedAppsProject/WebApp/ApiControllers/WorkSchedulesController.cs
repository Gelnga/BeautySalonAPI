#nullable disable
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using App.BLL.DTO;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WorkSchedulesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public WorkSchedulesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/WorkSchedules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkSchedule>>> GetWorkSchedules()
        {
            var res = await _bll.WorkSchedules.GetAllAsync();
            return Ok(res);
        }

        // GET: api/WorkSchedules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkSchedule>> GetWorkSchedule(Guid id)
        {
            var workSchedule = await _bll.WorkSchedules.FirstOrDefaultAsync(id);

            if (workSchedule == null)
            {
                return NotFound();
            }

            return workSchedule;
        }

        // PUT: api/WorkSchedules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkSchedule(Guid id, WorkSchedule workSchedule)
        {
            if (id != workSchedule.Id)
            {
                return BadRequest();
            }
            
            _bll.WorkSchedules.Update(workSchedule);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkScheduleExists(id))
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

        // POST: api/WorkSchedules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkSchedule>> PostWorkSchedule(WorkSchedule workSchedule)
        {
            _bll.WorkSchedules.Add(workSchedule);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetWorkSchedule", new { id = workSchedule.Id }, workSchedule);
        }

        // DELETE: api/WorkSchedules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkSchedule(Guid id)
        {
            var workSchedule = await _bll.WorkSchedules.FirstOrDefaultAsync(id);
            if (workSchedule == null)
            {
                return NotFound();
            }

            _bll.WorkSchedules.Remove(workSchedule);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkScheduleExists(Guid id)
        {
            return _bll.WorkSchedules.Exists(id);
        }
    }
}

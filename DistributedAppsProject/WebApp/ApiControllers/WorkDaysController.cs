#nullable disable
using App.Contracts.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.BLL.DTO;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WorkDaysController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public WorkDaysController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/WorkDays
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkDay>>> GetWorkDays()
        {
            var res = await _bll.WorkDays.GetAllAsync();
            return Ok(res);
        }

        // GET: api/WorkDays/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkDay>> GetWorkDay(Guid id)
        {
            var workDay = await _bll.WorkDays.FirstOrDefaultAsync(id);

            if (workDay == null)
            {
                return NotFound();
            }

            return workDay;
        }

        // PUT: api/WorkDays/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkDay(Guid id, WorkDay workDay)
        {
            if (id != workDay.Id)
            {
                return BadRequest();
            }

            _bll.WorkDays.Update(workDay);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkDayExists(id))
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

        // POST: api/WorkDays
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkDay>> PostWorkDay(WorkDay workDay)
        {
            _bll.WorkDays.Add(workDay);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetWorkDay", new { id = workDay.Id }, workDay);
        }

        // DELETE: api/WorkDays/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkDay(Guid id)
        {
            var workDay = await _bll.WorkDays.FirstOrDefaultAsync(id);
            if (workDay == null)
            {
                return NotFound();
            }

            _bll.WorkDays.Remove(workDay);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkDayExists(Guid id)
        {
            return _bll.WorkDays.Exists(id);
        }
    }
}

#nullable disable
using App.Contracts.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using WebApp.DTO;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WorkSchedulesController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public WorkSchedulesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/WorkSchedules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkSchedule>>> GetWorkSchedules()
        {
            var res = await _uow.WorkSchedules.GetAllAsyncPublic();
            return Ok(res);
        }

        // GET: api/WorkSchedules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkSchedule>> GetWorkSchedule(Guid id)
        {
            var workSchedule = await _uow.WorkSchedules.FirstOrDefaultAsyncPublic(id);

            if (workSchedule == null)
            {
                return NotFound();
            }

            return workSchedule;
        }

        // PUT: api/WorkSchedules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkSchedule(Guid id, WorkScheduleDTO workScheduleDTO)
        {
            if (id != workScheduleDTO.Id)
            {
                return BadRequest();
            }

            var workSchedule = workScheduleDTO.ToEntity();
            _uow.WorkSchedules.Update(workSchedule);

            try
            {
                await _uow.SaveChangesAsync();
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
        public async Task<ActionResult<WorkSchedule>> PostWorkSchedule(WorkScheduleDTO workScheduleDTO)
        {
            var workSchedule = workScheduleDTO.ToEntity();
            _uow.WorkSchedules.Add(workSchedule);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetWorkSchedule", new { id = workSchedule.Id }, workSchedule);
        }

        // DELETE: api/WorkSchedules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkSchedule(Guid id)
        {
            var workSchedule = await _uow.WorkSchedules.FirstOrDefaultAsyncPublic(id);
            if (workSchedule == null)
            {
                return NotFound();
            }

            _uow.WorkSchedules.RemovePublic(workSchedule);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkScheduleExists(Guid id)
        {
            return _uow.WorkSchedules.Exists(id);
        }
    }
}

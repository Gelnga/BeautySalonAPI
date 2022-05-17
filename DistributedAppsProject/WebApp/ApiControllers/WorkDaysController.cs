#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL;
using App.DAL.EF;
using App.Domain;
using WebApp.DTO;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkDaysController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public WorkDaysController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/WorkDays
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkDay>>> GetWorkDays()
        {
            var res = await _uow.WorkDays.GetAllAsync();
            return Ok(res);
        }

        // GET: api/WorkDays/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkDay>> GetWorkDay(Guid id)
        {
            var workDay = await _uow.WorkDays.FirstOrDefaultAsync(id);

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

            _uow.WorkDays.Update(workDay);

            try
            {
                await _uow.SaveChangesAsync();
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
        public async Task<ActionResult<WorkDay>> PostWorkDay(WorkDayDTO workDayDTO)
        {
            var workDay = workDayDTO.ToEntity();
            _uow.WorkDays.Add(workDay);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetWorkDay", new { id = workDay.Id }, workDay);
        }

        // DELETE: api/WorkDays/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkDay(Guid id)
        {
            var workDay = await _uow.WorkDays.FirstOrDefaultAsync(id);
            if (workDay == null)
            {
                return NotFound();
            }

            _uow.WorkDays.Remove(workDay);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkDayExists(Guid id)
        {
            return _uow.WorkDays.Exists(id);
        }
    }
}

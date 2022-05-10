#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.DAL;
using App.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.App;
using WebApp.DTO;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkScheduleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WorkScheduleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/WorkSchedule
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkScheduleDto>>> GetWorkSchedules()
        {
            var res = (await _context.WorkSchedules.ToListAsync())
                .Select(x => new WorkScheduleDto(x))
                .ToList();
            return res;
        }

        // GET: api/WorkSchedule/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkSchedule>> GetWorkSchedule(Guid id)
        {
            var workSchedule = await _context.WorkSchedules.FindAsync(id);

            if (workSchedule == null)
            {
                return NotFound();
            }

            return workSchedule;
        }

        // PUT: api/WorkSchedule/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkSchedule(Guid id, WorkSchedule workSchedule)
        {
            if (id != workSchedule.Id)
            {
                return BadRequest();
            }

            _context.Entry(workSchedule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

        // POST: api/WorkSchedule
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkScheduleDto>> PostWorkSchedule(WorkScheduleDto workScheduleDto)
        {
            var workSchedule = workScheduleDto.ToEntity();
            _context.WorkSchedules.Add(workSchedule);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkSchedule", new { id = workSchedule.Id }, workSchedule);
        }

        // DELETE: api/WorkSchedule/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkSchedule(Guid id)
        {
            var workSchedule = await _context.WorkSchedules.FindAsync(id);
            if (workSchedule == null)
            {
                return NotFound();
            }

            _context.WorkSchedules.Remove(workSchedule);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkScheduleExists(Guid id)
        {
            return _context.WorkSchedules.Any(e => e.Id == id);
        }
    }
}

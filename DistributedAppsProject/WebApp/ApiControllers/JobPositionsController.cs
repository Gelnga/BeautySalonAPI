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
    public class JobPositionsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public JobPositionsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/JobPositions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobPosition>>> GetJobPositions()
        {
            var res = await _uow.JobPositions.GetAllAsync();
            return Ok(res);
        }

        // GET: api/JobPositions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobPosition>> GetJobPosition(Guid id)
        {
            var jobPosition = await _uow.JobPositions.FirstOrDefaultAsync(id);

            if (jobPosition == null)
            {
                return NotFound();
            }

            return jobPosition;
        }

        // PUT: api/JobPositions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobPosition(Guid id, JobPosition jobPosition)
        {
            if (id != jobPosition.Id)
            {
                return BadRequest();
            }

            _uow.JobPositions.Update(jobPosition);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobPositionExists(id))
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

        // POST: api/JobPositions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JobPosition>> PostJobPosition(JobPositionDTO jobPositionDTO)
        {
            var jobPosition = jobPositionDTO.ToEntity();
            _uow.JobPositions.Add(jobPosition);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetJobPosition", new { id = jobPosition.Id }, jobPosition);
        }

        // DELETE: api/JobPositions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobPosition(Guid id)
        {
            var jobPosition = await _uow.JobPositions.FirstOrDefaultAsync(id);
            if (jobPosition == null)
            {
                return NotFound();
            }

            _uow.JobPositions.Remove(jobPosition);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        private bool JobPositionExists(Guid id)
        {
            return _uow.JobPositions.Exists(id);
        }
    }
}

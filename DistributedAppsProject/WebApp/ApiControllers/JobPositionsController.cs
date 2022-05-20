#nullable disable
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.BLL.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using WebApp.DTO;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class JobPositionsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public JobPositionsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/JobPositions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobPosition>>> GetJobPositions()
        {
            var res = await _bll.JobPositions.GetAllAsync();
            return Ok(res);
        }

        // GET: api/JobPositions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobPosition>> GetJobPosition(Guid id)
        {
            var jobPosition = await _bll.JobPositions.FirstOrDefaultAsync(id);

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

            _bll.JobPositions.Update(jobPosition);

            try
            {
                await _bll.SaveChangesAsync();
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
        public async Task<ActionResult<JobPosition>> PostJobPosition(JobPosition jobPosition)
        {
            _bll.JobPositions.Add(jobPosition);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetJobPosition", new { id = jobPosition.Id }, jobPosition);
        }

        // DELETE: api/JobPositions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobPosition(Guid id)
        {
            var jobPosition = await _bll.JobPositions.FirstOrDefaultAsync(id);
            if (jobPosition == null)
            {
                return NotFound();
            }

            _bll.JobPositions.Remove(jobPosition);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private bool JobPositionExists(Guid id)
        {
            return _bll.JobPositions.Exists(id);
        }
    }
}

#nullable disable
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Public.DTO.v1;
using AutoMapper;
using Base.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using WebApp.Mappers;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = "admin")]
    public class JobPositionsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly JobPositionMapper _mapper;

        public JobPositionsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new JobPositionMapper(mapper);
        }

        /// <summary>
        /// Get all job positions
        /// </summary>
        /// <returns></returns>
        // GET: api/JobPositions
        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.JobPosition>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<JobPosition>>> GetJobPositions()
        {
            var res = (await _bll.JobPositions.GetAllAsync())
                .Select(e => _mapper.Map(e));
            return Ok(res);
        }

        /// <summary>
        /// Get single job position
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/JobPositions/5
        [HttpGet("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.JobPosition), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<JobPosition>> GetJobPosition(Guid id)
        {
            var jobPosition = await _bll.JobPositions.FirstOrDefaultAsync(id);

            if (jobPosition == null)
            {
                return NotFound();
            }

            return _mapper.Map(jobPosition);
        }

        /// <summary>
        /// Updaet single job position by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jobPositionDTO"></param>
        /// <returns></returns>
        // PUT: api/JobPositions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PutJobPosition(Guid id, JobPosition jobPositionDTO)
        {
            var jobPosition = _mapper.Map(jobPositionDTO)!;
            if (id != jobPosition.Id)
            {
                return BadRequest();
            }

            _bll.JobPositions.Update(jobPosition, User.GetUserId());

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await JobPositionExists(id))
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

        /// <summary>
        /// Create job position
        /// </summary>
        /// <param name="jobPositionDTO"></param>
        /// <returns></returns>
        // POST: api/JobPositions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.JobPosition), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<JobPosition>> PostJobPosition(JobPosition jobPositionDTO)
        {
            var jobPosition = _mapper.Map(jobPositionDTO)!;
            var added = _bll.JobPositions.Add(jobPosition, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetJobPosition", new {id = added.Id}, _mapper.Map(added));
        }

        /// <summary>
        /// Delete job position by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/JobPositions/5
        [HttpDelete("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
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

        private async Task<bool> JobPositionExists(Guid id)
        {
            return await _bll.JobPositions.ExistsAsync(id);
        }
    }
}
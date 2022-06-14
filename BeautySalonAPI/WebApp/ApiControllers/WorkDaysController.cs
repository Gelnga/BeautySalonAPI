#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Public.DTO.v1;
using App.Contracts.BLL;
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
    public class WorkDaysController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly WorkDayMapper _mapper;

        public WorkDaysController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new WorkDayMapper(mapper);
        }

        /// <summary>
        /// Get all work days
        /// </summary>
        /// <returns></returns>
        // GET: api/WorkDays
        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.WorkDay>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<WorkDay>>> GetWorkDays()
        {
            var res = (await _bll.WorkDays.GetAllAsync())
                .Select(e => _mapper.Map(e));
            return Ok(res);
        }

        /// <summary>
        /// Get single workday by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/WorkDays/5
        [HttpGet("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.WorkDay), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<WorkDay>> GetWorkDay(Guid id)
        {
            var workDay = await _bll.WorkDays.FirstOrDefaultAsync(id);

            if (workDay == null)
            {
                return NotFound();
            }

            return _mapper.Map(workDay);
        }

        /// <summary>
        /// Update single workday by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="workDayDTO"></param>
        /// <returns></returns>
        // PUT: api/WorkDays/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PutWorkDay(Guid id, WorkDay workDayDTO)
        {
            var workDay = _mapper.Map(workDayDTO)!;
            if (id != workDay.Id)
            {
                return BadRequest();
            }

            _bll.WorkDays.Update(workDay, User.GetUserId());

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await WorkDayExists(id))
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
        /// Create a work day
        /// </summary>
        /// <param name="workDayDTO"></param>
        /// <returns></returns>
        // POST: api/WorkDays
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.WorkDay), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<WorkDay>> PostWorkDay(WorkDay workDayDTO)
        {
            var workDay = _mapper.Map(workDayDTO)!;
            var added = _bll.WorkDays.Add(workDay, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetWorkDay", new {id = added.Id}, _mapper.Map(added));
        }

        /// <summary>
        /// Delete single work day by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/WorkDays/5
        [HttpDelete("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
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

        private async Task<bool> WorkDayExists(Guid id)
        {
            return await _bll.WorkDays.ExistsAsync(id);
        }
    }
}
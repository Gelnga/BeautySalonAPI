#nullable disable
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using App.Public.DTO.v1;
using AutoMapper;
using Base.Extensions;
using WebApp.Mappers;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = "admin")]
    public class WorkSchedulesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly WorkScheduleMapper _mapper;

        public WorkSchedulesController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new WorkScheduleMapper(mapper);
        }

        /// <summary>
        /// Get all work schedules
        /// </summary>
        /// <returns></returns>
        // GET: api/WorkSchedules
        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.WorkSchedule>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<WorkSchedule>>> GetWorkSchedules()
        {
            var res = (await _bll.WorkSchedules.GetAllAsync())
                .Select(e => _mapper.Map(e));
            return Ok(res);
        }

        /// <summary>
        /// Get single work schedule by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/WorkSchedules/5
        [HttpGet("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.WorkSchedule), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<WorkSchedule>> GetWorkSchedule(Guid id)
        {
            var workSchedule = await _bll.WorkSchedules.FirstOrDefaultAsync(id);

            if (workSchedule == null)
            {
                return NotFound();
            }

            return _mapper.Map(workSchedule);
        }

        /// <summary>
        /// Update single work schedule by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="workScheduleDTO"></param>
        /// <returns></returns>
        // PUT: api/WorkSchedules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PutWorkSchedule(Guid id, WorkSchedule workScheduleDTO)
        {
            var workSchedule = _mapper.Map(workScheduleDTO)!;
            if (id != workSchedule.Id)
            {
                return BadRequest();
            }

            _bll.WorkSchedules.Update(workSchedule, User.GetUserId());

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

        /// <summary>
        /// Create a work schedule
        /// </summary>
        /// <param name="workScheduleDTO"></param>
        /// <returns></returns>
        // POST: api/WorkSchedules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.WorkSchedule), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<WorkSchedule>> PostWorkSchedule(WorkSchedule workScheduleDTO)
        {
            var workSchedule = _mapper.Map(workScheduleDTO)!;
            workSchedule.OwnerId = User.GetUserId();
            var added = _bll.WorkSchedules.Add(workSchedule);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetWorkSchedule", new {id = added.Id}, _mapper.Map(added));
        }

        /// <summary>
        /// Delete single work schedule by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/WorkSchedules/5
        [HttpDelete("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
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
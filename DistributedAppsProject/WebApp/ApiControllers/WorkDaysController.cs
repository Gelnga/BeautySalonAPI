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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WorkDaysController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly WorkDayMapper _mapper;

        public WorkDaysController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new WorkDayMapper(mapper);
        }

        // GET: api/WorkDays
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkDay>>> GetWorkDays()
        {
            var res = (await _bll.WorkDays.GetAllAsync())
                .Select(e => _mapper.Map(e));
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

            return _mapper.Map(workDay);
        }

        // PUT: api/WorkDays/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
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

        // POST: api/WorkDays
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkDay>> PostWorkDay(WorkDay workDayDTO)
        {
            var workDay = _mapper.Map(workDayDTO)!;
            var added = _bll.WorkDays.Add(workDay, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetWorkDay", new { id = added.Id }, _mapper.Map(added));
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

        private async Task<bool> WorkDayExists(Guid id)
        {
            return await _bll.WorkDays.ExistsAsync(id);
        }
    }
}

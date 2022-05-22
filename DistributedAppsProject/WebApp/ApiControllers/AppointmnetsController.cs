#nullable disable
using App.Contracts.BLL;
using App.Public.DTO.v1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class AppointmnetsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly AppointmentMapper _mapper;

        public AppointmnetsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new AppointmentMapper(mapper);
        }

        // GET: api/Appointmnets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
        {
            var res = (await _bll.Appointments.GetAllAsync())
                .Select(e => _mapper.Map(e));
            return Ok(res);
        }

        // GET: api/Appointmnets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(Guid id)
        {
            var appointment = await _bll.Appointments.FirstOrDefaultAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return _mapper.Map(appointment);
        }

        // PUT: api/Appointmnets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointment(Guid id, Appointment appointmentDTO)
        {
            var appointment = _mapper.Map(appointmentDTO)!;
            if (id != appointment.Id)
            {
                return BadRequest();
            }

            _bll.Appointments.Update(appointment, User.GetUserId());

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await AppointmentExists(id))
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

        // POST: api/Appointmnets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Appointment>> PostAppointment(Appointment appointmentDTO)
        {
            var appointment = _mapper.Map(appointmentDTO)!;
            var added = _bll.Appointments.Add(appointment, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetAppointment", new { id = added.Id }, _mapper.Map(added));
        }

        // DELETE: api/Appointmnets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(Guid id)
        {
            var appointment = await _bll.Appointments.FirstOrDefaultAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            _bll.Appointments.Remove(appointment);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> AppointmentExists(Guid id)
        {
            return await _bll.Appointments.ExistsAsync(id);
        }
    }
}

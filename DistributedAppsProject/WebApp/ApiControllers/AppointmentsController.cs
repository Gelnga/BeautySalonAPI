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
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = "user, worker, admin")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly AppointmentMapper _mapper;

        public AppointmentsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new AppointmentMapper(mapper);
        }

        /// <summary>
        /// Get all USER appointments, requires user to be logged in
        /// </summary>
        /// <returns></returns>
        // GET: api/Appointmnets
        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Appointment>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
        {
            var res = (await _bll.Appointments.GetAllAsync(User.GetUserId()))
                .Select(e => _mapper.Map(e));
            return Ok(res);
        }

        /// <summary>
        /// Get single appointment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Appointmnets/5
        [HttpGet("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.Appointment), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Appointment>> GetAppointment(Guid id)
        {
            var appointment = await _bll.Appointments.FirstOrDefaultAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return _mapper.Map(appointment);
        }

        /// <summary>
        /// Update single appointment by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="appointmentDTO"></param>
        /// <returns></returns>
        // PUT: api/Appointmnets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
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

        /// <summary>
        /// Create appointment, requires user to be logged in
        /// </summary>
        /// <param name="appointmentDTO"></param>
        /// <returns></returns>
        // POST: api/Appointmnets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.Appointment), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Appointment>> PostAppointment(Appointment appointmentDTO)
        {
            var appointment = _mapper.Map(appointmentDTO)!;
            var added = _bll.Appointments.Add(appointment, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetAppointment", new { id = added.Id }, _mapper.Map(added));
        }

        /// <summary>
        /// Delete appointment, requires to be logged in
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Appointmnets/5
        [HttpDelete("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteAppointment(Guid id)
        {
            var appointment = await _bll.Appointments.FirstOrDefaultAsync(id, User.GetUserId());
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

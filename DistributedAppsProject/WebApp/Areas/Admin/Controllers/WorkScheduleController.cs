#nullable disable
using App.Contracts.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.DTO;
using static Base.Extensions.IdentityExtension;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class WorkScheduleController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public WorkScheduleController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Admin/WorkSchedule
        public async Task<IActionResult> Index()
        {
            var res = (await _uow.WorkSchedules.GetAllAsync(User.GetUserId()))
                .Select(ws => new WorkScheduleDTO(ws));
            return View(res);
        }

        // GET: Admin/WorkSchedule/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workSchedule = await _uow.WorkSchedules
                .FirstOrDefaultAsync(id.Value, User.GetUserId());
            if (workSchedule == null)
            {
                return NotFound();
            }

            return View(new WorkScheduleDTO(workSchedule));
        }

        // GET: Admin/WorkSchedule/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/WorkSchedule/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,IsWeek,Id,Commentary")] WorkScheduleDTO workScheduleDto)
        {
            if (ModelState.IsValid)
            {
                var workSchedule = workScheduleDto.ToEntity();
                workSchedule.Id = Guid.NewGuid();
                workSchedule.AppUserId = User.GetUserId();
                _uow.WorkSchedules.Add(workSchedule);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workScheduleDto);
        }

        // GET: Admin/WorkSchedule/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workSchedule = await _uow.WorkSchedules.FirstOrDefaultAsync(id.Value, User.GetUserId());
            if (workSchedule == null)
            {
                return NotFound();
            }

            var workScheduleDTO = new WorkScheduleDTO(workSchedule);
            return View(workScheduleDTO);
        }

        // POST: Admin/WorkSchedule/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, WorkScheduleDTO workScheduleDto)
        {
            var workSchedule = workScheduleDto.ToEntity();
            workSchedule.AppUserId = User.GetUserId();
            if (id != workSchedule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.WorkSchedules.Update(workSchedule);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkScheduleExists(workSchedule.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(new WorkScheduleDTO(workSchedule));
        }

        // GET: Admin/WorkSchedule/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workSchedule = await _uow.WorkSchedules.RemoveAsync(id.Value, User.GetUserId());
            return View(new WorkScheduleDTO(workSchedule));
        }

        // POST: Admin/WorkSchedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var workSchedule = await _uow.WorkSchedules.FirstOrDefaultAsync(id, User.GetUserId());
            _uow.WorkSchedules.RemovePublic(workSchedule!);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkScheduleExists(Guid id)
        {
            return _uow.WorkSchedules.Exists(id);
        }
    }
}

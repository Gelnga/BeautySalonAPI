#nullable disable
using App.Contracts.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.BLL.DTO;
using static Base.Extensions.IdentityExtensions;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class WorkScheduleController : Controller
    {
        private readonly IAppBLL _bll;

        public WorkScheduleController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Admin/WorkSchedule
        public async Task<IActionResult> Index()
        {
            var res = (await _bll.WorkSchedules.GetAllAsync(User.GetUserId()));
            return View(res);
        }

        // GET: Admin/WorkSchedule/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workSchedule = await _bll.WorkSchedules
                .FirstOrDefaultAsync(id.Value, User.GetUserId());
            if (workSchedule == null)
            {
                return NotFound();
            }

            return View(workSchedule);
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
        public async Task<IActionResult> Create([Bind("Name,IsWeek,Id,Commentary")] WorkSchedule workSchedule)
        {
            if (ModelState.IsValid)
            {
                workSchedule.Id = Guid.NewGuid();
                workSchedule.AppUserId = User.GetUserId();
                _bll.WorkSchedules.Add(workSchedule);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workSchedule);
        }

        // GET: Admin/WorkSchedule/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workSchedule = await _bll.WorkSchedules.FirstOrDefaultAsync(id.Value, User.GetUserId());
            if (workSchedule == null)
            {
                return NotFound();
            }

            return View(workSchedule);
        }

        // POST: Admin/WorkSchedule/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, WorkSchedule workSchedule)
        {
            workSchedule.AppUserId = User.GetUserId();
            if (id != workSchedule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.WorkSchedules.Update(workSchedule);
                    await _bll.SaveChangesAsync();
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
            return View(workSchedule);
        }

        // GET: Admin/WorkSchedule/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workSchedule = await _bll.WorkSchedules.RemoveAsync(id.Value, User.GetUserId());
            return View(workSchedule);
        }

        // POST: Admin/WorkSchedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var workSchedule = await _bll.WorkSchedules.FirstOrDefaultAsync(id, User.GetUserId());
            _bll.WorkSchedules.Remove(workSchedule!);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkScheduleExists(Guid id)
        {
            return _bll.WorkSchedules.Exists(id);
        }
    }
}

#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.DAL;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.App;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WorkDaysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorkDaysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/WorkDays
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.WorkDays.Include(w => w.WorkSchedule);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/WorkDays/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workDay = await _context.WorkDays
                .Include(w => w.WorkSchedule)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workDay == null)
            {
                return NotFound();
            }

            return View(workDay);
        }

        // GET: Admin/WorkDays/Create
        public IActionResult Create()
        {
            ViewData["WorkScheduleId"] = new SelectList(_context.WorkSchedules, "Id", "Name");
            return View();
        }

        // POST: Admin/WorkDays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkScheduleId,WorkDayStart,WorkDayEnd,WeekDay,Id,Commentary")] WorkDay workDay)
        {
            if (ModelState.IsValid)
            {
                workDay.Id = Guid.NewGuid();
                _context.Add(workDay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WorkScheduleId"] = new SelectList(_context.WorkSchedules, "Id", "Name", workDay.WorkScheduleId);
            return View(workDay);
        }

        // GET: Admin/WorkDays/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workDay = await _context.WorkDays.FindAsync(id);
            if (workDay == null)
            {
                return NotFound();
            }
            ViewData["WorkScheduleId"] = new SelectList(_context.WorkSchedules, "Id", "Name", workDay.WorkScheduleId);
            return View(workDay);
        }

        // POST: Admin/WorkDays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("WorkScheduleId,WorkDayStart,WorkDayEnd,WeekDay,Id,Commentary")] WorkDay workDay)
        {
            if (id != workDay.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workDay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkDayExists(workDay.Id))
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
            ViewData["WorkScheduleId"] = new SelectList(_context.WorkSchedules, "Id", "Name", workDay.WorkScheduleId);
            return View(workDay);
        }

        // GET: Admin/WorkDays/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workDay = await _context.WorkDays
                .Include(w => w.WorkSchedule)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workDay == null)
            {
                return NotFound();
            }

            return View(workDay);
        }

        // POST: Admin/WorkDays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var workDay = await _context.WorkDays.FindAsync(id);
            _context.WorkDays.Remove(workDay);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkDayExists(Guid id)
        {
            return _context.WorkDays.Any(e => e.Id == id);
        }
    }
}

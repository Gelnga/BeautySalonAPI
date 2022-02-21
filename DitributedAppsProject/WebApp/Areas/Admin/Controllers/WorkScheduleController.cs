#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App;
using Domain.App;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WorkScheduleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorkScheduleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/WorkSchedule
        public async Task<IActionResult> Index()
        {
            return View(await _context.WorkSchedules.ToListAsync());
        }

        // GET: Admin/WorkSchedule/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workSchedule = await _context.WorkSchedules
                .FirstOrDefaultAsync(m => m.Id == id);
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
                _context.Add(workSchedule);
                await _context.SaveChangesAsync();
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

            var workSchedule = await _context.WorkSchedules.FindAsync(id);
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
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,IsWeek,Id,Commentary")] WorkSchedule workSchedule)
        {
            if (id != workSchedule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workSchedule);
                    await _context.SaveChangesAsync();
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

            var workSchedule = await _context.WorkSchedules
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workSchedule == null)
            {
                return NotFound();
            }

            return View(workSchedule);
        }

        // POST: Admin/WorkSchedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var workSchedule = await _context.WorkSchedules.FindAsync(id);
            _context.WorkSchedules.Remove(workSchedule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkScheduleExists(Guid id)
        {
            return _context.WorkSchedules.Any(e => e.Id == id);
        }
    }
}

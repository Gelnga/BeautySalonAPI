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
    public class WorkerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorkerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Worker
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Workers.Include(w => w.JobPosition).Include(w => w.WorkSchedule);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Worker/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worker = await _context.Workers
                .Include(w => w.JobPosition)
                .Include(w => w.WorkSchedule)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (worker == null)
            {
                return NotFound();
            }

            return View(worker);
        }

        // GET: Admin/Worker/Create
        public IActionResult Create()
        {
            ViewData["JobPositionId"] = new SelectList(_context.JobPositions, "Id", "Name");
            ViewData["WorkScheduleId"] = new SelectList(_context.WorkSchedules, "Id", "Name");
            return View();
        }

        // POST: Admin/Worker/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobPositionId,WorkScheduleId,FirstName,LastName,Email,PhoneNumber,Id,Commentary")] Worker worker)
        {
            if (ModelState.IsValid)
            {
                worker.Id = Guid.NewGuid();
                _context.Add(worker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JobPositionId"] = new SelectList(_context.JobPositions, "Id", "Name", worker.JobPositionId);
            ViewData["WorkScheduleId"] = new SelectList(_context.WorkSchedules, "Id", "Name", worker.WorkScheduleId);
            return View(worker);
        }

        // GET: Admin/Worker/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worker = await _context.Workers.FindAsync(id);
            if (worker == null)
            {
                return NotFound();
            }
            ViewData["JobPositionId"] = new SelectList(_context.JobPositions, "Id", "Name", worker.JobPositionId);
            ViewData["WorkScheduleId"] = new SelectList(_context.WorkSchedules, "Id", "Name", worker.WorkScheduleId);
            return View(worker);
        }

        // POST: Admin/Worker/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("JobPositionId,WorkScheduleId,FirstName,LastName,Email,PhoneNumber,Id,Commentary")] Worker worker)
        {
            if (id != worker.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(worker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkerExists(worker.Id))
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
            ViewData["JobPositionId"] = new SelectList(_context.JobPositions, "Id", "Name", worker.JobPositionId);
            ViewData["WorkScheduleId"] = new SelectList(_context.WorkSchedules, "Id", "Name", worker.WorkScheduleId);
            return View(worker);
        }

        // GET: Admin/Worker/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worker = await _context.Workers
                .Include(w => w.JobPosition)
                .Include(w => w.WorkSchedule)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (worker == null)
            {
                return NotFound();
            }

            return View(worker);
        }

        // POST: Admin/Worker/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var worker = await _context.Workers.FindAsync(id);
            _context.Workers.Remove(worker);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkerExists(Guid id)
        {
            return _context.Workers.Any(e => e.Id == id);
        }
    }
}

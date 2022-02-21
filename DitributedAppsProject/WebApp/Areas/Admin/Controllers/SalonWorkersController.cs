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
    public class SalonWorkersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalonWorkersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/SalonWorkers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SalonWorkers.Include(s => s.Salon).Include(s => s.Worker);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/SalonWorkers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salonWorker = await _context.SalonWorkers
                .Include(s => s.Salon)
                .Include(s => s.Worker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salonWorker == null)
            {
                return NotFound();
            }

            return View(salonWorker);
        }

        // GET: Admin/SalonWorkers/Create
        public IActionResult Create()
        {
            ViewData["SalonId"] = new SelectList(_context.Salons, "Id", "Address");
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "Email");
            return View();
        }

        // POST: Admin/SalonWorkers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalonId,WorkerId,Id,Commentary")] SalonWorker salonWorker)
        {
            if (ModelState.IsValid)
            {
                salonWorker.Id = Guid.NewGuid();
                _context.Add(salonWorker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SalonId"] = new SelectList(_context.Salons, "Id", "Address", salonWorker.SalonId);
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "Email", salonWorker.WorkerId);
            return View(salonWorker);
        }

        // GET: Admin/SalonWorkers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salonWorker = await _context.SalonWorkers.FindAsync(id);
            if (salonWorker == null)
            {
                return NotFound();
            }
            ViewData["SalonId"] = new SelectList(_context.Salons, "Id", "Address", salonWorker.SalonId);
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "Email", salonWorker.WorkerId);
            return View(salonWorker);
        }

        // POST: Admin/SalonWorkers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("SalonId,WorkerId,Id,Commentary")] SalonWorker salonWorker)
        {
            if (id != salonWorker.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salonWorker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalonWorkerExists(salonWorker.Id))
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
            ViewData["SalonId"] = new SelectList(_context.Salons, "Id", "Address", salonWorker.SalonId);
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "Email", salonWorker.WorkerId);
            return View(salonWorker);
        }

        // GET: Admin/SalonWorkers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salonWorker = await _context.SalonWorkers
                .Include(s => s.Salon)
                .Include(s => s.Worker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salonWorker == null)
            {
                return NotFound();
            }

            return View(salonWorker);
        }

        // POST: Admin/SalonWorkers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var salonWorker = await _context.SalonWorkers.FindAsync(id);
            _context.SalonWorkers.Remove(salonWorker);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalonWorkerExists(Guid id)
        {
            return _context.SalonWorkers.Any(e => e.Id == id);
        }
    }
}

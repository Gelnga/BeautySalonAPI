#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.DAL;
using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.App;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SalonsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Salons
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Salons.Include(s => s.WorkSchedule);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Salons/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salon = await _context.Salons
                .Include(s => s.WorkSchedule)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salon == null)
            {
                return NotFound();
            }

            return View(salon);
        }

        // GET: Admin/Salons/Create
        public IActionResult Create()
        {
            ViewData["WorkScheduleId"] = new SelectList(_context.WorkSchedules, "Id", "Name");
            return View();
        }

        // POST: Admin/Salons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkScheduleId,Name,Description,Address,GoogleMapsLink,Email,PhoneNumber,Id,Commentary")] Salon salon)
        {
            if (ModelState.IsValid)
            {
                salon.Id = Guid.NewGuid();
                _context.Add(salon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WorkScheduleId"] = new SelectList(_context.WorkSchedules, "Id", "Name", salon.WorkScheduleId);
            return View(salon);
        }

        // GET: Admin/Salons/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salon = await _context.Salons.FindAsync(id);
            if (salon == null)
            {
                return NotFound();
            }
            ViewData["WorkScheduleId"] = new SelectList(_context.WorkSchedules, "Id", "Name", salon.WorkScheduleId);
            return View(salon);
        }

        // POST: Admin/Salons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("WorkScheduleId,Name,Description,Address,GoogleMapsLink,Email,PhoneNumber,Id,Commentary")] Salon salon)
        {
            if (id != salon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalonExists(salon.Id))
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
            ViewData["WorkScheduleId"] = new SelectList(_context.WorkSchedules, "Id", "Name", salon.WorkScheduleId);
            return View(salon);
        }

        // GET: Admin/Salons/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salon = await _context.Salons
                .Include(s => s.WorkSchedule)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salon == null)
            {
                return NotFound();
            }

            return View(salon);
        }

        // POST: Admin/Salons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var salon = await _context.Salons.FindAsync(id);
            _context.Salons.Remove(salon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalonExists(Guid id)
        {
            return _context.Salons.Any(e => e.Id == id);
        }
    }
}

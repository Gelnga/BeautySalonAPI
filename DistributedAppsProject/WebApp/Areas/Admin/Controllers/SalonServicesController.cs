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
    public class SalonServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalonServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/SalonServices
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SalonServices.Include(s => s.Salon).Include(s => s.Service).Include(s => s.Unit);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/SalonServices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salonService = await _context.SalonServices
                .Include(s => s.Salon)
                .Include(s => s.Service)
                .Include(s => s.Unit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salonService == null)
            {
                return NotFound();
            }

            return View(salonService);
        }

        // GET: Admin/SalonServices/Create
        public IActionResult Create()
        {
            ViewData["SalonId"] = new SelectList(_context.Salons, "Id", "Address");
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name");
            ViewData["UnitId"] = new SelectList(_context.Units, "Id", "Name");
            return View();
        }

        // POST: Admin/SalonServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalonId,ServiceId,UnitId,Price,Id,Commentary")] SalonService salonService)
        {
            if (ModelState.IsValid)
            {
                salonService.Id = Guid.NewGuid();
                _context.Add(salonService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SalonId"] = new SelectList(_context.Salons, "Id", "Address", salonService.SalonId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", salonService.ServiceId);
            ViewData["UnitId"] = new SelectList(_context.Units, "Id", "Name", salonService.UnitId);
            return View(salonService);
        }

        // GET: Admin/SalonServices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salonService = await _context.SalonServices.FindAsync(id);
            if (salonService == null)
            {
                return NotFound();
            }
            ViewData["SalonId"] = new SelectList(_context.Salons, "Id", "Address", salonService.SalonId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", salonService.ServiceId);
            ViewData["UnitId"] = new SelectList(_context.Units, "Id", "Name", salonService.UnitId);
            return View(salonService);
        }

        // POST: Admin/SalonServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("SalonId,ServiceId,UnitId,Price,Id,Commentary")] SalonService salonService)
        {
            if (id != salonService.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salonService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalonServiceExists(salonService.Id))
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
            ViewData["SalonId"] = new SelectList(_context.Salons, "Id", "Address", salonService.SalonId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", salonService.ServiceId);
            ViewData["UnitId"] = new SelectList(_context.Units, "Id", "Name", salonService.UnitId);
            return View(salonService);
        }

        // GET: Admin/SalonServices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salonService = await _context.SalonServices
                .Include(s => s.Salon)
                .Include(s => s.Service)
                .Include(s => s.Unit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salonService == null)
            {
                return NotFound();
            }

            return View(salonService);
        }

        // POST: Admin/SalonServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var salonService = await _context.SalonServices.FindAsync(id);
            _context.SalonServices.Remove(salonService);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalonServiceExists(Guid id)
        {
            return _context.SalonServices.Any(e => e.Id == id);
        }
    }
}

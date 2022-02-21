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
    public class ImageObjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ImageObjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ImageObjects
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ImageObjects.Include(i => i.Image).Include(i => i.Salon).Include(i => i.Service).Include(i => i.Worker);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/ImageObjects/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageObject = await _context.ImageObjects
                .Include(i => i.Image)
                .Include(i => i.Salon)
                .Include(i => i.Service)
                .Include(i => i.Worker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imageObject == null)
            {
                return NotFound();
            }

            return View(imageObject);
        }

        // GET: Admin/ImageObjects/Create
        public IActionResult Create()
        {
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Description");
            ViewData["SalonId"] = new SelectList(_context.Salons, "Id", "Address");
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name");
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "Email");
            return View();
        }

        // POST: Admin/ImageObjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImageId,SalonId,ServiceId,WorkerId,Id,Commentary")] ImageObject imageObject)
        {
            if (ModelState.IsValid)
            {
                imageObject.Id = Guid.NewGuid();
                _context.Add(imageObject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Description", imageObject.ImageId);
            ViewData["SalonId"] = new SelectList(_context.Salons, "Id", "Address", imageObject.SalonId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", imageObject.ServiceId);
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "Email", imageObject.WorkerId);
            return View(imageObject);
        }

        // GET: Admin/ImageObjects/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageObject = await _context.ImageObjects.FindAsync(id);
            if (imageObject == null)
            {
                return NotFound();
            }
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Description", imageObject.ImageId);
            ViewData["SalonId"] = new SelectList(_context.Salons, "Id", "Address", imageObject.SalonId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", imageObject.ServiceId);
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "Email", imageObject.WorkerId);
            return View(imageObject);
        }

        // POST: Admin/ImageObjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ImageId,SalonId,ServiceId,WorkerId,Id,Commentary")] ImageObject imageObject)
        {
            if (id != imageObject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(imageObject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImageObjectExists(imageObject.Id))
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
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Description", imageObject.ImageId);
            ViewData["SalonId"] = new SelectList(_context.Salons, "Id", "Address", imageObject.SalonId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", imageObject.ServiceId);
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "Email", imageObject.WorkerId);
            return View(imageObject);
        }

        // GET: Admin/ImageObjects/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageObject = await _context.ImageObjects
                .Include(i => i.Image)
                .Include(i => i.Salon)
                .Include(i => i.Service)
                .Include(i => i.Worker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imageObject == null)
            {
                return NotFound();
            }

            return View(imageObject);
        }

        // POST: Admin/ImageObjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var imageObject = await _context.ImageObjects.FindAsync(id);
            _context.ImageObjects.Remove(imageObject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImageObjectExists(Guid id)
        {
            return _context.ImageObjects.Any(e => e.Id == id);
        }
    }
}

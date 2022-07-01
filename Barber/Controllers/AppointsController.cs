using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Barber.Data;
using Barber.Models;

namespace Barber.Controllers
{
    public class AppointsController : Controller
    {
        private readonly BarberContext _context;

        public AppointsController(BarberContext context)
        {
            _context = context;
        }

        // GET: Appoints
        public async Task<IActionResult> Index()
        {
              return _context.Appoint != null ? 
                          View(await _context.Appoint.ToListAsync()) :
                          Problem("Entity set 'BarberContext.Appoint'  is null.");
        }

        public IActionResult Check()
        {
            return View();
        }

        public async Task<IActionResult> CheckAvaibility(DateTime? Date) 
        {
            if (Date == null || _context.Appoint == null)
            {
                ViewData["results"] = "";
                return View(null);
              
            }
            
            var appoints = await _context.Appoint
                .Where(m => m.Date == Date).ToListAsync();
            if (appoints.Count == 0)
            {
                ViewData["results"] = null;
                return View(null);
            }
           
            ViewData["results"] = appoints;
            return View(appoints);
        }

        // GET: Appoints/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Appoint == null)
            {
                return NotFound();
            }

            var appoint = await _context.Appoint
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appoint == null)
            {
                return NotFound();
            }

            return View(appoint);
        }

        // GET: Appoints/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Appoints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PhoneNumber,Date,Time")] Appoint appoint)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appoint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appoint);
        }

        // GET: Appoints/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Appoint == null)
            {
                return NotFound();
            }

            var appoint = await _context.Appoint.FindAsync(id);
            if (appoint == null)
            {
                return NotFound();
            }
            return View(appoint);
        }

        // POST: Appoints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PhoneNumber,Date,Time")] Appoint appoint)
        {
            if (id != appoint.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appoint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointExists(appoint.Id))
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
            return View(appoint);
        }

        // GET: Appoints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Appoint == null)
            {
                return NotFound();
            }

            var appoint = await _context.Appoint
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appoint == null)
            {
                return NotFound();
            }

            return View(appoint);
        }

        // POST: Appoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Appoint == null)
            {
                return Problem("Entity set 'BarberContext.Appoint'  is null.");
            }
            var appoint = await _context.Appoint.FindAsync(id);
            if (appoint != null)
            {
                _context.Appoint.Remove(appoint);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointExists(int id)
        {
          return (_context.Appoint?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

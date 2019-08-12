using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CondoEntryLogs.Models;

namespace CondoEntryLogs.Controllers
{
    public class VisitorsController : Controller
    {
        private readonly CondoContext _context;

        public VisitorsController(CondoContext context)
        {
            _context = context;
        }

        // GET: Visitors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Visitors.ToListAsync());
        }

        // GET: Visitors
        public async Task<IActionResult> GetRemainingGuests()
        {
            return View(await _context.Visitors.Where(v => v.TimeOfExit == null).ToListAsync());
        }

        // GET: Visitors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitor = await _context.Visitors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visitor == null)
            {
                return NotFound();
            }

            return View(visitor);
        }

        // GET: Visitors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Visitors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,PhoneNumber,UnitNumber")] Visitor visitor)
        {
            if (ModelState.IsValid)
            {
                visitor.TimeOfEntry = DateTime.Now;
                _context.Add(visitor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(visitor);
        }

        // GET: Visitors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitor = await _context.Visitors.FindAsync(id);
            if (visitor == null)
            {
                return NotFound();
            }
            return View(visitor);
        }

        // POST: Visitors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,PhoneNumber,TimeOfEntry,TimeOfExit,UnitNumber")] Visitor visitor)
        {
            if (id != visitor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visitor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitorExists(visitor.Id))
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
            return View(visitor);
        }

        // GET: Visitors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitor = await _context.Visitors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visitor == null)
            {
                return NotFound();
            }

            return View(visitor);
        }

        // POST: Visitors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var visitor = await _context.Visitors.FindAsync(id);
            _context.Visitors.Remove(visitor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Visitors/Delete/5
        [HttpGet, ActionName("SetExitTime")]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetExitTime(int id)
        {
            var visitor = await _context.Visitors.FindAsync(id);
            visitor.TimeOfExit = DateTime.Now;
            _context.Update(visitor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisitorExists(int id)
        {
            return _context.Visitors.Any(e => e.Id == id);
        }
    }
}

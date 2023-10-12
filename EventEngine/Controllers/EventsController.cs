using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventEngine.Data;
using EventEngine.Models;

namespace EventEngine.Controllers
{
    public class EventsController : Controller
    {
        private readonly EventEngineContext _context;

        public EventsController(EventEngineContext context)
        {
            _context = context;
        }

        // GET: Events
        public async Task<IActionResult> Index(string search, bool IsIndoor, int? categoryFilter, string sortCriteria)
        {
            if (_context.Event == null)
            {
                return Problem("Entity set 'EventEngineContext.Event'  is null.");
            }

            List<Category> categories = _context.Category.ToList();

            // Pass the list of categories to the view
            ViewBag.Categories = categories;

            // LINQ query to select events
            var @events = from e in _context.Event
                         select e;

            @events = @events.Where(e =>
             (String.IsNullOrEmpty(search) || e.Title.Contains(search)) &&
             (!IsIndoor || e.IsIndoor) &&
             (!categoryFilter.HasValue || e.CategoryID == categoryFilter) // Filter by selected category
            );

            switch (sortCriteria)
            {
                case "CostLowToHigh":
                    @events = @events.OrderBy(e => e.Cost);
                    break;
                case "CostHighToLow":
                    @events = @events.OrderByDescending(e => e.Cost);
                    break;
                case "CapacityLowToHigh":
                    @events = @events.OrderBy(e => e.Capacity);
                    break;
                case "CapacityHighToLow":
                    @events = @events.OrderByDescending(e => e.Capacity);
                    break;
                default:
                    // Default sorting criteria (e.g., by event title)
                    @events = @events.OrderBy(e => e.Title);
                    break;
            }
            return View(await @events.ToListAsync());
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Event == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Location,Cost,IsIndoor,IsOutdoor,Capacity")] Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Event == null)
            {
                return NotFound();
            }

            var @event = await _context.Event.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Location,Cost,IsIndoor,IsOutdoor,Capacity")] Event @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
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
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Event == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Event == null)
            {
                return Problem("Entity set 'EventEngineContext.Event'  is null.");
            }
            var @event = await _context.Event.FindAsync(id);
            if (@event != null)
            {
                _context.Event.Remove(@event);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
          return (_context.Event?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
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
             (!categoryFilter.HasValue || e.CategoryID == categoryFilter)
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
    }
}

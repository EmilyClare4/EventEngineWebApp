using System.Diagnostics;
using EventEngine.Data;
using EventEngine.Models;
using EventEngine.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EventEngine.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EventEngineContext _context;

        public HomeController(ILogger<HomeController> logger, EventEngineContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var popularEvents = _context.Event
                .Where(e => e.Title == "Cosy Cafe" || e.Title == "Paint 'n' Sip" || e.Title == "Hike")
                .ToArray();

            string[] imageFileNames = { "cafe.jpg", "hike.jpg", "paint.jpg" };

            var viewModel = new HomeIndexViewModel
            {
                Events = popularEvents,
                ImageFileNames = imageFileNames
            };

            return View(viewModel);
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using SlovenskeSlova.DataLayer;
using SlovenskeSlova.Models;
using System.Diagnostics;

namespace SlovenskeSlova.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index([FromServices] IDictionaryManager slovnik, int count = 5)
        {
            ViewData["count"] = count;
            return View(slovnik.GetWords(count));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

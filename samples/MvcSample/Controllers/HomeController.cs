using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcSample.Models;
using System.Diagnostics;

namespace MvcSample.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = Permissions.IsAdmin)]
        public IActionResult AdminOnly()
        {
            return View(nameof(Index));
        }

        [Authorize(Policy = Permissions.IsUser)]
        public IActionResult UserOnly()
        {
            return View(nameof(Index));
        }

        [Authorize(Policy = Permissions.IsAdminOrUser)]
        public IActionResult AdminOrUser()
        {
            return View(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

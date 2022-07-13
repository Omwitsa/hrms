using HRIS.IProviders;
using HRIS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace HRIS.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IScannerProvider _scannerProvider;

        public HomeController(ILogger<HomeController> logger, IScannerProvider scannerProvider)
        {
            _logger = logger;
            _scannerProvider = scannerProvider;
        }

        public IActionResult Index()
        {
            _scannerProvider.GetReaders();
            _scannerProvider.Enroll();
            return View();
        }

        public IActionResult Privacy()
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

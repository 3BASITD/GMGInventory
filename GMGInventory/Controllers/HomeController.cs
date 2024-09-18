using GMGInventory.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GMGInventory.Controllers
{
    [Authorize]
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

        public IActionResult Progress()
        {
            return View();
        }

        public IActionResult PrintSample()
        {
            return View();
        }

        public IActionResult PrintTest()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statuscode)
        {
            Response.StatusCode = statuscode;
            return statuscode switch
            {
                400 => View("~/Views/Shared/E400.cshtml"),
                403 => View("~/Views/Shared/E403.cshtml"),
                404 => View("~/Views/Shared/E404.cshtml"),
                500 => View("~/Views/Shared/E500.cshtml"),
                502 => View("~/Views/Shared/E502.cshtml"),
                503 => View("~/Views/Shared/E503.cshtml"),
                _ => View("~/Views/Shared/Error.cshtml")
            };
        }
    }

    public class HomeTaskRow
    {
        public string MD { get; set; }
        public string ST { get; set; }
    }
}
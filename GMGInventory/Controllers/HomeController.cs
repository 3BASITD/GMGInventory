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
            //Response.StatusDescription = "Forbidden"; //403
            //Response.StatusDescription = "Page not found"; //404
            //Response.StatusDescription = "Service Unavailable"; //503
            if (statuscode == 401)
            {
                return View($"/Views/Home/Login.cshtml");
            }
            else if (statuscode == 403 || statuscode == 404 || statuscode == 500)// || statuscode == 503)
            {
                return View($"/Views/Home/HTTP{statuscode}.cshtml");
            }
            else
            {
                ViewBag["statuscode"] = statuscode;
                return View();
            }
        }
    }

    public class HomeTaskRow
    {
        public string MD { get; set; }
        public string ST { get; set; }
    }
}
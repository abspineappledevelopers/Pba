using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using UCL.ISM.Client.Models;

namespace UCL.ISM.Client.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Culture"] = CultureInfo.CurrentUICulture.DisplayName;
            ViewData["Message"] = "Dette er det nye software til studie administrationen til afhjælpning af håndtering af internationale studerende.";
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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

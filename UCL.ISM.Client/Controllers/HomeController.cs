using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using UCL.ISM.Client.Models;
using UCL.ISM.Client.Infrastructure;

namespace UCL.ISM.Client.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole(UserRoles.Administration))
            {
                return RedirectToAction("Index", "Administration");
            }
            else if (User.Identity.IsAuthenticated && User.IsInRole(UserRoles.Interviewer))
            {
                return RedirectToAction("Index", "Interviewer");
            }
            else
            {
                ViewData["Culture"] = CultureInfo.CurrentUICulture.DisplayName;
                ViewData["Message"] = "Dette er det nye software til studie administrationen til afhjælpning af håndtering af internationale studerende."
                                        + "\r\n For at kunne anvende dette program, skal man være logget ind, og have tildelt en rolle i Azure Active Directory.";
            }
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

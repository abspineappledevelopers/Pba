using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UCL.ISM.Client.Infrastructure;

namespace UCL.ISM.Client.Controllers
{
    [Authorize]
    public class AdministrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// ("Role") Security Group - Student Administration = (GUID) "e102cb19-0f15-4a64-b781-446947355a51".
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = UserRoles.Administration)]
        public IActionResult Create_StudyField()
        {
            ViewData["Message"] = "StudyField test.";
            return View();
        }

        [Authorize(Roles = UserRoles.Administration)]
        public IActionResult Create_InterviewScheme()
        {
            ViewData["Message"] = "Interview test.";
            return View();
        }
    }
}
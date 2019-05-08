using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UCL.ISM.Client.Infrastructure;

namespace UCL.ISM.Client.Controllers
{
    [Authorize]
    public class InterviewerController : Controller
    {
        /// <summary>
        /// ("Role") Security Group - Interviewer = (GUID) "be407188-013b-43da-8c34-444f6a944b0f".
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = UserRoles.Interviewer)]
        public IActionResult Index()
        {
            ViewData["Title"] = "Interviewer";

            ViewData["Header1"] = "Ansøgere";

            ViewData["Calendar"] = "Kalender";
            return View();
        }
    }
}
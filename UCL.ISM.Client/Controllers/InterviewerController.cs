using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace UCL.ISM.Client.Controllers
{
    [Authorize]
    public class InterviewerController : Controller
    {
        /// <summary>
        /// ("Role") Security Group - Interviewer = (GUID) "be407188-013b-43da-8c34-444f6a944b0f".
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "be407188-013b-43da-8c34-444f6a944b0f")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
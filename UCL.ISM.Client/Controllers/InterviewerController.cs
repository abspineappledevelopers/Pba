using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace UCL.ISM.Client.Controllers
{
    [Authorize(Roles = "Interviewer")]
    public class InterviewerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
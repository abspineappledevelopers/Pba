using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace UCL.ISM.Client.Controllers
{
    [Authorize]
    public class AdministrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "DirectoryViewers")]
        public IActionResult Create_StudyField()
        {
            ViewData["Message"] = "StudyField test.";
            return View();
        }

        [Authorize(Roles = "DirectoryViewers")]
        public IActionResult Create_InterviewScheme()
        {
            ViewData["Message"] = "Interview test.";
            return View();
        }
    }
}
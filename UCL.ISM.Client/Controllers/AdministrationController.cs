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

        [Authorize(Roles = "e102cb19-0f15-4a64-b781-446947355a51")]
        public IActionResult Create_StudyField()
        {
            ViewData["Message"] = "StudyField test.";
            return View();
        }

        [Authorize(Roles = "e102cb19-0f15-4a64-b781-446947355a51")]
        public IActionResult Create_InterviewScheme()
        {
            ViewData["Message"] = "Interview test.";
            return View();
        }
    }
}
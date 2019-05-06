using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace UCL.ISM.Client.Controllers
{
    [Authorize(Roles = "Administration")]
    public class AdministrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create_StudyField()
        {
            return View();
        }

        public IActionResult Create_InterviewScheme()
        {
            return View();
        }
    }
}
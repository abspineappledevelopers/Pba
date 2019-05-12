﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UCL.ISM.Client.Infrastructure;
using System.Threading.Tasks;
using UCL.ISM.StudyField;

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
        public IActionResult Create_Applicant()
        {
            ViewData["Message"] = "Interview test.";
            return View();
        }

        [Authorize(Roles = UserRoles.Administration)]
        public async Task<IActionResult> createApplicant(int prio, IStudyField field, string fname, string lname, string age, string nationality, bool eu, string email, string interviewer)
        {
            try
            {
                
            }
            catch (System.Exception)
            {

                throw;
            }
            return View();
        }

        [Authorize(Roles = UserRoles.Administration)]
        public IActionResult Create_InterviewScheme()
        {
            return View();
        }
    }
}
﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UCL.ISM.Client.Infrastructure;
using System.Threading.Tasks;
using UCL.ISM.StudyField;

namespace UCL.ISM.Client.Controllers
{
    [Authorize(Roles = UserRoles.Administration)]
    public class AdministrationController : Controller
    {
        IStudyField _studyField;
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// ("Role") Security Group - Student Administration = (GUID) "e102cb19-0f15-4a64-b781-446947355a51".
        /// </summary>
        /// <returns></returns>
        public IActionResult Create_StudyField()
        {
            ViewData["Message"] = "StudyField test.";
            return View();
        }
        
        public IActionResult Create_Applicant()
        {
            _studyField = new UCL.ISM.StudyField.StudyField();
            //ViewData["Fields"] = _studyField.GetAllStudyFields();
            return View();
        }
        
        [HttpPost]
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

        public IActionResult Create_InterviewScheme()
        {
            return View();
        }
    }
}
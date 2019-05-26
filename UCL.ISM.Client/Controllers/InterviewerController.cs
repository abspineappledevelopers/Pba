using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UCL.ISM.Client.Infrastructure;
using UCL.ISM.BLL.BLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            Interviewer _interviewer = new Interviewer();
            string[] split = User.FindFirst("name").Value.Split(' ', 2);
            _interviewer.Firstname = split[0];
            _interviewer.Lastname = split[1];
            _interviewer.Id = User.FindFirst("oid").Value;

            if (_interviewer.GetInterviewer(_interviewer.Id) == null)
            {
                return RedirectToAction("CreateInterviewer", new { interviewer = _interviewer });
            }
            else
            {
                return RedirectToAction("GetApplicants", new { id = _interviewer.Id });
            }
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Interviewer)]
        public IActionResult CreateInterviewer(Interviewer interviewer)
        {
            interviewer.CreateInterviewer(interviewer);
            return View();
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Interviewer)]
        public IActionResult GetApplicants(string id)
        {
            Applicant appl = new Applicant();
            List<Applicant> temp = appl.GetAllApplicantsForInterviewer(id);
            List<Models.ApplicantVM> applicants = new List<Models.ApplicantVM>();

            foreach (Models.ApplicantVM item in temp)
            {
                applicants.Add(item);
            }

            return View("Index", applicants);
        }
        
        public IActionResult GetApplicantScheme(string id)
        {
            return View();
        }


    }
}
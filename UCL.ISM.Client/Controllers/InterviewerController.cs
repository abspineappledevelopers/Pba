using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UCL.ISM.Client.Infrastructure;
using UCL.ISM.BLL.BLL;
using System;
using System.Collections;
using System.Collections.Generic;

namespace UCL.ISM.Client.Controllers
{
    [Authorize]
    public class InterviewerController : Controller
    {
        Interviewer _interviewer;
        /// <summary>
        /// ("Role") Security Group - Interviewer = (GUID) "be407188-013b-43da-8c34-444f6a944b0f".
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = UserRoles.Interviewer)]
        public IActionResult Index()
        {
            _interviewer = new Interviewer();
            foreach (var item in User.Claims)
            {
                if (item.Type == "oid")
                {
                    _interviewer = _interviewer.GetInterviewer(item.Value);
                    break;
                }
            }
            
            Applicant appl = new Applicant();

            List<Applicant> applicants = appl.GetAllApplicantsForInterviewer(_interviewer.Id);



            return View("..Interviewer/Partials/_Applicants", applicants);
        }

        [Authorize(Roles = UserRoles.Interviewer)]
        public IActionResult CreateInterviewer()
        {

            return View();
        }

        [Authorize(Roles = UserRoles.Interviewer)]
        public IActionResult GetApplicants()
        {
            var user = HttpContext.Request.Query["Id"][0];
            _interviewer.GetInterviewer(user);
            Applicant appl = new Applicant();

            List<Applicant> applicants = appl.GetAllApplicantsForInterviewer(_interviewer.Id);



            return View("..Interviewer/Partials/_Applicants", applicants);
        }
        
    }
}
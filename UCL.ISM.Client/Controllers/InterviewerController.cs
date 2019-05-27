using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UCL.ISM.Client.Infrastructure;
using UCL.ISM.BLL.BLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace UCL.ISM.Client.Controllers
{
    [Authorize]
    public class InterviewerController : Controller
    {
        Interviewer _interviewer;

        public InterviewerController()
        {
            _interviewer = new Interviewer();
        }
        /// <summary>
        /// ("Role") Security Group - Interviewer = (GUID) "be407188-013b-43da-8c34-444f6a944b0f".
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = UserRoles.Interviewer)]
        public IActionResult Index()
        {
            if (_interviewer.GetInterviewer(User.FindFirst("oid").Value) == null)
            {
                return RedirectToAction("CreateInterviewer");
            }
            else
            {
                return RedirectToAction("GetApplicants");
            }
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Interviewer)]
        public IActionResult CreateInterviewer()
        {
            Models.InterviewerVM vm = new Models.InterviewerVM();
            string[] split = User.FindFirst("name").Value.Split(' ', 2);
            vm.Firstname = split[0];
            vm.Lastname = split[1];
            vm.Id = User.FindFirst("oid").Value;
            _interviewer.CreateInterviewer(vm);
            return View("Index");
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Interviewer)]
        public IActionResult GetApplicants()
        {
            Models.InterviewerVM vm = new Models.InterviewerVM();
            vm.Id = User.FindFirst("oid").Value;

            Applicant appl = new Applicant();
            List<Applicant> temp = appl.GetAllApplicantsForInterviewer(vm.Id);
            List<Models.ApplicantVM> applicants = new List<Models.ApplicantVM>();

            foreach (Models.ApplicantVM item in temp)
            {
                applicants.Add(item);
            }

            return View("Index", applicants);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Interviewer)]
        public IActionResult Add_InterviewSchemeToApplicant(Models.ApplicantVM model)
        {
            Applicant _applicant = new Applicant();
            _applicant.AddInterviewSchemeToApplicant(model);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = UserRoles.Interviewer)]
        public IActionResult GetApplicantScheme(Models.ApplicantVM model)
        {
            Applicant app = new Applicant();
            app.InterviewScheme.GetInterviewSchemeAndQuestions(model.InterviewScheme.Id);
            
            
            return View();
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Interviewer)]
        public IActionResult Add_Answer(Models.ApplicantVM model)
        {
            Applicant app = new Applicant();
            app.InterviewScheme.GetInterviewSchemeAndQuestions(model.InterviewScheme.Id);
            app.InterviewScheme.AddAnswerToInterview(model, model.InterviewScheme.Question);
            return View("Index");
        }

        [Authorize(Roles = UserRoles.Interviewer)]
        public IActionResult Get_InterviewScheme_Modal(string id)
        {
            InterviewScheme _interviewScheme = new InterviewScheme();
            var schemes = _interviewScheme.GetSpecificInterviewSchemes(id);

            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var sch in schemes)
            {
                list.Add(new SelectListItem() { Text = sch.Name, Value = sch.Id.ToString() });
            }

            Models.ApplicantVM vm = new Models.ApplicantVM()
            {
                Id = Guid.Parse(id),
                InterviewSchemes = list
            };

            return PartialView("../Interviewer/Partials/Add_InterviewScheme", vm);
        }

        [Authorize(Roles = UserRoles.Interviewer)]
        public IActionResult Get_Interview_Modal(string id)
        {
            Models.ApplicantVM app = new Models.ApplicantVM();
            Applicant temp = new Applicant();
            app = temp.GetApplicant(id);
            InterviewScheme _interviewScheme = new InterviewScheme();
            var quest = _interviewScheme.GetInterviewSchemeAndQuestions(app.InterviewScheme.Id);
            foreach (var item in quest.Questions)
            {
                app.InterviewScheme.Questions.Add(new Models.QuestionVM() { Id = item.Id, Order = item.Order, Answer = item.Answer, InterviewSchemeId = item.InterviewSchemeId, Quest = item.Quest });
            }
            app.InterviewScheme.Questions.OrderBy(x => x.Order).ToList();


            return PartialView("../Interviewer/Partials/DoInterview", app);
        }
    }
}
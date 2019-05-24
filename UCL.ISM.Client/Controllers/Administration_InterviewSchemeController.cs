using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UCL.ISM.BLL;
using UCL.ISM.BLL.BLL;
using UCL.ISM.Client.Infrastructure;
using UCL.ISM.Client.Models;

namespace UCL.ISM.Client.Controllers
{
    public class Administration_InterviewSchemeController : Controller
    {
        StudyField _studyField;
        Nationality _nationality;
        InterviewScheme _interviewScheme;

        [Authorize(Roles = UserRoles.Administration)]
        public IActionResult Create_InterviewScheme()
        {
            _nationality = new Nationality();
            _studyField = new StudyField();
            
            InterviewSchemeVM vm = new InterviewSchemeVM();

            var nationalities = _nationality.GetAllNationalities();
            var studyfields = _studyField.GetAllStudyFields();
            foreach (var country in nationalities)
            {
                vm.Countries.Add(new SelectListItem() { Text = country.Name, Value = country.Id.ToString() });
            }
            foreach (var study in studyfields)
            {
                vm.Studyfields.Add(new SelectListItem() { Text = study.FieldName, Value = study.Id.ToString() });
            }

            return View("../Administration/Create_InterviewScheme", vm);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Administration)]
        public IActionResult Pass_InterviewScheme(InterviewSchemeVM model)
        {
            _interviewScheme = new InterviewScheme();
            _nationality = new Nationality();
            _studyField = new StudyField();

            var id = _interviewScheme.AddInterviewSchema(model);

            var nationalities = _nationality.GetAllNationalities();
            var studyfields = _studyField.GetAllStudyFields();
            foreach (var country in nationalities)
            {
                model.Countries.Add(new SelectListItem() { Text = country.Name, Value = country.Id.ToString() });
            }
            foreach(var study in studyfields)
            {
                model.Studyfields.Add(new SelectListItem() { Text = study.FieldName, Value = study.Id.ToString() });
            }

            model.Id = id;
            var questions = _interviewScheme.GetQuestions(model.Id);

            foreach (var quest in questions)
            {
                model.Questions.Add(new QuestionVM() { Id = quest.Id, Answer = quest.Answer, InterviewSchemeId = quest.InterviewSchemeId, Quest = quest.Quest });
            }

            return View("../Administration/CreateQuestionToInterview", model);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Administration)]
        public IActionResult Pass_InterviewScheme_Question(InterviewSchemeVM model)
        {

            _interviewScheme = new InterviewScheme();
            _nationality = new Nationality();
            _studyField = new StudyField();

            model.Question.InterviewSchemeId = model.Id;

            var countquestions = _interviewScheme.GetQuestions(model.Id);

            if (countquestions.Count == 0)
            {
                model.Question.Order = 1;
            }
            else
            {
                model.Question.Order = countquestions.Count + 1;
            }

            _interviewScheme.AddQuestionToInterview(model.Question);
            
            var nationalities = _nationality.GetAllNationalities();
            var studyfields = _studyField.GetAllStudyFields();
            foreach (var country in nationalities)
            {
                model.Countries.Add(new SelectListItem() { Text = country.Name, Value = country.Id.ToString() });
            }
            foreach (var study in studyfields)
            {
                model.Studyfields.Add(new SelectListItem() { Text = study.FieldName, Value = study.Id.ToString() });
            }

            var questions = _interviewScheme.GetQuestions(model.Id);
            foreach (var quest in questions)
            {
                model.Questions.Add(new QuestionVM() { Id = quest.Id, Order = quest.Order, Answer = quest.Answer, InterviewSchemeId = quest.InterviewSchemeId, Quest = quest.Quest });
            }

            model.Questions = model.Questions.OrderBy(x => x.Order).ToList();

            return View("../Administration/CreateQuestionToInterview", model);
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Administration)]
        public IActionResult Get_All_InterviewScheme()
        {
            _interviewScheme = new InterviewScheme();

            InterviewSchemeVM vm;
            List<InterviewSchemeVM> listvm = new List<InterviewSchemeVM>();

            var list = _interviewScheme.GetAllInterviewSchemesAndQuestions();

            foreach(var item in list)
            {
                vm = new InterviewSchemeVM();
                vm = item;
                //vm.Questions = new List<QuestionVM>();
                foreach (var quest in item.Questions)
                {
                    vm.Questions.Add(new QuestionVM() { Id = quest.Id, Order = quest.Order, InterviewSchemeId = quest.InterviewSchemeId, Quest = quest.Quest });
                }
                vm.Questions = vm.Questions.OrderBy(x => x.Order).ToList();
                listvm.Add(vm);
            }

            return View("../Administration/Get_All_InterviewSchemes_And_Questions", listvm);
        }
    }
}
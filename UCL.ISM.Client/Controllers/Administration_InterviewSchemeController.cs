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
            foreach (var study in studyfields)
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
                countquestions = countquestions.OrderBy(x => x.Order).ToList();
                var last = countquestions.LastOrDefault();
                model.Question.Order = last.Order + 1;
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

        [HttpPost]
        [Authorize(Roles = UserRoles.Administration)]
        public IActionResult Pass_Edited_InterviewScheme_Question(QuestionVM model)
        {
            _interviewScheme = new InterviewScheme();
            
            _interviewScheme.UpdateQuestion(model);
            
            return RedirectToAction("Get_Specific_Interview", new { id = model.InterviewSchemeId });
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Administration)]
        public IActionResult Get_All_InterviewScheme()
        {
            _interviewScheme = new InterviewScheme();
            
            List<InterviewSchemeVM> listvm = new List<InterviewSchemeVM>();

            var list = _interviewScheme.GetAllInterviewSchemes();

            foreach(var item in list)
            {
                listvm.Add(item);
            }
            

            return View("../Administration/Get_All_InterviewSchemes_And_Questions", listvm);
        }

        
        [Authorize(Roles = UserRoles.Administration)]
        public IActionResult Get_Specific_Interview(int? id)
        {
            _interviewScheme = new InterviewScheme();
            _nationality = new Nationality();
            _studyField = new StudyField();


            InterviewSchemeVM vm = new InterviewSchemeVM();

            var model = _interviewScheme.GetInterviewSchemeAndQuestions(id);
            vm = model;

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

            foreach (var quest in model.Questions)
            {
                vm.Questions.Add(new QuestionVM() { Id = quest.Id, Order = quest.Order, InterviewSchemeId = quest.InterviewSchemeId, Quest = quest.Quest });
            }
            vm.Questions = vm.Questions.OrderBy(x => x.Order).ToList();

            return View("../Administration/Specific_Interview_And_Questions", vm);
        }
        
        [Authorize(Roles = UserRoles.Administration)]
        public IActionResult Delete_InterviewScheme_Question(string id, string schemeid)
        {
            _interviewScheme = new InterviewScheme();
            
            _interviewScheme.RemoveQuestion(Guid.Parse(id));

            return RedirectToAction("Get_Specific_Interview", new { id = schemeid });
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Administration)]
        public IActionResult Pass_new_Question(InterviewSchemeVM model)
        {

            _interviewScheme = new InterviewScheme();

            model.Question.InterviewSchemeId = model.Id;

            var countquestions = _interviewScheme.GetQuestions(model.Id);
            
            if (countquestions.Count == 0)
            {
                model.Question.Order = 1;
            }
            else
            {
                countquestions = countquestions.OrderBy(x => x.Order).ToList();
                var last = countquestions.LastOrDefault();
                model.Question.Order = last.Order + 1;
            }

            _interviewScheme.AddQuestionToInterview(model.Question);

            return RedirectToAction("Get_Specific_Interview", new { id = model.Id });
        }

        //[HttpPost]
        //[Authorize(Roles = UserRoles.Administration)]
        //public IActionResult New_Question_To_Modal(InterviewSchemeVM vm)
        //{
        //    var index = vm.Questions.Count + 1;
        //    vm.Question.Order = index;

        //    vm.Questions.Add(vm.Question);

        //    return PartialView("../Administration/Partials/Get_All_InterviewSchemes_And_Questions", vm);
        //}

        [HttpPost]
        [Authorize(Roles = UserRoles.Administration)]
        public IActionResult Save_Changes_InterviewSchemeAndQuestions(InterviewSchemeVM vm)
        {
            _interviewScheme = new InterviewScheme();

            _interviewScheme.UpdateInterviewScheme(vm);

            return RedirectToAction("Get_Specific_Interview", new { id = vm.Id });
        }
        
        [Authorize(Roles = UserRoles.Administration)]
        public IActionResult Delete_InterviewSchemeAndQuestions(InterviewSchemeVM vm)
        {
            _interviewScheme = new InterviewScheme();

            _interviewScheme.DeleteInterviewScheme(vm.Id);

            return RedirectToAction("Get_All_InterviewScheme");
        }
    }
}
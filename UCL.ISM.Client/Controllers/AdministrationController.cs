using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UCL.ISM.Client.Infrastructure;
using System.Threading.Tasks;
using UCL.ISM.BLL.Interface;
using UCL.ISM.BLL;
using UCL.ISM.Client.Models;
using System.Collections.Generic;
using UCL.ISM.BLL.BLL;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;

namespace UCL.ISM.Client.Controllers
{
    [Authorize]
    public class AdministrationController : Controller
    {
        IStudyField _studyField;
        INationality _nationality;
        IApplicant _applicant;
        IInterviewer _interviewer;
        InterviewScheme _interviewScheme;

        public IActionResult Index()
        {
            //_studyField = new StudyField();
            _applicant = new Applicant();
            //_nationality = new Nationality();
            //_interviewer = new Interviewer();
            //ApplicantVM appvm;
            
            List<IApplicant> listapp = new List<IApplicant>();

            //var studyfields = _studyField.GetAllStudyFields();
            //var nationalities = _nationality.GetAllNationalities();
            //var interviewers = _interviewer.GetAllInterviewers();
            var list = _applicant.GetAllApplicantsWithoutSchema();
            foreach(var app in list)
            {
                //appvm = new ApplicantVM();
                //appvm = app;
                IApplicant applicant = app;
                //foreach (var st in studyfields)
                //{
                //    appvm.StudyFields.Add(new SelectListItem() { Text = st.FieldName, Value = st.Id.ToString() });
                //}

                //foreach (var na in nationalities)
                //{
                //    appvm.Nationalities.Add(new SelectListItem() { Text = na.Name, Value = na.Id.ToString() });
                //}

                //foreach (var inn in interviewers)
                //{
                //    appvm.Interviewers.Add(new SelectListItem() { Text = inn.Firstname + " " + inn.Lastname, Value = inn.Id.ToString() });
                //}

                listapp.Add(applicant);
            }

            return View("../Administration/Index", listapp);
        }

        /// <summary>
        /// ("Role") Security Group - Student Administration = (GUID) "e102cb19-0f15-4a64-b781-446947355a51".
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = UserRoles.Administration)]
        public IActionResult Create_StudyField()
        {
            _studyField = new StudyField();

            //var allStudyFields = _studyField.GetAllStudyFields();
            //foreach (var studyfield in allStudyFields)
            //{
               // model.All
                //sfvm = new StudyFieldVM();
                //sfvm = studyfield;
                //model.AllStudyFields.Add(sfvm);
            //}

            return View("../Administration/Create_StudyField", _studyField.GetAllStudyFields());
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Administration)]
        public IActionResult Pass_New_StudyField(StudyFieldVM model)
        {
            _studyField = new StudyField();

            _studyField.CreateNewStudyField(model.FieldName);

            return RedirectToAction("Create_StudyField");
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Administration)]
        public IActionResult Edit_StudyField(IStudyField model)
        {
            _studyField = new StudyField();
            
            _studyField.EditStudyField(model);

            return RedirectToAction("Create_StudyField");
        }
        
        [Authorize(Roles = UserRoles.Administration)]
        public IActionResult Delete_StudyField(int Id)
        {
            _studyField = new StudyField();

            _studyField.DeleteStudyField(Id);

            return RedirectToAction("Create_StudyField", "Administration");
        }

        [Authorize(Roles = UserRoles.Administration)]
        public IActionResult Create_Applicant()
        {
            _studyField = new StudyField();
            _nationality = new Nationality();
            _interviewer = new Interviewer();

            ApplicantVM vm = new ApplicantVM();

            var studyfields = _studyField.GetAllStudyFields();
            var nationalities = _nationality.GetAllNationalities();
            var interviewers = _interviewer.GetAllInterviewers();

            foreach(var sf in studyfields)
            {
                vm.StudyFields.Add(new SelectListItem() { Text = sf.FieldName, Value = sf.Id.ToString() });
            }

            foreach(var na in nationalities)
            {
                vm.Nationalities.Add(new SelectListItem() { Text = na.Name, Value = na.Id.ToString() });
            }

            foreach(var inn in interviewers)
            {
                vm.Interviewers.Add(new SelectListItem() { Text = inn.Firstname + " " + inn.Lastname, Value = inn.Id.ToString() });
            }

            return View("../Administration/Create_Applicant", vm);
        }

        [Authorize(Roles = UserRoles.Administration)]
        public async Task<IActionResult> createApplicant(ApplicantVM model)
        {
            try
            {
                _applicant = new Applicant();
                _nationality = new Nationality();
                model.Id = Guid.NewGuid();
                model.IsEU = _nationality.IsEu(model.Nationality.Id);

                _applicant.CreateNewApplicant(model);
            }
            catch
            {
                return RedirectToAction("Create_Applicant");
            }

            return RedirectToAction("Create_Applicant");
        }

        
        public IActionResult Get_Interview_Modal(string id)
        {
            _interviewer = new Interviewer();
            var interviewers = _interviewer.GetAllInterviewers();
            
            List<SelectListItem> list = new List<SelectListItem>();
            foreach(var inn in interviewers)
            {
                list.Add(new SelectListItem() { Text = inn.Firstname + " " + inn.Lastname, Value = inn.Id.ToString() });
            }
            
            ApplicantVM vm = new ApplicantVM()
            {
                Id = Guid.Parse(id),
                Interviewers = list
            };

            return PartialView("../Administration/Partials/_PopulateModalWithInterviewers", vm);
        }

        public IActionResult Get_InterviewScheme_Modal(string id)
        {
            _interviewScheme = new InterviewScheme();
            var schemes = _interviewScheme.GetAllInterviewSchemes();

            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var sch in schemes)
            {
                list.Add(new SelectListItem() { Text = sch.Name, Value = sch.Id.ToString() });
            }

            ApplicantVM vm = new ApplicantVM()
            {
                Id = Guid.Parse(id),
                InterviewSchemes = list
            };

            return PartialView("../Administration/Partials/_PopulateModalWithInterviewSchemes", vm);
        }

        public IActionResult Get_Applicant_Modal(string id)
        {
            _applicant = new Applicant();
            ApplicantVM vm = new ApplicantVM();

            vm = _applicant.GetApplicant(id);

            _studyField = new StudyField();
            _nationality = new Nationality();
            _interviewer = new Interviewer();
            _interviewScheme = new InterviewScheme();
            var studyfields = _studyField.GetAllStudyFields();
            var nationalities = _nationality.GetAllNationalities();
            var interviewers = _interviewer.GetAllInterviewers();
            var schemes = _interviewScheme.GetAllInterviewSchemes();

            foreach (var sf in studyfields)
            {
                vm.StudyFields.Add(new SelectListItem() { Text = sf.FieldName, Value = sf.Id.ToString() });
            }

            foreach (var na in nationalities)
            {
                vm.Nationalities.Add(new SelectListItem() { Text = na.Name, Value = na.Id.ToString() });
            }

            foreach (var inn in interviewers)
            {
                vm.Interviewers.Add(new SelectListItem() { Text = inn.Firstname + " " + inn.Lastname, Value = inn.Id.ToString() });
            }

            foreach (var sch in schemes)
            {
                vm.InterviewSchemes.Add(new SelectListItem() { Text = sch.Name, Value = sch.Id.ToString() });
            }

            return PartialView("../Administration/Partials/_PopulateModalWithApplicant", vm);
        }

        [HttpPost]
        public IActionResult Add_Interviewer(ApplicantVM model)
        {
            _applicant = new Applicant();
            _applicant.AddInterviewerToApplicant(model);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Add_InterviewSchemeToApplicant(ApplicantVM model)
        {
            _applicant = new Applicant();
            _applicant.AddInterviewSchemeToApplicant(model);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = UserRoles.Administration)]
        public IActionResult Create_InterviewScheme()
        {
            _nationality = new Nationality();
            //_interviewScheme = new InterviewScheme();
            InterviewSchemeVM vm = new InterviewSchemeVM();

            var nationalities = _nationality.GetAllNationalities();
            foreach(var country in nationalities)
            {
                vm.Countries.Add(new SelectListItem() { Text = country.Name, Value = country.Id.ToString() });
            }

            return View("../Administration/Create_InterviewScheme", vm);
        }

        [HttpPost]
        public IActionResult Pass_InterviewScheme(InterviewSchemeVM model)
        {
            _interviewScheme = new InterviewScheme();
            _nationality = new Nationality();

            var id = _interviewScheme.AddInterviewSchema(model);

            var nationalities = _nationality.GetAllNationalities();
            foreach (var country in nationalities)
            {
                model.Countries.Add(new SelectListItem() { Text = country.Name, Value = country.Id.ToString() });
            }

            model.Id = id;
            model.Questions = _interviewScheme.GetQuestions(id);

            return View("../Administration/CreateQuestionToInterview", model);
        }

        [HttpPost]
        public IActionResult Pass_InterviewScheme_Question(InterviewSchemeVM model)
        {
            _interviewScheme = new InterviewScheme();
            _nationality = new Nationality();

            model.Question.InterviewSchemeId = model.Id;
            
            _interviewScheme.AddQuestionToInterview(model.Question);

            var nationalities = _nationality.GetAllNationalities();
            foreach (var country in nationalities)
            {
                model.Countries.Add(new SelectListItem() { Text = country.Name, Value = country.Id.ToString() });
            }

            model.Questions = _interviewScheme.GetQuestions(model.Id);

            model.Question = new Question();

            return View("../Administration/CreateQuestionToInterview", model);
        }

        
        public IActionResult SortByPriority()
        {
            _applicant = new Applicant();
            ApplicantVM appvm;

            List<ApplicantVM> listapp = new List<ApplicantVM>();
            
            var list = _applicant.GetAllApplicantsWithoutSchema();
            foreach (var app in list)
            {
                appvm = new ApplicantVM();
                appvm = app;

                listapp.Add(appvm);
            }

            var ordered = listapp.OrderBy(x => x.Priority).ToList();

            return View("../Administration/Index", ordered);
        }

        public IActionResult SortByEU()
        {
            ApplicantVM appvm;
            _applicant = new Applicant();
            List<ApplicantVM> listapp = new List<ApplicantVM>();
            
            var list = _applicant.GetAllApplicantsWithoutSchema();

            foreach (var app in list)
            {
                if (app.IsEU)
                {
                    appvm = new ApplicantVM();
                    appvm = app;

                    listapp.Add(appvm);
                }                
            }

            return View("../Administration/Index", listapp);
        }

        public IActionResult SortByEUPriority()
        {
            _applicant = new Applicant();
            ApplicantVM appvm;

            List<ApplicantVM> listapp = new List<ApplicantVM>();

            var list = _applicant.GetAllApplicantsWithoutSchema();
            foreach (var app in list)
            {
                if (app.IsEU)
                {
                    appvm = new ApplicantVM();
                    appvm = app;

                    listapp.Add(appvm);
                }
            }

            var ordered = listapp.OrderBy(x => x.Priority).ToList();

            return View("../Administration/Index", ordered);
        }

        public IActionResult SortByNonEU()
        {
            ApplicantVM appvm;
            _applicant = new Applicant();
            List<ApplicantVM> listapp = new List<ApplicantVM>();

            var list = _applicant.GetAllApplicantsWithoutSchema();

            foreach (var app in list)
            {
                if (!app.IsEU)
                {
                    appvm = new ApplicantVM();
                    appvm = app;

                    listapp.Add(appvm);
                }
            }

            return View("../Administration/Index", listapp);
        }

        public IActionResult SortByNonEUPriority()
        {
            ApplicantVM appvm;
            _applicant = new Applicant();
            List<ApplicantVM> listapp = new List<ApplicantVM>();

            var list = _applicant.GetAllApplicantsWithoutSchema();

            foreach (var app in list)
            {
                if (!app.IsEU)
                {
                    appvm = new ApplicantVM();
                    appvm = app;

                    listapp.Add(appvm);
                }
            }

            var ordered = listapp.OrderBy(x => x.Priority).ToList();

            return View("../Administration/Index", ordered);
        }
    }
}
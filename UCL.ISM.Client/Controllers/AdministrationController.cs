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
        StudyField _studyField;
        Nationality _nationality;
        Applicant _applicant;
        Interviewer _interviewer;
        InterviewScheme _interviewScheme;

        public IActionResult Index()
        {
            _studyField = new StudyField();
            _applicant = new Applicant();
            _nationality = new Nationality();
            _interviewer = new Interviewer();
            ApplicantVM appvm;
            
            List<ApplicantVM> listapp = new List<ApplicantVM>();

            var studyfields = _studyField.GetAllStudyFields();
            var nationalities = _nationality.GetAllNationalities();
            var interviewers = _interviewer.GetAllInterviewers();
            var list = _applicant.GetAllApplicantsWithoutSchemaOrInterviewer();
            foreach(var app in list)
            {
                appvm = new ApplicantVM();
                appvm = app;
                //ApplicantVM applicant = (Applicant)app;
                foreach (var st in studyfields)
                {
                    appvm.StudyFields.Add(new SelectListItem() { Text = st.FieldName, Value = st.Id.ToString() });
                }

                foreach (var na in nationalities)
                {
                    appvm.Nationalities.Add(new SelectListItem() { Text = na.Name, Value = na.Id.ToString() });
                }

                foreach (var inn in interviewers)
                {
                    appvm.Interviewers.Add(new SelectListItem() { Text = inn.Firstname + " " + inn.Lastname, Value = inn.Id.ToString() });
                }

                listapp.Add(appvm);
            }

            return View("../Administration/Index", listapp);
        }

        /// <summary>
        /// ("Role") Security Group - Student Administration = (GUID) "e102cb19-0f15-4a64-b781-446947355a51".
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = UserRoles.Administration)]
        public IActionResult Create_StudyField(StudyFieldVM model)
        {
            _studyField = new StudyField();

            var allStudyFields = _studyField.GetAllStudyFields();
            foreach (var studyfield in allStudyFields)
            {
                //model.All
                StudyFieldVM sfvm = new StudyFieldVM();
                sfvm = studyfield;
                //
                model.AllStudyFields.Add(sfvm);
            }

            return View("../Administration/Create_StudyField", model);
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
        public IActionResult Edit_StudyField(StudyFieldVM model)
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

        [HttpGet]
        [Authorize(Roles = UserRoles.Administration)]
        public IActionResult Applicant_Process()
        {
            _applicant = new Applicant();
            var data = _applicant.GetAllApplicantsLimitedData();
            LimitedApplicantData vm;
            List<LimitedApplicantData> list = new List<LimitedApplicantData>();
            foreach(var app in data)
            {
                vm = new LimitedApplicantData()
                {
                    Id = app.Id,
                    Firstname = app.Firstname,
                    Lastname = app.Lastname,
                    ProcessId = app.Process.Id,
                    Process = app.Process.Process,
                    InterviewerName = app.Interviewer.Firstname + " " + app.Interviewer.Lastname
                };

                list.Add(vm);
            }

            return View("../Administration/ApplicantsProcess", list);
        }

        [Authorize(Roles = UserRoles.Administration)]
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

        [Authorize(Roles = UserRoles.Administration)]
        public IActionResult Get_InterviewScheme_Modal(string id)
        {
            _interviewScheme = new InterviewScheme();
            var schemes = _interviewScheme.GetSpecificInterviewSchemes(id);

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

        [Authorize(Roles = UserRoles.Administration)]
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
            var schemes = _interviewScheme.GetSpecificInterviewSchemes(id);

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
        [Authorize(Roles = UserRoles.Administration)]
        public IActionResult Pass_Edited_Applicant(ApplicantVM model)
        {
            _applicant = new Applicant();
            ApplicantVM vm = new ApplicantVM();
            vm = _applicant.EditApplicant(model);
            string Id = vm.Id.ToString();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Administration)]
        public IActionResult Add_Interviewer(ApplicantVM model)
        {
            _applicant = new Applicant();
            _applicant.AddInterviewerToApplicant(model);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Administration)]
        public IActionResult Add_InterviewSchemeToApplicant(ApplicantVM model)
        {
            _applicant = new Applicant();
            _applicant.AddInterviewSchemeToApplicant(model);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = UserRoles.Administration)]
        public IActionResult SortByPriority()
        {
            _applicant = new Applicant();
            ApplicantVM appvm;

            List<ApplicantVM> listapp = new List<ApplicantVM>();
            
            var list = _applicant.GetAllApplicantsWithoutSchemaOrInterviewer();
            foreach (var app in list)
            {
                appvm = new ApplicantVM();
                appvm = app;

                listapp.Add(appvm);
            }

            var ordered = listapp.OrderBy(x => x.Priority).ToList();

            return View("../Administration/Index", ordered);
        }

        [Authorize(Roles = UserRoles.Administration)]
        public IActionResult SortByEU()
        {
            ApplicantVM appvm;
            _applicant = new Applicant();
            List<ApplicantVM> listapp = new List<ApplicantVM>();
            
            var list = _applicant.GetAllApplicantsWithoutSchemaOrInterviewer();

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

        [Authorize(Roles = UserRoles.Administration)]
        public IActionResult SortByEUPriority()
        {
            _applicant = new Applicant();
            ApplicantVM appvm;

            List<ApplicantVM> listapp = new List<ApplicantVM>();

            var list = _applicant.GetAllApplicantsWithoutSchemaOrInterviewer();
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

        [Authorize(Roles = UserRoles.Administration)]
        public IActionResult SortByNonEU()
        {
            ApplicantVM appvm;
            _applicant = new Applicant();
            List<ApplicantVM> listapp = new List<ApplicantVM>();

            var list = _applicant.GetAllApplicantsWithoutSchemaOrInterviewer();

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

        [Authorize(Roles = UserRoles.Administration)]
        public IActionResult SortByNonEUPriority()
        {
            ApplicantVM appvm;
            _applicant = new Applicant();
            List<ApplicantVM> listapp = new List<ApplicantVM>();

            var list = _applicant.GetAllApplicantsWithoutSchemaOrInterviewer();

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
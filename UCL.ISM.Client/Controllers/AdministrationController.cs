using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UCL.ISM.Client.Infrastructure;
using System.Threading.Tasks;
using UCL.ISM.BLL.Interface;
using UCL.ISM.BLL;
using UCL.ISM.Client.Models;
using System.Collections.Generic;

namespace UCL.ISM.Client.Controllers
{
    [Authorize]
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
        [Authorize(Roles = UserRoles.Administration)]
        public IActionResult Create_StudyField()
        {
            IStudyField sf = new StudyField();
            StudyFieldVM model = new StudyFieldVM();
            StudyFieldVM sfvm;

            var allStudyFields = sf.GetAllStudyFields();
            foreach (var studyfield in allStudyFields)
            {
                sfvm = new StudyFieldVM();
                sfvm = studyfield;
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
            ViewData["Fields"] = _studyField.GetAllStudyFields();
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
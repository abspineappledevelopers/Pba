using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UCL.ISM.StudyField;

namespace UCL.ISM.Applicant
{
    [ModelBinder]
    public class Applicant
    {
        private Guid _id;
        private int _priority;
        private IStudyField _studyField;
        private string _firstName;
        private string _lastName;
        private int _age;
        private string _nationality;
        private bool _eu;
        private string _email;
        private string _interviewer;

        public Applicant()
        {

        }

        public int Priority { get => _priority; set => _priority = value; }
        public IStudyField StudyField { get => _studyField; set => _studyField = value; }
        public string FirstName { get => _firstName; set => _firstName = value; }
        public string LastName { get => _lastName; set => _lastName = value; }
        public int Age { get => _age; set => _age = value; }
        public string Nationality { get => _nationality; set => _nationality = value; }
        public bool EU { get => _eu; set => _eu = value; }
        public string Email { get => _email; set => _email = value; }
        public string Interviewer { get => _interviewer; set => _interviewer = value; }
        public Guid Id { get => _id; set => _id = value; }

        public async Task<Applicant> getApplicant()
        {
            Applicant currentApplicant;
            try
            {
                currentApplicant = await 
            }
            catch (Exception)
            {

                throw;
            }
            return currentApplicant;
        }
    }
}

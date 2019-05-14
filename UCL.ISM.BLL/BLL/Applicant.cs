using System;
using System.Collections.Generic;
using System.Text;
using UCL.ISM.BLL.DAL;
using UCL.ISM.BLL.Interface;

namespace UCL.ISM.BLL.BLL
{
    public class Applicant : IApplicant
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public Nationality Nationality { get; set; }
        public List<Nationality> Nationalities { get; set; }
        public bool IsEU { get; set; }
        //What does this do?
        public int Priority { get; set; }
        public string Comment { get; set; }
        public Interviewer Interviewer { get; set; }
        public List<Interviewer> Interviewers { get; set; }
        public StudyField StudyField { get; set; }
        public List<StudyField> StudyFields { get; set; }

        public Applicant()
        {
            Interviewer = new Interviewer();
            Interviewers = new List<Interviewer>();
            Nationality = new Nationality();
            Nationalities = new List<Nationality>();
        }

        private ApplicantDB db;

        public void CreateNewApplicant(Applicant applicant)
        {
            db = new ApplicantDB();
            db.CreateApplicant(applicant);
        }

        public List<Applicant> GetAllApplicantsWithoutSchema()
        {
            db = new ApplicantDB();

            return db.GetAllApplicantsWithoutSchema();
        }
    }
}

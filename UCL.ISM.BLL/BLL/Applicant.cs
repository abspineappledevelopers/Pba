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
        public bool HasResidencePermit { get; set; }
        //What does this do?
        public int Priority { get; set; }
        public string Comment { get; set; }
        public Interviewer Interviewer { get; set; }
        public List<Interviewer> Interviewers { get; set; }
        public StudyField StudyField { get; set; }
        public List<StudyField> StudyFields { get; set; }
        public InterviewScheme InterviewScheme { get; set; }
        public List<InterviewScheme> InterviewSchemes { get; set; }
        public ApplicantProcess Process { get; set; }

        public Applicant()
        {
            Interviewer = new Interviewer();
            Interviewers = new List<Interviewer>();
            Nationality = new Nationality();
            Nationalities = new List<Nationality>();
            InterviewScheme = new InterviewScheme();
            InterviewSchemes = new List<InterviewScheme>();
            Process = new ApplicantProcess();
        }

        private ApplicantDB db;

        public void CreateNewApplicant(Applicant applicant)
        {
            db = new ApplicantDB();
            db.CreateApplicant(applicant);
        }

        public Applicant GetApplicant(string id)
        {
            db = new ApplicantDB();

            return db.GetApplicant(id);
        }

        public Applicant EditApplicant(Applicant model)
        {
            db = new ApplicantDB();

            return db.EditApplicant(model);
        }

        public List<Applicant> GetAllApplicantsLimitedData()
        {
            db = new ApplicantDB();

            return db.GetAllApplicantsLimitedData();
        }

        public List<Applicant> GetAllApplicantsForInterviewer(string id)
        {
            db = new ApplicantDB();
            return db.GetAllApplicantsForInterviewer(id);
        }

        public List<Applicant> GetAllApplicantsWithoutSchemaOrInterviewer()
        {
            db = new ApplicantDB();

            return db.GetAllApplicantsWithoutSchemaOrInterviewer();
        }

        public void AddInterviewerToApplicant(Applicant model)
        {
            db = new ApplicantDB();
            db.AddInterviewerToApplicant(model);
        }

        public void AddInterviewSchemeToApplicant(Applicant model)
        {
            db = new ApplicantDB();
            db.AddInterviewSchemeToApplicant(model);
        }
    }
}

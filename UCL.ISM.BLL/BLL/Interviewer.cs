using System;
using System.Collections.Generic;
using System.Text;
using UCL.ISM.BLL.DAL;
using UCL.ISM.BLL.Interface;

namespace UCL.ISM.BLL.BLL
{
    public class Interviewer : IInterviewer
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        InterviewerDB db;

        public void CreateInterviewer(Interviewer interviewer)
        {
            db = new InterviewerDB();
            db.CreateInterviewer(interviewer);
        }

        public Interviewer GetInterviewer(string id)
        {
            db = new InterviewerDB();
            return db.GetInterviewer(id);
        }

        public List<Interviewer> GetAllInterviewers()
        {
            db = new InterviewerDB();

            return db.GetAllInterviewers();
        }
    }
}

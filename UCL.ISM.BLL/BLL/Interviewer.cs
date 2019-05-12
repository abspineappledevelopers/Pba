using System;
using System.Collections.Generic;
using System.Text;

namespace UCL.ISM.BLL.BLL
{
    public class Interviewer
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }

        public Interviewer GetInterviewer(Guid Id)
        {
            return null;
        }

        public List<Interviewer> GetAllInterviewers()
        {
            return null;
        }
    }
}

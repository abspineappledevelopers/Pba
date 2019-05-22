using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UCL.ISM.Client.Models
{
    public class LimitedApplicantData
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int ProcessId { get; set; }
        public string Process { get; set; }
        public string InterviewerName { get; set; }

        public LimitedApplicantData()
        {

        }
    }
}

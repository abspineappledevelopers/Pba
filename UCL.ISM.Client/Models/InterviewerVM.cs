using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UCL.ISM.BLL.BLL;

namespace UCL.ISM.Client.Models
{
    public class InterviewerVM
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public static implicit operator InterviewerVM(Interviewer interviewer)
        {
            return new InterviewerVM
            {
                Id = interviewer.Id,
                Firstname = interviewer.Firstname,
                Lastname = interviewer.Lastname
            };
        }

        public static implicit operator Interviewer(InterviewerVM vm)
        {
            return new Interviewer
            {
                Id = vm.Id,
                Firstname = vm.Firstname,
                Lastname = vm.Lastname
            };
        }
    }
}

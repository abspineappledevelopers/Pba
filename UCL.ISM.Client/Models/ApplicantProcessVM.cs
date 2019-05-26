using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UCL.ISM.BLL.BLL;

namespace UCL.ISM.Client.Models
{
    public class ApplicantProcessVM
    {
        public int Id { get; set; }
        public string Process { get; set; }

        public static implicit operator ApplicantProcessVM(ApplicantProcess process)
        {
            return new ApplicantProcessVM
            {
                Id = process.Id,
                Process = process.Process
            };
        }

        public static implicit operator ApplicantProcess(ApplicantProcessVM vm)
        {
            return new ApplicantProcess
            {
                Id = vm.Id,
                Process = vm.Process
            };
        }
    }
}

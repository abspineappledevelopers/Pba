using System;
using System.Collections.Generic;
using System.Text;
using UCL.ISM.BLL.BLL;

namespace UCL.ISM.BLL.Interface
{
    public interface IApplicant
    {
        void CreateNewApplicant(Applicant model);
        List<Applicant> GetAllApplicantsWithoutSchema();
        void AddInterviewerToApplicant(Applicant model);
    }
}

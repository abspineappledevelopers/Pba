using System;
using System.Collections.Generic;
using System.Text;

namespace UCL.ISM.BLL.Interface
{
    public interface IQuestion
    {
        Guid Id { get; set; }
        string Quest { get; set; }
        string Answer { get; set; }
        int InterviewSchemeId { get; set; }
    }
}

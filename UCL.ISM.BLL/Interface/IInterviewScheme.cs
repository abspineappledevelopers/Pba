using System;
using System.Collections.Generic;
using System.Text;

namespace UCL.ISM.BLL.Interface
{
    public interface IInterviewScheme
    {
        int Id { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime EditedDate { get; set; }
        string Comment { get; set; }

        List<IQuestion> GetQuestions();
    }
}

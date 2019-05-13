using System;
using System.Collections.Generic;
using System.Text;
using UCL.ISM.BLL.BLL;

namespace UCL.ISM.BLL.Interface
{
    public interface IInterviewScheme
    {
        int Id { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime EditedDate { get; set; }
        string Comment { get; set; }

        List<Question> GetQuestions(int id);
        int AddInterviewSchema(InterviewScheme interview);
        void AddQuestionToInterview(Question quest);
        
    }
}

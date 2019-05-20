using System;
using System.Collections.Generic;

namespace UCL.ISM.BLL.Interface
{
    public interface IInterviewScheme
    {
        int Id { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime EditedDate { get; set; }
        string Name { get; set; }
        string Comment { get; set; }
        List<UCL.ISM.BLL.BLL.Question> Questions { get; set; }
        List<int> CountryId { get; set; }

        //List<IQuestion> GetQuestions(int id);
        //int AddInterviewSchema(InterviewScheme interview);
        //void AddQuestionToInterview(IQuestion quest);
        //List<InterviewScheme> GetAllInterviewSchemes();
        
    }
}

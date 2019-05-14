﻿using System;
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
        string Name { get; set; }
        string Comment { get; set; }
        List<IQuestion> Questions { get; set; }
        List<int> CountryId { get; set; }

        List<IQuestion> GetQuestions(int id);
        int AddInterviewSchema(IInterviewScheme interview);
        void AddQuestionToInterview(IQuestion quest);
        
    }
}

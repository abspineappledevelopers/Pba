using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UCL.ISM.BLL.BLL;

namespace UCL.ISM.Client.Models
{
    public class QuestionVM
    {
        public Guid Id { get; set; }
        public string Quest { get; set; }
        public string Answer { get; set; }
        public int? InterviewSchemeId { get; set; }

        public static implicit operator QuestionVM(Question quest)
        {
            return new QuestionVM
            {
                Id = quest.Id,
                Quest = quest.Quest,
                Answer = quest.Answer,
                InterviewSchemeId = quest.InterviewSchemeId

            };
        }

        public static implicit operator Question(QuestionVM vm)
        {
            return new Question
            {
                Id = vm.Id,
                Quest = vm.Quest,
                Answer = vm.Answer,
                InterviewSchemeId = vm.InterviewSchemeId
            };
        }
    }
}

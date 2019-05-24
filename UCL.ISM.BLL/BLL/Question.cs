using System;
using System.Collections.Generic;
using System.Text;
using UCL.ISM.BLL.Interface;
using UCL.ISM.BLL.DAL;

namespace UCL.ISM.BLL.BLL
{
    public class Question : IQuestion
    {
        public Guid Id { get; set; }
        public int Order { get; set; }
        public string Quest { get; set; }
        public string Answer { get; set; }
        public int? InterviewSchemeId { get; set; }
    }
}

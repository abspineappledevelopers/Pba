using System;
using System.Collections.Generic;
using System.Text;
using UCL.ISM.BLL.Interface;
using UCL.ISM.BLL.DAL;

namespace UCL.ISM.BLL.BLL
{
    public class InterviewScheme : IInterviewScheme
    {
        private readonly List<IQuestion> _questions;
        private readonly InterviewSchemeDB _db;

        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EditedDate { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public List<Question> Questions { get; set; }
        public List<int> CountryId { get; set; }

        public InterviewScheme()
        {
            _db = new InterviewSchemeDB();
            Questions = new List<Question>();
            CountryId = new List<int>();
        }

        public int AddInterviewSchema(InterviewScheme interview)
        {
            return _db.CreateNewInterviewScheme(interview);
        }

        public void AddQuestionToInterview(Question question)
        {
            _db.AddQuestion(question);
        }

        public void RemoveQuestion(IQuestion question)
        {
            _db.RemoveQuestion(question);
        }

        public List<Question> GetQuestions(int id)
        {
            List<Question> temp = _db.GetAllSchemeQuestions(id);
            return temp;
        }
    }
}

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
        public string Comment { get; set; }
        public List<Question> Questions { get; set; }
        public List<Nationality> SchemeForCountries { get; set; }

        public InterviewScheme()
        {
            _db = new InterviewSchemeDB();
            Questions = new List<Question>();
            SchemeForCountries = new List<Nationality>();
        }

        public void AddInterviewSchema(InterviewScheme interview)
        {
            _db.CreateNewInterviewScheme(interview);


        }

        public void AddQuestion(Question question)
        {
            _db.AddQuestion(question);
        }

        public void RemoveQuestion(IQuestion question)
        {
            _db.RemoveQuestion(question);
        }

        public List<IQuestion> GetQuestions()
        {
            List<IQuestion> temp = _db.GetAllSchemeQuestions(Id);
            return temp;
        }
    }
}

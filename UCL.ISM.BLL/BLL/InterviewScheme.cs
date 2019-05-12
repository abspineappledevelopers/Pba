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

        public InterviewScheme()
        {
            _db = new InterviewSchemeDB();
        }

        internal List<IQuestion> Questions => _questions;

        public void AddQuestion(IQuestion question)
        {
            _db.AddQuestion(question.Id, question.Content);
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

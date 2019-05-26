using System;
using System.Collections.Generic;
using System.Text;
using UCL.ISM.BLL.Interface;
using UCL.ISM.BLL.DAL;

namespace UCL.ISM.BLL.BLL
{
    public class InterviewScheme : IInterviewScheme
    {
        private readonly InterviewSchemeDB _db;

        public int? Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EditedDate { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public List<Question> Questions { get; set; }
        public List<int> CountryId { get; set; }
        public List<int> StudyFieldId { get; set; }

        public InterviewScheme()
        {
            _db = new InterviewSchemeDB();
            Questions = new List<Question>();
            CountryId = new List<int>();
            StudyFieldId = new List<int>();
        }

        public int AddInterviewSchema(InterviewScheme interview)
        {
            return _db.CreateNewInterviewScheme(interview);
        }

        public void UpdateInterviewScheme(InterviewScheme interview)
        {
            _db.UpdateInterviewScheme(interview);
        }

        public void DeleteInterviewScheme(int? id)
        {
            _db.DeleteInterviewScheme(id);
        }

        public void AddQuestionToInterview(Question question)
        {
            question.Id = Guid.NewGuid();

            _db.AddQuestion(question);
        }

        public void UpdateQuestion(Question question)
        {
            if (!string.IsNullOrEmpty(question.Quest))
            {
                _db.UpdateQuestion(question);
            }
        }

        public void RemoveQuestion(Guid id)
        {
            _db.RemoveQuestion(id);
        }

        public List<Question> GetQuestions(int? id)
        {
            List<Question> temp = _db.GetAllSchemeQuestions(id);

            return temp;
        }

        public List<InterviewScheme> GetAllInterviewSchemes()
        {
            return _db.GetAllInterviewSchemes();
        }

        public List<InterviewScheme> GetSpecificInterviewSchemes(string id)
        {
            return _db.GetSpecificInterviewSchemes(id);
        }

        public InterviewScheme GetInterviewSchemeAndQuestions(int? id)
        {
            return _db.GetInterviewSchemeAndQuestions(id);
        }
    }
}

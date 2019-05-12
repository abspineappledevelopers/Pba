using System;
using System.Collections.Generic;
using System.Text;

namespace UCL.ISM.InterviewScheme
{
    public class InterviewScheme : IInterviewScheme
    {
        private readonly List<IQuestions> _questions;

        public InterviewScheme()
        {
            _questions = new List<IQuestions>();
        }

        internal List<IQuestions> Questions => _questions;

        public void AddQuestion(IQuestions question)
        {
            Questions.Add(question);
        }

        public void RemoveQuestion(IQuestions question)
        {
            if (Questions.Contains(question))
            {
                try
                {
                    Questions.Remove(question);
                }
                catch (Exception)
                {

                    throw;
                }
                
            }
        }

        public List<IQuestions> GetQuestions()
        {
            List<IQuestions> temp = Questions;
            return temp;
        }
    }
}

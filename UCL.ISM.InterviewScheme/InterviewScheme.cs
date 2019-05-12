using System;
using System.Collections.Generic;
using System.Text;

namespace UCL.ISM.InterviewScheme
{
    public class InterviewScheme
    {
        private readonly List<IQuestions> _questions;

        public InterviewScheme()
        {

        }

        internal List<IQuestions> Questions => _questions;
    }
}

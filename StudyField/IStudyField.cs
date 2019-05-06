using System;
using System.Collections.Generic;
using System.Text;

namespace UCL.ISM.StudyField
{
    public interface IStudyField
    {
        void CreateNewStudyField(string fieldName);
        StudyField GetStudyField(int Id);
        List<StudyField> GetAllStudyFields();
        StudyField EditStudyField(StudyField studyField);
        void DeleteStudyField(int Id);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace UCL.ISM.BLL.Interface
{
    public interface IStudyField
    {
        int Id { get; set; }
        string FieldName { get; set; }
        DateTime Created { get; set; }
        DateTime Edited { get; set; }

        void CreateNewStudyField(string fieldName);
        StudyField GetStudyField(int Id);
        List<StudyField> GetAllStudyFields();
        void EditStudyField(StudyField studyField);
        void DeleteStudyField(int Id);
    }
}

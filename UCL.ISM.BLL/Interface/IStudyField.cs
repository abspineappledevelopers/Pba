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
        IStudyField GetStudyField(int Id);
        List<IStudyField> GetAllStudyFields();
        void EditStudyField(IStudyField studyField);
        void DeleteStudyField(int Id);
    }
}

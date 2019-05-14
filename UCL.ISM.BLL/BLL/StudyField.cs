using System;
using System.Collections.Generic;
using System.Text;
using UCL.ISM.BLL.Interface;

namespace UCL.ISM.BLL
{
    public class StudyField : IStudyField
    {
        public int Id { get; set; }
        public string FieldName { get; set; }
        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }

        private StudyFieldDB _db;

        public void CreateNewStudyField(string fieldName)
        {
            if (!string.IsNullOrWhiteSpace(fieldName))
            {
                _db = new StudyFieldDB();

                _db.CreateNewStudyField(fieldName);
            }
            else
            {
                throw new ArgumentException("Study name was not in correct format.");
            }
        }

        public IStudyField GetStudyField(int Id)
        {
            _db = new StudyFieldDB();

            return _db.GetStudyField(Id);
        }

        public List<IStudyField> GetAllStudyFields()
        {
            _db = new StudyFieldDB();

            return _db.GetAllStudyFields();
        }

        public void EditStudyField(IStudyField studyField)
        {
            _db = new StudyFieldDB();

            _db.EditStudyField(studyField);
        }

        public void DeleteStudyField(int Id)
        {
            _db = new StudyFieldDB();

            try
            {
                _db.DeleteStudyField(Id);
            }
            catch
            {
                throw new ArgumentException("Could not delete study field.");
            }
        }
    }
}

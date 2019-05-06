using System;
using System.Collections.Generic;
using System.Text;

namespace UCL.ISM.StudyField
{
    public class StudyField : IStudyField
    {
        public int Id { get; set; }
        public string FieldName { get; set; }



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

        public StudyField GetStudyField(int Id)
        {
            _db = new StudyFieldDB();

            return _db.GetStudyField(Id);
        }

        public List<StudyField> GetAllStudyFields()
        {
            _db = new StudyFieldDB();

            return _db.GetAllStudyFields();
        }

        public StudyField EditStudyField(StudyField studyField)
        {
            _db = new StudyFieldDB();

            return _db.EditStudyField(studyField);
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

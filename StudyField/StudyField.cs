﻿using System;
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
    }
}

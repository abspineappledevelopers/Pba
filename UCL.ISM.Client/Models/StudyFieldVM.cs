using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UCL.ISM.BLL;

namespace UCL.ISM.Client.Models
{
    public class StudyFieldVM
    {
        public int Id { get; set; }
        public string FieldName { get; set; }
        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }
        public List<StudyFieldVM> AllStudyFields { get; set; }

        public StudyFieldVM()
        {
            AllStudyFields = new List<StudyFieldVM>();
        }

        public static implicit operator StudyFieldVM(StudyField studyfield)
        {
            return new StudyFieldVM
            {
                Id = studyfield.Id,
                FieldName = studyfield.FieldName,
                Created = studyfield.Created,
                Edited = studyfield.Edited
            };
        }

        public static implicit operator StudyField(StudyFieldVM vm)
        {
            return new StudyField
            {
                Id = vm.Id,
                FieldName = vm.FieldName,
                Created = vm.Created,
                Edited = vm.Edited
            };
        }
    }
}

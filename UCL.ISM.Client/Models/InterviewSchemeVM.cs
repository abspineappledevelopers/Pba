using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UCL.ISM.BLL.BLL;
using UCL.ISM.BLL.Interface;

namespace UCL.ISM.Client.Models
{
    public class InterviewSchemeVM
    {
        public int? Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EditedDate { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public Question Question { get; set; }
        public List<Question> Questions { get; set; }
        public List<int> CountryId { get; set; }
        public List<SelectListItem> Countries { get; set; }

        public InterviewSchemeVM()
        {
            Countries = new List<SelectListItem>();
            Questions = new List<Question>();
            CountryId = new List<int>();
            Question = new Question();
        }

        public static implicit operator InterviewSchemeVM(InterviewScheme interview)
        {
            return new InterviewSchemeVM
            {
                Id = interview.Id,
                CreatedDate = interview.CreatedDate,
                EditedDate = interview.EditedDate,
                Name = interview.Name,
                Comment = interview.Comment,
                CountryId = interview.CountryId
            };
        }

        public static implicit operator InterviewScheme(InterviewSchemeVM vm)
        {
            return new InterviewScheme
            {
                Id = vm.Id,
                CreatedDate = vm.CreatedDate,
                EditedDate = vm.EditedDate,
                Name = vm.Name,
                Comment = vm.Comment,
                CountryId = vm.CountryId
            };
        }
    }
}

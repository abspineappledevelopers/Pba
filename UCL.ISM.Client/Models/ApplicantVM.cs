﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UCL.ISM.BLL.BLL;

namespace UCL.ISM.Client.Models
{
    public class ApplicantVM
    {
        public Guid Id { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int Age { get; set; }
        public NationalityVM Nationality { get; set; }
        public List<SelectListItem> Nationalities { get; set; }
        public bool IsEU { get; set; }
        public bool HasResidencePermit { get; set; }
        //What does this do?
        [Required]
        public int Priority { get; set; }
        public string Comment { get; set; }
        public InterviewerVM Interviewer { get; set; }
        public List<SelectListItem> Interviewers { get; set; }
        public StudyFieldVM StudyField { get; set; }
        public List<SelectListItem> StudyFields { get; set; }
        public InterviewSchemeVM InterviewScheme { get; set; }
        public List<SelectListItem> InterviewSchemes { get; set; }
        public ApplicantProcessVM Process { get; set; }

        public ApplicantVM()
        {
            Interviewer = new InterviewerVM();
            Interviewers = new List<SelectListItem>();
            StudyField = new StudyFieldVM();
            StudyFields = new List<SelectListItem>();
            Nationality = new NationalityVM();
            Nationalities = new List<SelectListItem>();
            InterviewScheme = new InterviewSchemeVM();
            InterviewSchemes = new List<SelectListItem>();
            Process = new ApplicantProcessVM();
        }

        public static implicit operator ApplicantVM(Applicant applicant)
        {
            return new ApplicantVM
            {
                Id = applicant.Id,
                Firstname = applicant.Firstname,
                Lastname = applicant.Lastname,
                Email = applicant.Email,
                Age = applicant.Age,
                Nationality = applicant.Nationality,
                IsEU = applicant.IsEU,
                HasResidencePermit = applicant.HasResidencePermit,
                Priority = applicant.Priority,
                Interviewer = applicant.Interviewer, 
                StudyField = applicant.StudyField,
                Comment = applicant.Comment,
                InterviewScheme = applicant.InterviewScheme,
                Process = applicant.Process
            };
        }

        public static implicit operator Applicant(ApplicantVM vm)
        {
            return new Applicant
            {
                Id = vm.Id,
                Firstname = vm.Firstname,
                Lastname = vm.Lastname,
                Email = vm.Email,
                Age = vm.Age,
                Nationality = vm.Nationality,
                IsEU = vm.IsEU,
                HasResidencePermit = vm.HasResidencePermit,
                Priority = vm.Priority,
                Interviewer = vm.Interviewer,
                StudyField = vm.StudyField,
                Comment = vm.Comment,
                InterviewScheme = vm.InterviewScheme,
                Process = vm.Process
            };
        }
    }
}

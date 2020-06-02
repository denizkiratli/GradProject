using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GradProj.Models
{
    public class ResultModel
    {
        [Display(Name = "Submit Id")]
        public int ResultId { get; set; }

        [Display(Name = "Student Id")]
        public int InstitutionId { get; set; }

        [Display(Name = "Assignment Name")]
        public string AssignmentName { get; set; }

        [Display(Name = "Submit Score")]
        public double Score { get; set; }

        [Display(Name = "Number of Attendance on Assignment")]
        public int NumberofAttendance { get; set; }
        public int AssignmentId { get; set; }
    }
}
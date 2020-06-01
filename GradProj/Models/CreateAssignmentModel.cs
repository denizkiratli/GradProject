﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GradProj.Models
{
    public class CreateAssignmentModel
    {
        [Display(Name ="Assignment Id")]
        public int AssignmentId { get; set; }

        [Display(Name = "Assignment Name")]
        public string AssignmentName { get; set; }

        [Display(Name = "Assignment Information")]
        public string AssignmentInfo { get; set; }

        [Display(Name = "Assignment Date")]
        public DateTime AssignmentDate { get; set; }
    }
}
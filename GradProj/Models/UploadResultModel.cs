using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GradProj.Models
{
    public class UploadResultModel
    {
        [Display(Name = "Number of Classified Data")]
        public int NumofClassData { get; set; }

        [Display(Name = "Number of Classified Method")]
        public int NumofClassMethod { get; set; }

        [Display(Name = "Number of Critical Class")]
        public int NumofCritClass { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GradProj.Models
{
    public class UploadResultModel
    {
        [Display(Name = "Read & Write Security Metric Score")]
        public double RWSMS { get; set; }

        [Display(Name = "Security Design Principle Metric Score")]
        public double SDPMS { get; set; }

        [Display(Name = "OOP Metrics Score")]
        public int OOPMG { get; set; }

        [Display(Name = "Tecnique Usage Score")]
        public int TUG { get; set; }

        [Display(Name = "Testabilty Score")]
        public double TG { get; set; }

        [Display(Name = "Final Score")]
        public double finalScore { get; set; }
    }
}
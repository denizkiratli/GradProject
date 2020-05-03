using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GradProj.Models
{
    public class UserModel
    {
        [Display(Name = "Institution ID")]
        public int UserId { get; set; }

        [Display(Name = "Name")]
        public string UserName { get; set; }

        [Display(Name = "Email Address")]
        public string MailAddress { get; set; }
    }
}
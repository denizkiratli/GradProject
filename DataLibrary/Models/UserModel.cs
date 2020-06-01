using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class UserModel
    {
        public string UserId { get; set; }
        public int InstitutionId { get; set; }
        public string UserFullName { get; set; }
        public string Email { get; set; }
        public string RoleId { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class ResultModel
    {
        public string UserId { get; set; }
        public int ResId { get; set; }
        public int InstitutionId { get; set; }
        public int AssignmentId { get; set; }
        public string AsName { get; set; }
        public double Score { get; set; }
        public int TotAsNum { get; set; }
    }
}

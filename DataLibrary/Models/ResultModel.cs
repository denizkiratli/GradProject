using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class ResultModel
    {
        public int ResultId { get; set; }
        public string AssignmentName { get; set; }
        public double Score { get; set; }
        public int TotalAssignmentNumber { get; set; }
    }
}

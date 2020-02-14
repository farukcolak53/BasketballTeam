using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSE3055.Models
{
    class Staff
    {
        public int StaffID { get; set; }
        public int TeamID { get; set; }
        public string StaffName { get; set; }
        public int StaffBirthDate { get; set; }
        public int Salary { get; set; }
        public int ContractPeriod { get; set; }
    }
}

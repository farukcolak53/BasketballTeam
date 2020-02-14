using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSE3055.Models
{
    class Player
    {
        public int PlayerID { get; set; }
        public int TeamID { get; set; }
        public string PlayerName { get; set; }
        public string Position { get; set; }
        public string PlayerBirthDate { get; set; }
        public int Minutes { get; set; }
        public int Points { get; set; }
        public int Rebounds { get; set; }
        public int Assists { get; set; }
        public int Salary { get; set; }
        public int ContractPeriod { get; set; }
        public int Height { get; set; }
    }
}

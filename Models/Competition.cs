using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSE3055.Models
{
    class Competition
    {
        public int CompetitionID { get; set; }
        public string CompetitionName { get; set; }
        public int Prize { get; set; }
        public int NumberOfParticipants { get; set; }
        public string LastChampion { get; set; }
    }
}

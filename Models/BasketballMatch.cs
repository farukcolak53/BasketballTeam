using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSE3055.Models
{
    class BasketballMatch
    {
        public int MatchID { get; set; }
        public string MatchDate { get; set; }
        public TimeSpan MatchTime { get; set; }
        public string AwayTeamName { get; set; }
        public string HomeTeamName { get; set; }
        public string MatchScore { get; set; }
    }
}

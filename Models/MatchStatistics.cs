using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSE3055.Models
{
    class MatchStatistics
    {
        public int MatchID { get; set; }
        public int MatchStatisticID { get; set; }
        public string AwayTeamName { get; set; }
        public int AwayTeamPoints { get; set; }
        public int AwayTeamRebounds { get; set; }
        public int AwayTeamAssists { get; set; }
        public int AwayTeamTurnovers { get; set; }
        public string AwayTeamFG { get; set; }
        public string AwayTeam3P { get; set; }
        public string AwayTeam2P { get; set; }
        public string AwayTeamFP { get; set; }
        public string HomeTeamName { get; set; }
        public int HomeTeamPoints { get; set; }
        public int HomeTeamRebounds { get; set; }
        public int HomeTeamAssists { get; set; }
        public int HomeTeamTurnovers { get; set; }
        public string HomeTeamFG { get; set; }
        public string HomeTeam3P { get; set; }
        public string HomeTeam2P { get; set; }
        public string HomeTeamFP { get; set; }
    }
}

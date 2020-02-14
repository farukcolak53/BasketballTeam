using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSE3055.Models
{
    class TeamSponsorship
    {
        public int TeamID{ get; set; }
        public int SponsorID { get; set; }
        public string TeamName{ get; set; }
        public string SponsorName { get; set; }
        public int SponsorshipContractPeriod { get; set; }
        public int SponsorshipPayment { get; set; }
    }
}

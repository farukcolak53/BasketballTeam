using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSE3055.Models
{
    class PlayerSponsorship
    {
        public int PlayerID { get; set; }
        public int SponsorID { get; set; }
        public string PlayerName { get; set; }
        public string SponsorName { get; set; }
        public int SponsorshipContractPeriod { get; set; }
        public int SponsorshipPayment { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSE3055.Models
{
    public class Team
    {
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public string TeamMascot { get; set; }
        public string FirstColor { get; set; }
        public string SecondColor { get; set; }
        public int CityID { get; set; }
    }
}
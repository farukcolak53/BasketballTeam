using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSE3055.Models
{
    public class Gym
    {
        public int GymID { get; set; }
        public int CityID { get; set; }
        public string GymName { get; set; }
        public string GymStreet { get; set; }
        public string GymDistrict { get; set; }
        public string ConstructionDate { get; set; }
        public int Capacity { get; set; }
    }
}
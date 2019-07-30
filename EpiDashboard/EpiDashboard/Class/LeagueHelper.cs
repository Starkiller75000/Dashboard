using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EpiDashboard.Class
{
    public class LeagueHelper
    {
        public string leagueId { get; set; }
        public string leagueName { get; set; }
        public string tier { get; set; }
        public string queueType { get; set; }
        public string rank { get; set; }
        public string playerOrTeamId { get; set; }
        public string playerOrTeamName { get; set; }
        public int leaguePoints { get; set; }
        public float wins { get; set; }
        public float losses { get; set; }
        public bool veteran { get; set; }
        public bool inactive { get; set; }
        public bool freshBlood { get; set; }
        public bool hotStreak { get; set; }
    }
}

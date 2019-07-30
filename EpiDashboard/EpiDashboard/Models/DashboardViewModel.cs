using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EpiDashboard.Models
{
    public class DashboardViewModel
    {
        //OpenWeatherMap variables
        public string apiResponse { get; set; }
        public string cities { get; set; }

        //GoogleTranslator variables
        public string message { get; set; }
        public string source { get; set; }
        public string target { get; set; }
        public string translated { get; set; }

        //Twitch variables
        public string twitchResponse { get; set; }
        public string channel { get; set; }

        //Movie variable
        public string movieResponce { get; set; }
        public string movieName { get; set; }
        public string MovieResult { get; set; }

        //LOL variable
        public string SummonerName { get; set; }
        public string Region { get; set; }
        public string LolResponse { get; set; }

        //NSFW variables
        public string NumberResult { get; set; }
        public string tags { get; set; }
        public string search { get; set; }
        public string NSFWRep { get; set; }
    }
}

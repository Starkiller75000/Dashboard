using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EpiDashboard.Class
{
    public class GoogleHelper
    {
        public class Translation
        {
            public string translatedText { get; set; }
        }

        public class Data
        {
            public List<Translation> translations { get; set; }
        }

        public class RootObject
        {
            public Data data { get; set; }
        }
    }
}

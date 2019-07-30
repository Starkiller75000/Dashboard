using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EpiDashboard.Class
{
    public class JsonAbout
    {
        public Client client;
        public Server server;
    }
    public class Client
    {
        public string host;
    }

    public class Param
    {
        public string name;
        public string type;
    }

    public class Widget
    {
        public string name;
        public string description;
        public List<Param> parameters;
    }

    public class Service
    {
        public string name;
        public List<Widget> widgets;
    }

    public class Server
    {
        public string current_time;
        public List<Service> services;
    }
}

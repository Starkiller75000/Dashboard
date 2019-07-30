using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EpiDashboard.Models;
using EpiDashboard.Class;
using System.Net;
using System.Net.Sockets;

namespace EpiDashboard.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        [HttpGet("about.json")]
        public JsonResult Abbout()
        {
            JsonAbout jsonAbout = new JsonAbout();
            jsonAbout.client = new Client();
            jsonAbout.server = new Server();

            //Weather service
            Service service = new Service();
            //Google service
            Service service1 = new Service();
            //Twitch service
            Service service2 = new Service();
            //Movie Db service
            Service service3 = new Service();
            //Riot games service
            Service service4 = new Service();
            //Redtube service
            Service service5 = new Service();

            //Weather widget
            Widget widget = new Widget();
            //Google widget
            Widget widget1 = new Widget();
            //Twitch widget
            Widget widget2 = new Widget();
            //Movie Db widget
            Widget widget3 = new Widget();
            //Riot games widget
            Widget widget4 = new Widget();
            //Redtube widget
            Widget widget5 = new Widget();

            //Weather param
            Param param = new Param();
            //Google traduction param
            Param param1 = new Param();
            Param param2 = new Param();
            //Twitch param
            Param param3 = new Param();
            //Movie Db param
            Param param4 = new Param();
            Param param5 = new Param();
            //Riot games param
            Param param6 = new Param();
            Param param7 = new Param();
            //Redtube param
            Param param8 = new Param();
            Param param9 = new Param();
            Param param10 = new Param();

            //Weather param list
            widget.parameters = new List<Param>();
            //Goolge param list
            widget1.parameters = new List<Param>();
            //Twitch param list
            widget2.parameters = new List<Param>();
            //MovieDb param list
            widget3.parameters = new List<Param>();
            //Riot Games param list
            widget4.parameters = new List<Param>();
            //Redtube param list
            widget5.parameters = new List<Param>();

            //Weather widget list
            service.widgets = new List<Widget>();
            //Google widget list
            service1.widgets = new List<Widget>();
            //Twitch widget list
            service2.widgets = new List<Widget>();
            //MovieDb widget list
            service3.widgets = new List<Widget>();
            //Riot Games widget list
            service4.widgets = new List<Widget>();
            //Redtube widget list
            service5.widgets = new List<Widget>();

            jsonAbout.server.services = new List<Service>();

            jsonAbout.client.host = GetLocalIPAddress();
            jsonAbout.server.current_time = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();

            service.name = "OpenWeathermap";
            service1.name = "Google";
            service2.name = "Twitch";
            service3.name = "MovieDb";
            service4.name = "Riot Games";
            service5.name = "Redtube";

            widget.name = "weather";
            widget1.name = "GoogleTraduction";
            widget2.name = "Live twitch";
            widget3.name = "Movie";
            widget4.name = "Summoner's stats";
            widget5.name = "Redtube video";
            widget.description = "Affiche la météo courante pour une ville renseigné";
            widget1.description = "Traduit une phrase";
            widget2.description = "Affiche les infos d'une chaîne twitch en live";
            widget3.description = "Affiche les infos d'un film renseigné";
            widget4.description = "Affiche les stats d'un joueur de League of Legends";
            widget5.description = "Affiche les informations d'une vidéo pornographique renseigné";

            //Weather
            param.name = "city";
            //Google traduction
            param1.name = "source phrase";
            param2.name = "target phrase";
            //Twitch
            param3.name = "twitch channel";
            //Movie Db
            param4.name = "number of results";
            param5.name = "movie's name";
            //Riot
            param6.name = "region's name";
            param7.name = "summoner's name";
            //Redtube
            param8.name = "number of results";
            param9.name = "tags";
            param10.name = "video's name";
            //Weather
            param.type = "string";
            //Google traduction
            param1.type = "string";
            param2.type = "string";
            //Twitch
            param3.type = "string";
            //Movie Db
            param4.type = "integer";
            param5.type = "string";
            //Riot games
            param6.type = "string";
            param7.type = "string";
            //Redtube
            param8.type = "integer";
            param9.type = "string";
            param10.type = "string";

            widget.parameters.Add(param);
            widget1.parameters.Add(param1);
            widget1.parameters.Add(param2);
            widget2.parameters.Add(param3);
            widget3.parameters.Add(param4);
            widget3.parameters.Add(param5);
            widget4.parameters.Add(param6);
            widget4.parameters.Add(param7);
            widget5.parameters.Add(param8);
            widget5.parameters.Add(param9);
            widget5.parameters.Add(param10);

            service.widgets.Add(widget);
            service1.widgets.Add(widget1);
            service2.widgets.Add(widget2);
            service3.widgets.Add(widget3);
            service4.widgets.Add(widget4);
            service5.widgets.Add(widget5);

            jsonAbout.server.services.Add(service);
            jsonAbout.server.services.Add(service1);
            jsonAbout.server.services.Add(service2);
            jsonAbout.server.services.Add(service3);
            jsonAbout.server.services.Add(service4);
            jsonAbout.server.services.Add(service5);
            return Json(new { client = new { jsonAbout.client.host }, server = new { jsonAbout.server.current_time, jsonAbout.server.services }});
        }

    }
}

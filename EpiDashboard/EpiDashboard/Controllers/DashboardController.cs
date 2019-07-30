using System;
using System.Linq;
using System.Threading.Tasks;
using EpiDashboard.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EpiDashboard.Class;
using System.Net;
using System.IO;
using static EpiDashboard.Class.Helper;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;
using static EpiDashboard.Class.GoogleHelper;
using static EpiDashboard.Class.TwitchHelper;
using static EpiDashboard.Class.MovieHelper;

namespace EpiDashboard.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            DashboardViewModel openWeatherMap = new DashboardViewModel();
            return View(openWeatherMap);
        }

        [HttpPost]
        public ActionResult Index(DashboardViewModel openWeatherMap, string cities)
        {
            if (cities != null)
            {
                /*Calling API http://openweathermap.org/api */
                string apiKey = "9b102e84793bf9c05da530b6612257ed";
                HttpWebRequest apiRequest = WebRequest.Create("http://api.openweathermap.org/data/2.5/weather?q=" + cities + "&appid=" + apiKey + "&units=metric") as HttpWebRequest;

                string apiResponse = "";
                using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    apiResponse = reader.ReadToEnd();
                }
                /*End*/

                /*http://json2csharp.com*/
                ResponseWeather rootObject = JsonConvert.DeserializeObject<ResponseWeather>(apiResponse);

                StringBuilder sb = new StringBuilder();
                sb.Append("<div weather-panel=\"forecast\" show-entry=\"forecast.list[0]\" class=\"ng-isolate-scope ng-scope\" id=\"weather\"><div class=\"weather panel panel-primary\">");
                sb.Append("<div class=\"panel-heading ng-binding\" id=\"weatherheader\">" + rootObject.name + ", " + rootObject.sys.country + ": " + DateTime.Now + "</div>");
                sb.Append("<div class=\"panel-body\">");
                sb.Append("<div>");
                sb.Append("<p class=\"lead ng-binding\"><img ng-src=http://openweathermap.org/img/w/" + rootObject.weather[0].icon + ".png " + "src=\"http://openweathermap.org/img/w/" + rootObject.weather[0].icon + ".png\"> " + rootObject.main.temp + "°C " + rootObject.weather[0].main + "</p>");
                sb.Append("<p class=\"ng-binding\">" + rootObject.weather[0].description + "&nbsp; ~&nbsp;High: " + rootObject.main.temp_max + "°C &nbsp; ~&nbsp;Low: " + rootObject.main.temp_min + "°C</p>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("<div class=\"panel-footer\"><small class=\"ng-binding\">Pressure: " + rootObject.main.pressure + "hPa&nbsp;~&nbsp;Humidity: " + rootObject.main.humidity + "%</small></div>");
                sb.Append("</div>");
                sb.Append("</div>");
                openWeatherMap.apiResponse = sb.ToString();
            }
            else
            {
                ViewBag.ErrorMessage = "please provide a city name";
            }
            return View(openWeatherMap);
        }

        [HttpPost]
        public ActionResult Google(DashboardViewModel googletranslator, string message, string source, string target)
        {
            if (message == null)
            {
                ViewBag.Error = "please enter type";
            }
            if (source != null && target != null)
            {
                /*Calling API http://google.apis.com */
                string apiKey = "AIzaSyCfhXPvfixR2EKfI_Eb69xH0_I38b5CYBg";
                HttpWebRequest apiRequest = WebRequest.Create("https://translation.googleapis.com/language/translate/v2?key=" + apiKey + "&q=" + message + "&source=" + source + "&target=" + target) as HttpWebRequest;

                string apiResponse = "";
                using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    apiResponse = reader.ReadToEnd();
                }
                /*End*/

                /*http://json2csharp.com*/
                RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(apiResponse);

                StringBuilder sb = new StringBuilder();
                sb.Append("<table><tr><th>Traduction</th></tr>");
                sb.Append("<tr><td></td><td>" + rootObject.data.translations[0].translatedText + "</td></tr>");
                sb.Append("</table>");
                googletranslator.translated = sb.ToString();
            }
            else
            {
                ViewBag.Error = "please enter type of language";
            }
            return View("Index", googletranslator);
        }

        [HttpGet]
        public ActionResult Twitch(DashboardViewModel twitch, string channel)
        {
            if (channel != null)
            {
                /*Calling API http://openweathermap.org/api */
                string apiKey = "n8d50u0n4ah3rop28l0mzdn61t373i";
                HttpWebRequest apiRequest = WebRequest.Create("https://api.twitch.tv/kraken/streams/" + channel + "?client_id=" + apiKey) as HttpWebRequest;

                string apiResponse = "";
                using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    apiResponse = reader.ReadToEnd();
                }
                /*End*/

                /*http://json2csharp.com*/
                Twitch rootObject = JsonConvert.DeserializeObject<Twitch>(apiResponse);
                if (rootObject.Stream == null)
                {
                    ViewBag.LiveOff = channel + " is not in live.";
                    return View("Index", twitch);
                }

                StringBuilder sb = new StringBuilder();
                string game_url = "\"https://www.twitch.tv/directory/game/" + rootObject.Stream.Game + "\"";
                sb.Append("<div id=\"twitch-div\">");
                sb.Append("<div id=\"twitchheader\">");
                sb.Append("<img src=\"" + rootObject.Stream.Preview.Medium + "\"></br>");
                sb.Append("</div>");
                sb.Append("<img id=\"twitch-logo\" src=\"" + rootObject.Stream.Channel.Logo +"\">");
                sb.Append("<a href=\"" + rootObject.Stream.Channel.Url + "\">" + rootObject.Stream.Channel.DisplayName + "</a>");
                sb.Append("<p>" + rootObject.Stream.Channel.Status + "</p>");
                sb.Append("<p> Joue à <a href=" + game_url + ">" + rootObject.Stream.Game + " </a> pour " + rootObject.Stream.Viewers + " spectateurs.</p>");
                sb.Append("<p>" + rootObject.Stream.Channel.Language + "</p>");
                sb.Append("</div>");
                twitch.twitchResponse = sb.ToString();
            }
            else
            {
                ViewBag.ErrorMessage = "please provide a channel name";
            }
            return View("Index", twitch);
        }

        [HttpGet]
        public ActionResult Movie(DashboardViewModel movie, string movieName, string MovieResult)
        {
            int result = 0;

            if (MovieResult == null)
            {
                result = 1;
            }
            else
            {
                result = Int32.Parse(MovieResult);
            }
            if (movieName != null)
            {
                /*Calling API http://openweathermap.org/api */
                string apiKey = "32d5e7e0e778766d37d410453c16fee8";
                HttpWebRequest apiRequestForIdMovie = WebRequest.Create("https://api.themoviedb.org/3/search/movie?api_key=" + apiKey + "&query=" + movieName) as HttpWebRequest;

                string apiResponse = "";
                using (HttpWebResponse response = apiRequestForIdMovie.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    apiResponse = reader.ReadToEnd();
                }
                /*End*/
                /*http://json2csharp.com*/
                RootMovie rootObject = JsonConvert.DeserializeObject<RootMovie>(apiResponse);
                if (rootObject.results == null)
                {
                    ViewBag.LiveOff = movieName + " is not in our database.";
                    return View("Index", movie);
                }

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < result; i++)
                {
                    sb.Append("<div id=\"movie-div\">");
                    sb.Append("<p>Title: " + rootObject.results[i].title + "</p>");
                    sb.Append("<p>Original language: " + rootObject.results[i].original_language + "</p>");
                    sb.Append("<p>Overview: " + rootObject.results[i].overview + "</p>");
                    sb.Append("<p>Vote average: " + rootObject.results[i].vote_average + "</p>");
                    sb.Append("<p>Number of vote: " + rootObject.results[i].vote_count + "</p>");
                    sb.Append("<p>Date release: " + rootObject.results[i].release_date + "</p>");
                    sb.Append("</div>");
                }
                movie.movieResponce = sb.ToString();

            }
            else
            {
                ViewBag.ErrorMessage = "please provide a movie name.";
            }
            return View("Index", movie);
        }

        [HttpGet]
        public ActionResult LOL(DashboardViewModel lol, string SummonerName, string Region)
        {
            if (Region == null)
            {
                ViewBag.ErrorMessage = "please provide a Region";
            }
            if (SummonerName != null)
            {
                string apiKey = "RGAPI-956acb55-3849-4a12-af72-9c24d7542f7b";
                HttpWebRequest apiRequestForIdSummoner = WebRequest.Create("https://" + Region + "1.api.riotgames.com/lol/summoner/v3/summoners/by-name/" + SummonerName + "?api_key=" +  apiKey) as HttpWebRequest;

                string apiResponse = "";
                using (HttpWebResponse response = apiRequestForIdSummoner.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    apiResponse = reader.ReadToEnd();
                }

                /*End*/
                /*http://json2csharp.com*/
                LolHelper rootObject = JsonConvert.DeserializeObject<LolHelper>(apiResponse);

                HttpWebRequest apiRequestForIdLeague = WebRequest.Create("https://" + Region + "1.api.riotgames.com/lol/league/v3/positions/by-summoner/" + rootObject.id + "?api_key=" + apiKey) as HttpWebRequest;

                string apiResponse1 = "";
                using (HttpWebResponse response = apiRequestForIdLeague.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    apiResponse1 = reader.ReadToEnd();  
                }

                var rootObject1 = JsonConvert.DeserializeObject<LeagueHelper[]>(apiResponse1);

                StringBuilder sb = new StringBuilder();
                sb.Append("<div id=\"LOL\">");
                sb.Append("<img id=\"summoner-icon\" src=\"http://ddragon.leagueoflegends.com/cdn/8.22.1/img/profileicon/" + rootObject.profileIconId + ".png\"/>");
                sb.Append("<p>" + rootObject.name + "</p>");
                sb.Append("<p>Summoner's lvl: " + rootObject.summonerLevel + "</p>");
                for (int i = 0; i < rootObject1.Length; i++)
                {
                    float win_rate = (rootObject1[i].wins / (rootObject1[i].wins + rootObject1[i].losses)) * 100;
                    sb.Append("<div class=\"div-result\">");
                    sb.Append("<p>League name: " + rootObject1[i].leagueName + "</p>");
                    sb.Append("<p>rank type: " + rootObject1[i].queueType + "</p>");
                    sb.Append("<p>rank: " + rootObject1[i].tier + " " + rootObject1[i].rank + "</p>");
                    sb.Append("<img id=\"rank-icon\" src=\"/images/base-icons/" + rootObject1[i].tier.ToLower() + ".png\"/>");
                    sb.Append("<p>" + rootObject1[i].leaguePoints + " LP</p>");
                    sb.Append("<p>" + rootObject1[i].wins + " W - " + rootObject1[i].losses + " L</p>");
                    sb.Append("<p> Total of game played: " + (rootObject1[i].wins + rootObject1[i].losses) + "</p>");
                    sb.Append("<p> Win rate: " + (int)win_rate + " %</p>");
                    sb.Append("</div>");
                }
                sb.Append("</div>");
                lol.LolResponse = sb.ToString();
            }
            else
            {
                ViewBag.ErrorMessage = "please provide a summoner name.";
            }
            return View("Index", lol);
        }

        [HttpGet]
        public ActionResult NSFW(DashboardViewModel nsfw, string tags, string search, string NumberResult)
        {
            int result = 0;

            if (NumberResult == null)
            {
                result = 1;
            }
            else
            {
                result = Int32.Parse(NumberResult);
            }
            if (search != null)
            {
                /*Calling API http://openweathermap.org/api */
                HttpWebRequest apiRequestForIdMovie = WebRequest.Create("https://api.redtube.com/?data=redtube.Videos.searchVideos&output=json&search=" + search + (tags != null ? "&tags[]=" + tags + "&thumbsize=medium" : "&thumbsize=medium")) as HttpWebRequest;

                string apiResponse = "";
                using (HttpWebResponse response = apiRequestForIdMovie.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    apiResponse = reader.ReadToEnd();
                }
                /*End*/
                /*http://json2csharp.com*/
                NSFW rootObject = JsonConvert.DeserializeObject<NSFW>(apiResponse);

                StringBuilder sb = new StringBuilder();
                sb.Append("<div id=\"nsfw-rep\">");
                for (int i = 0; i < result; i++)
                {
                    sb.Append("<div id=\"nsfw-div\">");
                    sb.Append("<img src=\"" + rootObject.videos[i].video.default_thumb + "\"/>");
                    sb.Append("<p>Title: <a href=\"" + rootObject.videos[i].video.url + "\">" + rootObject.videos[i].video.title + "</a></p>");
                    sb.Append("<p>Duration: " + rootObject.videos[i].video.duration + "</p>");
                    sb.Append("<p>Views: " + rootObject.videos[i].video.views + "</p>");
                    sb.Append("<p>rating: " + rootObject.videos[i].video.rating + " %</p>");
                    sb.Append("<p>Number of vote: " + rootObject.videos[i].video.ratings + "</p>");
                    sb.Append("<p>Published date: " + rootObject.videos[i].video.publish_date + "</p>");
                    sb.Append("</div>");
                }
                sb.Append("</div>");
                nsfw.NSFWRep = sb.ToString();

            }
            else
            {
                ViewBag.ErrorMessage = "please provide a movie name.";
            }
            return View("Index", nsfw);
        }

        // GET: Dashboard/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Dashboard/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Dashboard/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Dashboard/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Dashboard/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Dashboard/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
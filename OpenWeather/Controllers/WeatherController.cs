using OpenWeather.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpenWeather.Controllers
{
    public class WeatherController : Controller
    {
        public string Test()
        {
            return "Testing Weather Controller, Current Time: " + DateTime.Now.ToString();
        }

        public ActionResult City(string name)
        {
            var weather = OpenWeatherWebClient.GetWeather(name);
            return View(weather);
        }

        public ActionResult Location(string name)
        {
            var weather = OpenWeatherWebClient.GetWeather(name);
            return View(weather);
        }

    }
}
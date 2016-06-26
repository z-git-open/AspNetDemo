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

        public ActionResult Moscow()
        {
            var weather = OpenWeatherWebClient.GetWeather(524901);

            return View(weather);
            
        }


        // GET: Weather
        public ActionResult Index()
        {
            return View();
        }

    }
}
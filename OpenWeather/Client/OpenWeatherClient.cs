using Newtonsoft.Json;
using OpenWeather.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeather.Clients
{
    public class OpenWeatherWebClient
    {
        const string APIKey = "394bf88b568ad3b1aad96f6bf3c84db4";
        const string WeatherAPI = "http://api.openweathermap.org/data/2.5/forecast";

        static string CreateUri(int cityId)
        {
            return string.Format("{0}/city?APPID={1}&id={2}", WeatherAPI, APIKey, cityId.ToString());
        }

        //524901
        public static OpenWeatherRootObject GetWeather(int cityId)
        {
            WebClient client = new WebClient();
            var uri = CreateUri(cityId);
            var response = client.DownloadString(uri);
            var responseObj = JsonConvert.DeserializeObject<OpenWeatherRootObject>(response);
            return responseObj;
        }
    }
}

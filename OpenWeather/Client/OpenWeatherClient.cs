using Newtonsoft.Json;
using OpenWeather.Models;
using OpenWeather.Models.ByCityName;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeather.Clients
{
    public class OpenWeatherWebClient
    {
        const string APIKey = "394bf88b568ad3b1aad96f6bf3c84db4";
        const string WeatherAPI = "http://api.openweathermap.org/data/2.5/forecast";

        public static RootObject GetWeather(string cityName)
        {
            RootObject result = null;
            HttpClient client = new HttpClient();
            var uri = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&APPID={1}", cityName, APIKey);
            var task = client.GetAsync(uri)
                .ContinueWith(taskWithResponse =>
                {
                    var response = taskWithResponse.Result.EnsureSuccessStatusCode();
                    result = response.Content.ReadAsAsync<RootObject>().Result;
                });
            task.Wait();
            return result;
        }
    }
}

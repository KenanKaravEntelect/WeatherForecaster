using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using DotNetHelper_HttpClient.Helpers;
using System.IO;
using Kenan.Modules.WeatherForecaster.Integration.Rest.Models;
using Kenan.Modules.WeatherForecaster.Integration.Rest;

namespace Kenan.Modules.WeatherForecaster.Integration.Rest
{
    public class WeatherAPI: IWeatherAPI
    {


        public T GetWeather<T>(string CityName) where T : IRestModel
        {
            
                try
                {
                    string url = $"http://localhost:51302/api/Weather/City/{CityName}";
                    HttpWebRequest request = WebRequest.CreateHttp(url);
                    request.Method = "GET";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    string stringResult;
                    using (StreamReader reader = new StreamReader(response.GetResponseStream())) {

                        stringResult = reader.ReadToEnd();
                    }

                    T rawWeather = JsonSerializer.Deserialize<T>(stringResult);
                        
                    return rawWeather;
                }
                }
                catch (Exception e)
                {
                    return default(T);

                }
            
        }
    }
}
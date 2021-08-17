using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kenan.Modules.WeatherForecaster.Integration.Rest.Models;

namespace Kenan.Modules.WeatherForecaster.Integration.Rest
{
    interface IWeatherAPI
    {
        T GetWeather<T>(string CityName) where T : IRestModel;
    }
}

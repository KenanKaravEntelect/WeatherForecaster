using Kenan.Modules.WeatherForecaster.Models;
using System.Collections.Generic;

public interface ICityManager
{
    void CreateCity(City t);
    void DeleteCity(int cityId, int moduleId);
    void DeleteCity(City t);
    IEnumerable<City> GetCities(int moduleId);
    City GetCity(int cityId, int moduleId);
    void UpdateCity(City t);
}

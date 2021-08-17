using System.Collections.Generic;
using System.Linq;
using Kenan.Modules.WeatherForecaster.Components;
using Kenan.Modules.WeatherForecaster.Models;
using Moq;

namespace WeatherForecasterModuleTests.Mocks
{
    class MockStores
    {
        public static Mock<ICityManager> MockCityManager()
        {
            var allCitys = new List<City>();
            var mock = new Mock<ICityManager>();

            // void CreateCity(City t);
            mock.Setup(x => x.CreateCity(It.IsAny<City>()))
                .Callback((City i) =>
                {
                    allCitys.Add(i);
                });

            // void DeleteCity(int CityId, int moduleId);
            mock.Setup(x => x.DeleteCity(It.IsAny<int>(), It.IsAny<int>()))
                .Callback((int id, int mid) =>
                {
                    var remCity = allCitys.FirstOrDefault(i => i.CityId == id && i.ModuleId == mid);
                    allCitys.Remove(remCity);
                });

            // void DeleteCity(City t);
            mock.Setup(x => x.DeleteCity(It.IsAny<City>()))
                .Callback((City di) =>
                {
                    var remCity = allCitys.FirstOrDefault(i => i.CityId == di.CityId);
                    allCitys.Remove(remCity);
                });

            // IEnumerable<City> GetCities(int moduleId);
            mock.Setup(x => x.GetCities(It.IsAny<int>()))
                .Returns((int mid) => allCitys.Where(x => x.ModuleId == mid));

            // City GetCity(int CityId, int moduleId);
            mock.Setup(x => x.GetCity(It.IsAny<int>(), It.IsAny<int>()))
                .Returns((int id, int mid) => allCitys.FirstOrDefault(i => i.CityId == id && i.ModuleId == mid));

            // void UpdateCity(City t);
            mock.Setup(x => x.UpdateCity(It.IsAny<City>()))
                .Callback((City i) =>
                {
                    allCitys.Add(i);
                });

            return mock;
        }
    }
}

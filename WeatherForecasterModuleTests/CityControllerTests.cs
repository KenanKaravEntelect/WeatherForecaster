using Kenan.Modules.WeatherForecaster.Controllers;
using Kenan.Modules.WeatherForecaster.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using WeatherForecasterModuleTests.Mocks;

namespace WeatherForecasterModuleTests
{
    [TestClass]
    public class CityControllerTests
    {
        [TestMethod]
        public void Edit_CreateNewCity_ModuleIdAssignedInModel()
        {

            // 1 - Arrange
            int moduleId = 2;
            var mockData = MockStores.MockCityManager();
            
            var modTwoCityCtrl = new CityController(mockData.Object, moduleId);

            // 2 - Act 
            var actionResult = (ViewResult)modTwoCityCtrl.Edit();

            // 3 - Assert
            var cityModel = (City)actionResult.Model;
            Assert.IsTrue(cityModel != null && cityModel.ModuleId == moduleId);


        }

        [TestMethod]
        public void Index_ListMultipleCities_CityIdAssignedInModels()
        {

            // 1 - Arrange
            int moduleId = 2;
            int cityId1 = 1;
            int cityId2 = 2;
            var mockData = MockStores.MockCityManager();

            var modTwoCityCtrl = new CityController(mockData.Object, moduleId);

            // 2 - Act 
            var actionResult = (ViewResult)modTwoCityCtrl.Edit(cityId1);
            actionResult = (ViewResult)modTwoCityCtrl.Edit(cityId2);

            var cityModel = (City)actionResult.Model;
            var deleteResult = (ViewResult)modTwoCityCtrl.Delete(cityModel.ModuleId);

            // 3 - Assert
            var deletedCityModel = (City)actionResult.Model;
            Assert.IsTrue(deletedCityModel == null);


        }
    }
}

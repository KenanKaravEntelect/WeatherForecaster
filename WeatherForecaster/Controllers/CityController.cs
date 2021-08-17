using DotNetNuke.Entities.Users;
using DotNetNuke.Framework.JavaScriptLibraries;
using DotNetNuke.Web.Mvc.Framework.ActionFilters;
using DotNetNuke.Web.Mvc.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Kenan.Modules.WeatherForecaster.Components;
using Kenan.Modules.WeatherForecaster.Models;
using Kenan.Modules.WeatherForecaster.Integration.Rest;
using Kenan.Modules.WeatherForecaster.Integration.Models;
using DotNetNuke.Framework;

namespace Kenan.Modules.WeatherForecaster.Controllers
{
    [DnnHandleError]
    public class CityController : DnnController
    {
  
        private int _moduleId;
        public CityController(ICityManager manager, int moduleId) {

            this._moduleId = moduleId;
           

        }
        public CityController()
        {


        }
        
        public ActionResult Delete(int CityId)
        {

            try {

                CityManager.Instance.DeleteCity(CityId, ModuleContext.ModuleId);

                return RedirectToAction("Index");
            }

            catch (Exception e) {

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int CityId = -1)
        {

            try
            {
                DotNetNuke.Framework.JavaScriptLibraries.JavaScript.RequestRegistration(CommonJs.DnnPlugins);
            }
            catch { 
            }

            if (PortalSettings != null)
            {
                TempData["AllowManualEditing"] = ModuleContext.Configuration.ModuleSettings["WeatherForecaster_AllowManualEditing"] as string;

                var userlist = UserController.GetUsers(PortalSettings.PortalId);
                var users = from user in userlist.Cast<UserInfo>().ToList()
                            select new SelectListItem { Text = user.DisplayName, Value = user.UserID.ToString() };

                ViewBag.Users = users;
            }
            if (ModuleContext != null) {

                _moduleId = ModuleContext.ModuleId;
            
            }
            var city = (CityId == -1)
                 ? new City { ModuleId = _moduleId}
                 : CityManager.Instance.GetCity(CityId, _moduleId);

            return View(city);
        }

        [HttpPost]
        [DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
        public ActionResult Edit(City city)
        {
            bool ManualEditing = Boolean.Parse((string)TempData["AllowManualEditing"]??"False");
            try
            {
                RootObject weather;
                if (ManualEditing)
                {
                    weather = new RootObject {
                        main = new Main {
                            temp = Double.Parse(city.Temperature),
                            temp_max = Double.Parse(city.TemperatureMax),
                            temp_min = Double.Parse(city.TemperatureMin),

                        },
                        weather = new List<Weather>{new Weather {
                            description = city.CityDescription,
                        } ,
                    }

                    };
                }
                else
                {
                    WeatherAPI weatherAPI = new WeatherAPI();
                    weather = weatherAPI.GetWeather<RootObject>(city.CityName);

                    if (weather == null) // DataStub for no API access
                    {

                        weather = new RootObject
                        {

                            main = new Main
                            {
                                temp = 10,
                                temp_min = 5,
                                temp_max = 15,
                            },
                            weather = new List<Weather>{ new Weather
                        {
                            description = "Clear",
                        } },
                        };
                    }
                }


                
                var userlist = UserController.GetUsers(PortalSettings.PortalId);
                var users = userlist.Cast<UserInfo>().ToList();
                           
                if (city.CityId == -1)
                {
                    city.CreatedByUserId = User.UserID;
                    city.CreatedOnDate = DateTime.UtcNow;
                    city.LastModifiedByUserId = User.UserID;
                    city.LastModifiedOnDate = DateTime.UtcNow;
                    city.AssignedUserId = users.First().UserID;
                    city.Temperature = weather.main.temp.ToString();
                    city.TemperatureMax = weather.main.temp_max.ToString();
                    city.TemperatureMin = weather.main.temp_min.ToString();
                    city.CityDescription = weather.weather.First().description;
                    CityManager.Instance.CreateCity(city);
                }
                else
                {
                    var existingCity = CityManager.Instance.GetCity(city.CityId, city.ModuleId);
                    existingCity.LastModifiedByUserId = User.UserID;
                    existingCity.LastModifiedOnDate = DateTime.UtcNow;
                    existingCity.CityName = city.CityName;
                    existingCity.CityDescription = city.CityDescription;
                    existingCity.AssignedUserId = city.AssignedUserId;
                    existingCity.Temperature = weather.main.temp.ToString();
                    existingCity.TemperatureMax = weather.main.temp_max.ToString();
                    existingCity.TemperatureMin = weather.main.temp_min.ToString();
                    existingCity.CityDescription = weather.weather.First().description;
                    CityManager.Instance.UpdateCity(existingCity);
                }
            
            return RedirectToDefaultRoute();
            }
            catch (Exception e)
            {
                
                 city = (city.CityId == -1)
                     ? new City { ModuleId = ModuleContext.ModuleId }
                     : CityManager.Instance.GetCity(city.CityId, ModuleContext.ModuleId);

                string error = e.ToString();
                ViewBag.Error = error;
                return View(city);
            }
        }

        [ModuleAction(ControlKey = "Edit", TitleKey = "AddCity")]
        public ActionResult Index()
        {
            var Cities = CityManager.Instance.GetCities(ModuleContext.ModuleId);
            return View(Cities);
        }
    }
}

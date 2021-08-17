/*
' Copyright (c) 2021 Entelect
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using DotNetNuke.Data;
using DotNetNuke.Framework;
using System.Collections.Generic;
using Kenan.Modules.WeatherForecaster.Models;

namespace Kenan.Modules.WeatherForecaster.Components
{
   
    public class CityManager : ServiceLocator<ICityManager, CityManager>, ICityManager
    {
        public void CreateCity(City t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<City>();
                rep.Insert(t);
            }
        }

        public void DeleteCity(int CityId, int moduleId)
        {
            var t = GetCity(CityId, moduleId);
            DeleteCity(t);
        }

        public void DeleteCity(City t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<City>();
                rep.Delete(t);
            }
        }

        public IEnumerable<City> GetCities(int moduleId)
        {
            IEnumerable<City> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<City>();
                t = rep.Get(moduleId);
            }
            return t;
        }

        public City GetCity(int CityId, int moduleId)
        {
            City t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<City>();
                t = rep.GetById(CityId, moduleId);
            }
            return t;
        }

        public void UpdateCity(City t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<City>();
                rep.Update(t);
            }
        }

        protected override System.Func<ICityManager> GetFactory()
        {
            return () => new CityManager();
        }
    }
}

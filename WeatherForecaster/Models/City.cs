using DotNetNuke.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kenan.Modules.WeatherForecaster.Models
{
   
        [TableName("WeatherForecaster_Cities")]
        //setup the primary key for table
        [PrimaryKey("CityId", AutoIncrement = true)]
        //scope the objects to the ModuleId of a module on a page (or copy of a module on a page)
        [Scope("ModuleId")]
        public class City
        {
            ///<summary>
            /// The ID of your object with the name of the ItemName
            ///</summary>
            public int CityId { get; set; } = -1;
            ///<summary>
            /// A string with the name of the ItemName
            ///</summary>
            public string CityName { get; set; }

            ///<summary>
            /// A string with the description of the object
            ///</summary>
            public string CityDescription { get; set; }

            public string Temperature { get; set; }

            public string TemperatureMin { get; set; }

            public string TemperatureMax { get; set; }

            ///<summary>
            /// An integer with the user id of the assigned user for the object
            ///</summary>
            public int AssignedUserId { get; set; }

            ///<summary>
            /// The ModuleId of where the object was created and gets displayed
            ///</summary>
            public int ModuleId { get; set; }

            ///<summary>
            /// An integer for the user id of the user who created the object
            ///</summary>
            public int CreatedByUserId { get; set; } = -1;

            ///<summary>
            /// An integer for the user id of the user who last updated the object
            ///</summary>
            public int LastModifiedByUserId { get; set; } = -1;

            ///<summary>
            /// The date the object was created
            ///</summary>
            public DateTime CreatedOnDate { get; set; } = DateTime.UtcNow;

            ///<summary>
            /// The date the object was updated
            ///</summary>
            public DateTime LastModifiedOnDate { get; set; } = DateTime.UtcNow;
        }
    }

using System;

namespace _09_MVVM.Models
{
    // copy json-object
    // Edit -> Paste Special -> Paste JSON as classes
    // OR https://jsonutils.com

    // TODO hometask 1: Try https://jsonplaceholder.typicode.com/

    public class Units
    {
        public double Value { get; set; }
        public string Unit { get; set; }
        public int UnitType { get; set; }
    }

    public class Temperature
    {
        public Units Metric { get; set; }
        public Units Imperial { get; set; }
    }

    public class CurrentConditions
    {
        public DateTime LocalObservationDateTime { get; set; }
        public int EpochTime { get; set; }
        public string WeatherText { get; set; } 
        public int WeatherIcon { get; set; }// 1..2..32
        public bool HasPrecipitation { get; set; } // чи э опади?
        public object PrecipitationType { get; set; }
        public bool IsDayTime { get; set; } = false; // чи день?
        public Temperature Temperature { get; set; }
        public string MobileLink { get; set; }
        public string Link { get; set; }
    }

    // WeatherIcon -> Image
    // hasPresipitation -> text
    // IsDayTime -> колір теми (світла, темна) на стек панель з погодніми умовами

    // Прогноз погоди на 5 днів
    // https://developer.accuweather.com/accuweather-forecast-api/apis/get/forecasts/v1/daily/5day/%7BlocationKey%7D
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09_MVVM.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Header
    {
        public string EffectiveDate { get; set; }
        public string EffectiveEpochDate { get; set; }
        public int Severity { get; set; }
        public string Text { get; set; }
        public string Category { get; set; }
        public string EndDate { get; set; }
        public string EndEpochDate { get; set; }
        public string MobileLink { get; set; }
        public string Link { get; set; }
    }

    public class Temperature_T
    {
        public Units Minimum { get; set; }
        public Units Maximum { get; set; }
    }

    public class TimeOfDay
    {
        public int Icon { get; set; }
        public string IconPhrase { get; set; }
        public bool HasPrecipitation { get; set; }
        public string PrecipitationType { get; set; }
        public string PrecipitationIntensity { get; set; }
    }

    public class DailyForecast
    {
        public DateTime Date { get; set; }
        public long EpochDate { get; set; }
      
        [JsonProperty("Temperature")]
        public Temperature_T Temperature { get; set; }
        public TimeOfDay Day { get; set; }
        public TimeOfDay Night { get; set; }
        public List<string> Sources { get; set; }
        public string MobileLink { get; set; }
        public string Link { get; set; }
    }

    public class FiveDayConditions
    {
        public Header Headline { get; set; }
        public List<DailyForecast> DailyForecasts { get; set; }
    }


}

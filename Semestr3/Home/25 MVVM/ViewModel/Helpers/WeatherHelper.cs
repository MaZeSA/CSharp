using _09_MVVM.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace _09_MVVM.ViewModel.Helpers
{
    public static class WeatherHelper
    {
        // API - application programming interface

        private const string BASE_URL = "http://dataservice.accuweather.com/";
        private const string API_KEY = "AthsL5TjcTPQdxJWgQ9gAOlssaWvFU1v"; // в кожного свій (зареєструвати) AthsL5TjcTPQdxJWgQ9gAOlssaWvFU1v
        private const string AUTOCOMPLETE_ENDPOINT = "locations/v1/cities/autocomplete?apikey={0}&q={1}";
        private const string CURRENT_CONDITIONS_ENDPOINT = "currentconditions/v1/{0}?apikey={1}";
        private const string DAILY5DAY = "forecasts/v1/daily/5day/{0}?apikey={1}";

        public static async Task<List<City>> GetCitiesAsync(string query)
        {
            var cities = new List<City>();

            var url = BASE_URL + String.Format(AUTOCOMPLETE_ENDPOINT, API_KEY, query);
            try
            {
                using (var client = new HttpClient())
                {
                    var responce = await client.GetAsync(url);
                    var json = await responce.Content.ReadAsStringAsync();
                    cities = JsonConvert.DeserializeObject<List<City>>(json);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            return cities;
        }

        public static async Task<CurrentConditions> GetCurrentConditionsAsync(string locationKey)
        {
            var currentConditionsList = new List<CurrentConditions>();

            var url = BASE_URL + String.Format(CURRENT_CONDITIONS_ENDPOINT, locationKey, API_KEY);

            using (var client = new HttpClient())
            {
                var responce = await client.GetAsync(url);
                var json = await responce.Content.ReadAsStringAsync();
                currentConditionsList = JsonConvert.DeserializeObject<List<CurrentConditions>>(json);
            }

            return currentConditionsList.FirstOrDefault();
        }
        public static async Task<FiveDayConditions> GetFiveDeyConditionsAsync(string locationKey)
        {
            FiveDayConditions fiveDeyConditions = null;

            var url = BASE_URL + String.Format(DAILY5DAY, locationKey, API_KEY);

            using (var client = new HttpClient())
            {
                var responce = await client.GetAsync(url);
                var json = await responce.Content.ReadAsStringAsync();
                fiveDeyConditions = JsonConvert.DeserializeObject<FiveDayConditions>(json);
            }

            return fiveDeyConditions;
        }
    }
}

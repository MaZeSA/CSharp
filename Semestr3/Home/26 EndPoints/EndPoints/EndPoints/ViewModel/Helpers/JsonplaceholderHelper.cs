using EndPoints.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EndPoints.ViewModel.Helpers
{
    public static class JsonplaceholderHelper
    {
        private const string BASE_URL = "https://jsonplaceholder.typicode.com/";
        //private const string TODOS = "todos"; 
        //private const string USERS = "users";

        public static async Task<List<T>> GetUsersAsync<T>(string query)
        {
            var data = new List<T>();

            var url = BASE_URL + query;
            try
            {
                using (var client = new HttpClient())
                {
                    var responce = await client.GetAsync(url);
                    var json = await responce.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<List<T>>(json);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return data;
        }
    }
}

using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using B3MobileApp.Model;
using Newtonsoft.Json;

namespace B3MobileApp.Services
{
    internal class ActivityDataService : IActivityDataService
    {
        private readonly HttpClient _httpClient;

        public ActivityDataService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> SaveActivity(Activity activity)
        {
            var restUrl = "http://192.168.1.2:58938/api/activity";
            var uri = new Uri(string.Format(restUrl));

            var json = JsonConvert.SerializeObject(activity);
            var content = new StringContent(json, Encoding.UTF8, "application/json");


            HttpResponseMessage response = null;
            response = await _httpClient.PostAsync(uri, content);


            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Activity successfully saved on server.");
            }
            else
            {
                Debug.WriteLine("Something went wrong...");
            }

            return await response.Content.ReadAsStringAsync();

        }

    }
}
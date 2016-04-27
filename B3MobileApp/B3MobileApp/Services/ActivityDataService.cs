using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using B3MobileApp.Helpers;
using B3MobileApp.Model;
using Newtonsoft.Json;

namespace B3MobileApp.Services
{
    internal class ActivityDataService : IActivityDataService
    {
        private readonly HttpClient _httpClient;
        private readonly Uri _restUri;

        public ActivityDataService()
        {
            var httpClientHandler = new HttpClientHandler()
            {
                MaxRequestContentBufferSize = 256000
            };

            //TODO consider to use an interface of httpclient and inject it
            _httpClient = new HttpClient(httpClientHandler);

            //TODO firstly, check if it isn't null, empty or whitespace
            _restUri = new Uri(Settings.ActivityRestUri);
        }

        public async Task SaveActivity(Activity activity)
        {

            var activityJson = JsonConvert.SerializeObject(activity);
            var activityHttpContent = new StringContent(activityJson, Encoding.UTF8, "application/json");

            //delete line below (or set more rational timeout). it was set for tests purposes only
            //_httpClient.Timeout = TimeSpan.FromSeconds(1);

            //HttpResponseMessage response = null;
            var cts = new CancellationTokenSource();

            try
            {
                var response = await _httpClient.PostAsync(_restUri, activityHttpContent, cts.Token);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                //TODO log an http exception ex.ToString()
            }
            catch (TaskCanceledException ex)
            {
                if (ex.CancellationToken == cts.Token)
                {
                    //TODO log a cancellation triggered by the caller
                }
                else
                {
                    //TODO log a web request timeout
                }
            }

        }

    }
}
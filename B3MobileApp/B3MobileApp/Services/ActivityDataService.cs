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
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;
        private readonly Uri _restUri;

        public ActivityDataService(ILogger logger)
        {
            _logger = logger;
            var httpClientHandler = new HttpClientHandler()
            {
                MaxRequestContentBufferSize = 256000
            };

            //TODO consider to use an interface of httpclient and inject it
            _httpClient = new HttpClient(httpClientHandler);

            //TODO firstly, check if it isn't null, empty or whitespace
            //_restUri = new Uri(Settings.ActivityRestUri);
            //TODO replaced for tests
            _restUri = new Uri("http://192.168.1.2:58938/api/activity");
        }

        public async Task SaveActivity(Activity activity)
        {

            var activityJson = JsonConvert.SerializeObject(activity);
            var activityHttpContent = new StringContent(activityJson, Encoding.UTF8, "application/json");

            var cts = new CancellationTokenSource();

            try
            {
                var response = await _httpClient.PostAsync(_restUri, activityHttpContent, cts.Token);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                _logger.Log(ex.ToString(), "SaveActivity", LogType.Error);
                throw new Exception("Http request error while sending activity to server.");
            }
            catch (TaskCanceledException ex)
            {
                if (ex.CancellationToken == cts.Token)
                {
                    _logger.Log("Activity send was canceled by user", "SaveActivity", LogType.Info);
                }
                else
                {
                    _logger.Log("Http request timeout", "SaveActivity", LogType.Error);
                    throw new Exception("Http request timeout while sending activity to server.");
                }
            }

        }

    }
}
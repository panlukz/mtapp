using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Mtapp.Helpers;
using Mtapp.Models;
using Newtonsoft.Json;

namespace Mtapp.Services
{
    public class ActivityDataService : IActivityDataService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;
        private Uri _activityRestUri;

        public ActivityDataService(ILogger logger)
        {
            _logger = logger;
            _httpClient = CreateHttpClient();
        }

        private HttpClient CreateHttpClient()
        {
            var httpClientHandler = new HttpClientHandler
            {
                MaxRequestContentBufferSize = 256000
            };

            return new HttpClient(httpClientHandler);
        }

        private void RefreshHttpConnectionSettings()
        {
            _httpClient.Timeout = TimeSpan.FromSeconds(20);

            var activityRestApi = Settings.ActivityRestUri;
            if (string.IsNullOrWhiteSpace(activityRestApi))
                throw new FormatException("Incorrect activity restapi format");

            _activityRestUri = new Uri(activityRestApi);

            var apiToken = Settings.ApiToken;
            if (string.IsNullOrWhiteSpace(apiToken))
                throw new FormatException("Invalid token");

            var authHeader = new AuthenticationHeaderValue("Token", apiToken);
            _httpClient.DefaultRequestHeaders.Authorization = authHeader;
        }

        public async Task Add(Activity activity)
        {
            RefreshHttpConnectionSettings();

            var activityJson = JsonConvert.SerializeObject(activity);
            var activityHttpContent = new StringContent(activityJson, Encoding.UTF8, "application/json");

            var cts = new CancellationTokenSource();

            try
            {
                var response = await _httpClient.PostAsync(_activityRestUri, activityHttpContent, cts.Token);
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
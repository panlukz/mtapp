using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mtapp.Helpers;
using Mtapp.Models;
using Newtonsoft.Json;

namespace Mtapp.Services
{
    public class AuthService : IAuthService
    {
        private HttpClient _httpClient;
        private Uri _authApiEndpoint;

        public AuthService()
        {
            _httpClient = createHttpClient();
            _authApiEndpoint = new Uri(Settings.AuthApiEndpoint);
        }

        private HttpClient createHttpClient()
        {
            var client = new HttpClient(new HttpClientHandler()
            {
                MaxRequestContentBufferSize = 256000
            });
            client.Timeout = TimeSpan.FromSeconds(20);



            return client;
        }

        public async Task<string> GetToken(UserAuth userAuth)
        {
            var userAuthJson = JsonConvert.SerializeObject(userAuth);
            var userAuthHttpContent = new StringContent(userAuthJson, Encoding.UTF8, "application/json");

            var cts = new CancellationTokenSource();
            string token = null;
            try
            {
                var response = await _httpClient.PostAsync(_authApiEndpoint, userAuthHttpContent, cts.Token);
                response.EnsureSuccessStatusCode();
                var responseJson = JsonConvert.DeserializeObject<TokenAuth>(response.Content.ReadAsStringAsync().Result);
                token = responseJson.Token;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Http request error while user login.");
            }
            catch (TaskCanceledException ex)
            {
                if (ex.CancellationToken == cts.Token)
                {
                    //canceled by user
                }
                else
                {
                    throw new Exception("Http request timeout!");
                }
            }

            return token;
        }
    }
}

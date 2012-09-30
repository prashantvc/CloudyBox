using System;
using System.Net.Http;
using System.Threading.Tasks;
using CloudyBoxLib.OAuth;

namespace CloudyBoxLib
{
    public sealed class Client : IDisposable
    {
        private readonly string _apikey = "ud1mygzz55xaory";
        private readonly string _appSecret = "xhr7bp2ohcs541r";
        private const string BaseUrl = "https://api.dropbox.com/1/";
        private const string ContentBaseUrl = "https://api-content.dropbox.com";

        private readonly HttpClient _client;

        public Client()
        {
            _client = new HttpClient(new OAuthMessageHandler(new HttpClientHandler()))
                          {
                              BaseAddress = new Uri(BaseUrl)
                          };  
        }


        public async Task<string> CreateTokenRequest()
        {
            var response = await _client.GetAsync("oauth/request_token");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}

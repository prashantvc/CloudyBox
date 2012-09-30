using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CloudyBoxLib.OAuth;
using Windows.Foundation;

namespace CloudyBoxLib
{
    public sealed class Client
    {
        private readonly string _apikey = "ud1mygzz55xaory";
        private readonly string _appSecret = "xhr7bp2ohcs541r";
        private const string BaseUrl = "https://api.dropbox.com";
        private const string ContentBaseUrl = "https://api-content.dropbox.com";
        private const string SignatureMethod = "PLAINTEXT";

        private readonly HttpClient _client;

        public Client()
        {
            _client = new HttpClient(new OAuthMessageHandler(new HttpClientHandler()));
            _client.BaseAddress = new Uri(BaseUrl);
        }


        public async Task<string> CreateTokenRequest()
        {
            var s = await _client.GetAsync("1/oauth/request_token");
            s.EnsureSuccessStatusCode();
            return await s.Content.ReadAsStringAsync();
        }
    }
}

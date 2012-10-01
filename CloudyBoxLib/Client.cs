using System;
using System.Net.Http;
using System.Threading.Tasks;
using CloudyBoxLib.Model;
using CloudyBoxLib.OAuth;

namespace CloudyBoxLib
{
    public sealed class Client : IDisposable
    {
        private const string BaseUrl = "https://api.dropbox.com/1/";
        private const string ContentBaseUrl = "https://api-content.dropbox.com";

        private readonly HttpClient _client;
        private readonly OAuthMessageHandler _messageHandler;

        public Client()
            : this("ud1mygzz55xaory", "xhr7bp2ohcs541r")
        {
        }

        public Client(string apikey, string appSecret)
        {
            _messageHandler = new OAuthMessageHandler(new HttpClientHandler(), apikey, appSecret, new UserLogin());

            _client = new HttpClient(_messageHandler)
                          {
                              BaseAddress = new Uri(BaseUrl)
                          };
        }

        public async Task<string> GetMetadata()
        {
            var res = await _client.GetAsync("metadata/dropbox");
            res.EnsureSuccessStatusCode();
         
            return await res.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Creates the token request.
        /// </summary>
        /// <returns>User login</returns>
        public async Task<UserLogin> GetToken()
        {
            var response = await _client.GetAsync("oauth/request_token");
            response.EnsureSuccessStatusCode();

            string urlParams = await response.Content.ReadAsStringAsync();

            return GetUserLoginFromParams(urlParams);
        }

        public async Task<string> AccessToken()
        {
            var res = await _client.GetAsync("oauth/access_token");
            res.EnsureSuccessStatusCode();
            return await res.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Creates the authorise URL.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <param name="callbackUrl">The callback URL.</param>
        /// <returns>Authorisation Url</returns>
        public string CreateAuthoriseUrl(UserLogin login, string callbackUrl = null)
        {
            return string.Format("https://www.dropbox.com/1/oauth/authorize?oauth_token={0}{1}", login.Token,
                (string.IsNullOrEmpty(callbackUrl) ? string.Empty : "&oauth_callback=" + callbackUrl));
        }

        public void SetUserLoginToHandler(UserLogin login)
        {
            _messageHandler.SetLogin(login);
        }

        UserLogin GetUserLoginFromParams(string urlParams)
        {
            var userLogin = new UserLogin();

            var parameters = urlParams.Split('&');

            foreach (var parameter in parameters)
            {
                if (parameter.Split('=')[0] == "oauth_token_secret")
                {
                    userLogin.Secret = parameter.Split('=')[1];
                }
                else if (parameter.Split('=')[0] == "oauth_token")
                {
                    userLogin.Token = parameter.Split('=')[1];
                }
            }

            return userLogin;
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}

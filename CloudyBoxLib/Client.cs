using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CloudyBoxLib.Model;
using CloudyBoxLib.OAuth;
using CloudyBoxLib.Utilities;

namespace CloudyBoxLib
{
    public sealed class Client : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Client" /> class.
        /// </summary>
        public Client()
            : this("ud1mygzz55xaory", "xhr7bp2ohcs541r")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Client" /> class.
        /// </summary>
        /// <param name="apikey">The apikey.</param>
        /// <param name="appSecret">The app secret.</param>
        public Client(string apikey, string appSecret)
        {
            _messageHandler = new OAuthMessageHandler(new HttpClientHandler(), apikey, appSecret, new UserLogin());

            _client = new HttpClient(_messageHandler)
                          {
                              BaseAddress = new Uri(BaseUrl)
                          };
        }

        /// <summary>
        /// Gets the account information.
        /// </summary>
        /// <returns>Login user account infromation</returns>
        public async Task<AccountInformation> GetAccountInformation()
        {
            var response = await _client.GetAsync("account/info");
            response.EnsureSuccessStatusCode();

            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                return stream.ReadJsonObject<AccountInformation>();
            }
        }

        /// <summary>
        /// Gets the root metadata
        /// </summary>
        /// <returns>Metadata for the root</returns>
        public async Task<Response<Metadata>> GetRoot()
        {
            return await GetMetadata(string.Empty);
        }

        /// <summary>
        /// Gets the metadata for the given path
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>Metadata for the path</returns>
        public async Task<Response<Metadata>> GetMetadata(string path)
        {
            string url;
            bool success;

            if (string.IsNullOrEmpty(path))
            {
                var value = GetHash(Root, out success);
                url = success
                          ? string.Format("metadata/{0}?hash={1}", Root, value)
                          : string.Format("metadata/{0}", Root);
                path = Root;
            }
            else
            {
                
                var value = GetHash(path, out success);
                url = success
                          ? string.Format("metadata/{0}/{1}?hash={2}", Root, path, value)
                          : string.Format("metadata/{0}/{1}", Root, path);
            }

            var response = await _client.GetAsync(url);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return new Response<Metadata>(response.StatusCode, null);
            }

            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                var metadata = stream.ReadJsonObject<Metadata>();
                if (metadata.Hash != null)
                {
                    if (_metadataHash.ContainsKey(path))
                    {
                        _metadataHash[path] = metadata.Hash;
                    }
                    else
                    {   
                        _metadataHash.Add(path, metadata.Hash);
                    }
                }

                return new Response<Metadata>(response.StatusCode, metadata);
            }
        }

        private string GetHash(string path, out bool success)
        {
            string value;
            success = _metadataHash.TryGetValue(path, out value);
            return value;
        }

        /// <summary>
        /// Creates the token request.
        /// </summary>
        /// <returns>User login</returns>
        public async Task<UserLogin> RequestToken()
        {
            var response = await _client.GetAsync("oauth/request_token");
            response.EnsureSuccessStatusCode();

            string urlParams = await response.Content.ReadAsStringAsync();

            return GetUserLoginFromParams(urlParams);
        }
        
        /// <summary>
        /// Accesses the token.
        /// </summary>
        /// <returns>User login</returns>
        public async Task<Response<UserLogin>> AccessToken()
        {
            var response = await _client.GetAsync("oauth/access_token");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return new Response<UserLogin>(response.StatusCode, null);
            }

            string urlParams = await response.Content.ReadAsStringAsync();
            var userLogin = GetUserLoginFromParams(urlParams);

            return new Response<UserLogin>(response.StatusCode, userLogin);
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

        /// <summary>
        /// Sets the user login to handler.
        /// </summary>
        /// <param name="login">The login.</param>
        public void SetUserLoginToHandler(UserLogin login)
        {
            _messageHandler.SetLogin(login);
        }

        static UserLogin GetUserLoginFromParams(string urlParams)
        {
            var userLogin = new UserLogin();

            var parameters = urlParams.Split('&');

            foreach (var parameter in parameters)
            {
                switch (parameter.Split('=')[0])
                {
                    case "oauth_token_secret":
                        userLogin.Secret = parameter.Split('=')[1];
                        break;
                    case "oauth_token":
                        userLogin.Token = parameter.Split('=')[1];
                        break;
                }
            }

            return userLogin;
        }

        const string BaseUrl = "https://api.dropbox.com/1/";
        const string ContentBaseUrl = "https://api-content.dropbox.com";
        const string Root = "dropbox";

        readonly HttpClient _client;
        readonly OAuthMessageHandler _messageHandler;

        readonly Dictionary<string, string> _metadataHash
            = new Dictionary<string, string>();

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}

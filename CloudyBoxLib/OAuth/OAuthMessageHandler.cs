using System.Net.Http;
using System.Net.Http.Headers;
using CloudyBoxLib.Model;

namespace CloudyBoxLib.OAuth
{
    public class OAuthMessageHandler : DelegatingHandler
    {
        public OAuthMessageHandler(HttpMessageHandler handler, string apiKey, string appSecret, UserLogin login)
            : base(handler)
        {
            _apiKey = apiKey;
            _appSecret = appSecret;

            _login = login;
            _authBase = new OAuthBase();
        }

        protected override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            string normalizedUri;
            string authHeader;
            string normalizedParameters;

            _authBase.GenerateSignature(
                request.RequestUri,
                _apiKey,
                _appSecret,
                _login.Token,
                _login.Secret,
                request.Method.Method,
                _authBase.GenerateTimeStamp(),
                _authBase.GenerateNonce(),
                out normalizedUri,
                out normalizedParameters,
                out authHeader);

            request.Headers.Authorization = new AuthenticationHeaderValue("OAuth", authHeader);

            return base.SendAsync(request, cancellationToken);
        }

        public void SetLogin(UserLogin login)
        {
            _login = login;
        }

        readonly OAuthBase _authBase;
        readonly string _apiKey ;
        readonly string _appSecret ;
        private UserLogin _login;
    }
}
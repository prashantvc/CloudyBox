using System.Net.Http;
using System.Net.Http.Headers;

namespace CloudyBoxLib.OAuth
{
    public class OAuthMessageHandler : DelegatingHandler
    {
        public OAuthMessageHandler(HttpMessageHandler handler)
            : base(handler)
        {
            _authBase = new OAuthBase();
        }

        protected override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            string normalizedUri;
            string authHeader;
            string normalizedParameters;

            _authBase.GenerateSignature(
                request.RequestUri,
                ConsumerKey,
                ConsumerSecret,
                _token,
                _tokenSecret,
                request.Method.Method,
                _authBase.GenerateTimeStamp(),
                _authBase.GenerateNonce(),
                out normalizedUri,
                out normalizedParameters,
                out authHeader);

            request.Headers.Authorization = new AuthenticationHeaderValue("OAuth", authHeader);

            return base.SendAsync(request, cancellationToken);
        }

        private readonly OAuthBase _authBase;
        private const string ConsumerKey = "ud1mygzz55xaory";
        private const string ConsumerSecret = "xhr7bp2ohcs541r";

        private static string _token = string.Empty;
        private static string _tokenSecret = string.Empty;

    }
}
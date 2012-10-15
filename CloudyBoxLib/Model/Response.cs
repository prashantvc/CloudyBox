using System.Net;

namespace CloudyBoxLib.Model
{
    public class Response<T>
    {
        public Response(HttpStatusCode statusCode, T data)
        {
            StatusCode = statusCode;
            Data = data;
        }

        public HttpStatusCode StatusCode { get; private set; }
        public T Data { get; private set; }
    }
}
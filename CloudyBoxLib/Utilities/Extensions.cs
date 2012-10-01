using System.IO;
using System.Runtime.Serialization.Json;

namespace CloudyBoxLib.Utilities
{
    public static class Extensions
    {
        public static T ReadJsonObject<T>(this Stream stream) where T : class
        {
            var serializer = new DataContractJsonSerializer(typeof (T));
            var obj = serializer.ReadObject(stream) as T;

            if (obj != null)
            {
                return obj;
            }

            return null;
        }
    }
}
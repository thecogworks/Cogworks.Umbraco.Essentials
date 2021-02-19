using System.Web.Mvc;
using Newtonsoft.Json;

namespace Cogworks.Umbraco.Essentials.Extensions
{
    public static class TempDataExtensions
    {
        public static void Put<T>(this TempDataDictionary tempData, string key, T value) where T : class
            => tempData[key] = JsonConvert.SerializeObject(value);

        public static T Get<T>(this TempDataDictionary tempData, string key) where T : class
            => tempData.TryGetValue(key, out var o)
                ? JsonConvert.DeserializeObject<T>((string)o)
                : null;
    }
}
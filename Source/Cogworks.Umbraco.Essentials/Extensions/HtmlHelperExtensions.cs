using System.IO;
using System.Web;
using System.Web.Mvc;
using Cogworks.Essentials.Extensions;

#pragma warning disable SA1313 // Parameter names should begin with lower-case letter

namespace Cogworks.Umbraco.Essentials.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static string GetCacheableUrl(this HtmlHelper _, string url)
            => GetCacheableUrl(url);

        public static string GetCacheableUrl<T>(this HtmlHelper<T> _, string url)
            => GetCacheableUrl(url);

        public static string GetCacheableUrl(string url)
        {
            if (!url.HasValue())
            {
                return string.Empty;
            }

            if (!url.StartsWith("/") || url.StartsWith("//"))
            {
                return url;
            }

            var file = new FileInfo(HttpContext.Current.Server.MapPath("~" + url));

            if (!file.Exists)
            {
                return url;
            }

            var ticks = file.LastWriteTimeUtc.Ticks;

            return url + (url.Contains("?") ? "&v=" : "?v=") + ticks;
        }
    }
}
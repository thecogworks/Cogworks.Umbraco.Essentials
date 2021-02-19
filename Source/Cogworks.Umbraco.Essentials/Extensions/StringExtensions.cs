using System;
using System.Web;
using Umbraco.Core;

namespace Cogworks.Umbraco.Essentials.Extensions
{
    public static class StringExtensions
    {
        public static bool HasValue(this string input)
            => !string.IsNullOrWhiteSpace(input);

        public static bool IsUmbracoPreview(this string input)
            => input.HasValue() && input.StartsWith("/umbraco/preview/");

        public static string ToAbsoluteUrl(this string url)
        {
            if (!url.HasValue())
            {
                return url;
            }

            if (url.InvariantStartsWith("http://") || url.InvariantStartsWith("https://"))
            {
                return url;
            }

            if (HttpContext.Current == null)
            {
                return url;
            }

            if (url.StartsWith("/"))
            {
                url = url.Insert(0, "~");
            }

            if (!url.StartsWith("~/"))
            {
                url = url.Insert(0, "~/");
            }

            var requestUrl = HttpContext.Current.Request.Url;
            var port = requestUrl.Port != 80 ? ":" + requestUrl.Port : string.Empty;

            return $"{requestUrl.Scheme}://{requestUrl.Host}{port}{VirtualPathUtility.ToAbsolute(url)}";
        }

        public static Uri ToUri(this string urlString)
            => urlString.HasValue()
                ? new Uri(urlString)
                : null;

        public static Udi ToDocumentUdi(this string documentUdi)
            => Udi.Parse(documentUdi);

        public static string GetIpAddressWithoutPort(this string ipAddress)
        {
            var portIndex = ipAddress.IndexOf(':');

            return portIndex < 0
                ? ipAddress
                : ipAddress.Substring(0, portIndex);
        }
    }
}
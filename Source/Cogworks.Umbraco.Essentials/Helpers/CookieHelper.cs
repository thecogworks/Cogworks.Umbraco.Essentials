using System;
using System.Web;
using Cogworks.Essentials.Extensions;
using Cogworks.Umbraco.Essentials.Configurations;
using Cogworks.Umbraco.Essentials.Extensions;

namespace Cogworks.Umbraco.Essentials.Helpers
{
    public static class CookieHelper
    {
        private static HttpContext Context => HttpContext.Current;

        private const int CookieDuration = 1;

        public static void Set(string key, string value)
        {
            var cookieDuration = CookieConfigurations.CookieDuration.HasValue() && CookieConfigurations.CookieDuration > 0
                ? CookieConfigurations.CookieDuration
                : CookieDuration;

            var httpCookie = new HttpCookie(key)
            {
                Value = value,
                Expires = DateTime.UtcNow.AddDays(cookieDuration)
            };

            Context.Response.Cookies.Add(httpCookie);
        }

        public static string Get(string key)
        {
            var httpCookie = Context.Request.Cookies.Get(key);

            return httpCookie != null
                ? Context.Server.HtmlEncode(httpCookie.Value).Trim()
                : string.Empty;
        }

        public static bool Exists(string key)
            => Context.Request.Cookies.Get(key) != null;
    }
}
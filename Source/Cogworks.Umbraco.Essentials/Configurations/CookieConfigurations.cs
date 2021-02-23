using Cogworks.Umbraco.Essentials.Helpers;

namespace Cogworks.Umbraco.Essentials.Configurations
{
    public static class CookieConfigurations
    {
        public static readonly int CookieDuration = AppSettings.Get<int>("Cookies.DurationInDays");
    }
}
using Cogworks.Umbraco.Essentials.Helpers;

namespace Cogworks.Umbraco.Essentials.Configurations
{
    public static class AppSettingsConfiguration
    {
        public static string ApplicationHostName { get; } = AppSettings.Get<string>("ApplicationHostName");

        public static string LiveDomain { get; } = AppSettings.Get<string>("LiveDomain");

        public static bool IsMasterServer { get; } = AppSettings.Get<bool>("IsMasterServer");
    }
}
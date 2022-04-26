using System.Collections.Generic;
using System.Linq;
using Cogworks.Essentials.Extensions;
using Cogworks.Umbraco.Essentials.Configurations;
using Umbraco.Core.Sync;

namespace Cogworks.Umbraco.Essentials.Infrastructure
{
    public class ServerRegistrar : IServerRegistrar
    {
        public IEnumerable<IServerAddress> Registrations { get; } = Enumerable.Empty<IServerAddress>();

        public ServerRole GetCurrentServerRole()
            => AppSettingsConfiguration.IsMasterServer
                ? ServerRole.Master
                : ServerRole.Replica;

        // NOTE: If you want to explicitly define the URL that your application is running on,
        // this will be used for the server to communicate with itself, you can return the
        // custom path here and it needs to be in this format:
        // http://www.mysite.com/umbraco
        public string GetCurrentServerUmbracoApplicationUrl()
        {
            var applicationHostName = AppSettingsConfiguration.ApplicationHostName;
            var umbracoUseHttps = AppSettingsConfiguration.UmbracoUseHttps;

            if (applicationHostName.HasValue())
            {
                var urlProtocol = umbracoUseHttps ? "https://" : "http://";
                var currentServerUmbracoApplicationUrl = $"{urlProtocol}{applicationHostName}/umbraco";

                return currentServerUmbracoApplicationUrl;
            }

            return null;
        }
    }
}
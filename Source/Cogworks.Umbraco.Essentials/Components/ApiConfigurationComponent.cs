using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Newtonsoft.Json;
using Umbraco.Core.Composing;

namespace Cogworks.Umbraco.Essentials.Components
{
    public class ApiConfigurationComponent : IComponent
    {
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public ApiConfigurationComponent(JsonSerializerSettings jsonSerializerSettings)
            => _jsonSerializerSettings = jsonSerializerSettings;

        public void Initialize()
        {
            RouteTable.Routes.MapMvcAttributeRoutes();
            GlobalConfiguration.Configuration.MapHttpAttributeRoutes();

            var jsonFormatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            jsonFormatter.SerializerSettings = _jsonSerializerSettings;
            jsonFormatter.UseDataContractJsonSerializer = false;
        }

        public void Terminate()
        {
        }
    }
}
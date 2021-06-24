using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Umbraco.Core.Composing;

namespace Cogworks.Umbraco.Essentials.Components
{
    public class ApiConfigurationComponent : IComponent
    {
        public void Initialize()
        {
            RouteTable.Routes.MapMvcAttributeRoutes();
            GlobalConfiguration.Configuration.MapHttpAttributeRoutes();

            var jsonFormatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            jsonFormatter.UseDataContractJsonSerializer = false;
        }

        public void Terminate()
        {
        }
    }
}
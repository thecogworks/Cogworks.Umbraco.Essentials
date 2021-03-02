using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http.Controllers;
using Umbraco.Web.WebApi;

namespace Cogworks.Umbraco.Essentials.Attributes.AngularAttributes
{
    public class AngularJsonOnlyConfigurationCamelCaseAttribute : AngularJsonOnlyConfigurationAttribute
    {
        public override void Initialize(HttpControllerSettings controllerSettings, HttpControllerDescriptor controllerDescriptor)
        {
            var formatters = controllerSettings.Formatters
                .Where(formatter => formatter is JsonMediaTypeFormatter || formatter is XmlMediaTypeFormatter)
                .ToList();

            foreach (var formatter in formatters)
            {
                controllerSettings.Formatters.Remove(formatter);
            }

            controllerSettings.Formatters.Add(new AngularJsonMediaFormatterCamelCase());
        }
    }
}
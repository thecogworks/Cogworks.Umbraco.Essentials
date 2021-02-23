using Newtonsoft.Json.Serialization;
using Umbraco.Web.WebApi;

namespace Cogworks.Umbraco.Essentials.Attributes.AngularAttributes
{
    public class AngularJsonMediaFormatterCamelCase : AngularJsonMediaTypeFormatter
    {
        public AngularJsonMediaFormatterCamelCase()
            => SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    }
}
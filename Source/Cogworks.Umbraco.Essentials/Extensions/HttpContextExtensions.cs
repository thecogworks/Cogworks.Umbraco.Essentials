using System;
using System.Linq;
using System.Web;
using Cogworks.Essentials.Extensions;
using Cogworks.Umbraco.Essentials.Constants;
using Umbraco.Core.Security;
using Umbraco.Web.Security;

namespace Cogworks.Umbraco.Essentials.Extensions
{
    public static class HttpContextExtensions
    {
        public static bool IsUserLoggedInUmbraco(this HttpContextWrapper httpContextWrapper)
            => httpContextWrapper?.GetUmbracoAuthTicket()?.Identity is UmbracoBackOfficeIdentity;

        public static string GetIpAddressWithoutPort(this HttpContextBase httpContext)
            => httpContext.GetRequestIpAddress()?.GetIpAddressWithoutPort();

        public static string GetRequestIpAddress(this HttpContextBase httpContext)
        {
            var serverVariable = httpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            var serverForwardedIpAddresses = serverVariable?.Split(
                new[] { StringConstants.Separators.Comma },
                StringSplitOptions.RemoveEmptyEntries);

            return serverForwardedIpAddresses.HasAny()
                ? serverForwardedIpAddresses.FirstOrDefault()
                : httpContext.Request.UserHostAddress;
        }
    }
}
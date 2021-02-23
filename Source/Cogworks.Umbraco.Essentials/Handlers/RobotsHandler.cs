using System.IO;
using System.Web;
using Cogworks.Umbraco.Essentials.Configurations;
using Cogworks.Umbraco.Essentials.Constants;

namespace Cogworks.Umbraco.Essentials.Handlers
{
    public class RobotsHandler : IHttpHandler
    {
        public bool IsReusable => true;

        public void ProcessRequest(HttpContext context)
        {
            var isProduction = IsProductionUrl(context);
            var robotsTxtContent = isProduction
                ? ReadFile(RobotsConstants.Live, context)
                : ReadFile(RobotsConstants.Disallowed, context);

            context.Response.ContentType = "text/plain";
            context.Response.Clear();
            context.Response.BufferOutput = true;
            context.Response.Write(robotsTxtContent);
        }

        private static string ReadFile(string name, HttpContext context)
            => File.ReadAllText(GetPath("robots." + name + ".txt", context));

        private static string GetPath(string name, HttpContext context)
            => context.Server.MapPath(VirtualPathUtility.ToAbsolute("~/" + name));

        private static bool IsProductionUrl(HttpContext context)
            => context.Request.Url?.Host == AppSettingsConfiguration.LiveDomain
               || context.Request.UrlReferrer?.Host == AppSettingsConfiguration.LiveDomain;
    }
}
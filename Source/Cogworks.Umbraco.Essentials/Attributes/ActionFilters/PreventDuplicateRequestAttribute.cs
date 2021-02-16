using System;
using System.Web;
using System.Web.Mvc;
using Cogworks.Umbraco.Essentials.Constants;

namespace Cogworks.Umbraco.Essentials.Attributes.ActionFilters
{
    // Code from
    // https://gist.github.com/jyarbro/facb175caed8eb9e2239faf750e37230
    // https://stackoverflow.com/a/32807640
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class PreventDuplicateRequestAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var currentToken = HttpContext.Current.Request["__RequestVerificationToken"];

            if (currentToken == null)
            {
                return;
            }

            if (HttpContext.Current.Session["LastProcessedToken"] == null)
            {
                HttpContext.Current.Session["LastProcessedToken"] = currentToken;

                return;
            }

            lock (HttpContext.Current.Session["LastProcessedToken"])
            {
                var lastToken = HttpContext.Current.Session["LastProcessedToken"].ToString();

                if (lastToken == currentToken)
                {
                    filterContext.Controller.ViewData.ModelState.AddModelError(
                        ErrorConstants.PreventDuplicateRequestError,
                        ErrorConstants.PreventDuplicateRequestErrorMessage);

                    return;
                }

                HttpContext.Current.Session["LastProcessedToken"] = currentToken;
            }
        }
    }
}
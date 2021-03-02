using System.Web.Mvc;
using Cogworks.Umbraco.Essentials.ActionResults;

namespace Cogworks.Umbraco.Essentials.Extensions
{
    public static class RedirectExtensions
    {
        public static RedirectWithMessageResult WithMessage(this RedirectToRouteResult instance, string header, string message)
            => new RedirectWithMessageResult(instance, header, message);

        public static RedirectWithMessageResult WithMessage(this RedirectResult instance, string header, string message)
            => new RedirectWithMessageResult(instance, header, message);
    }
}
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Cogworks.Essentials.Extensions;

namespace Cogworks.Umbraco.Essentials.Extensions
{
    public static class HtmlStringExtensions
    {
        public static bool HasValue(this IHtmlString input)
            => input != null && input.ToString().HasValue();

        public static MvcHtmlString CustomValidationSummary(
            this HtmlHelper htmlHelper,
            bool excludePropertyErrors,
            string message = null)
            => !(excludePropertyErrors && !htmlHelper.ViewData.ModelState.ContainsKey(string.Empty))
                ? htmlHelper.ValidationSummary(excludePropertyErrors, message)
                : null;

        public static MvcHtmlString CustomValidationMessage(
            this HtmlHelper htmlHelper, string modelName)
            => htmlHelper.ViewData.ModelState.ContainsKey(modelName) && htmlHelper.ViewData.ModelState[modelName].HasValue()
                ? htmlHelper.ValidationMessage(modelName)
                : null;
    }
}
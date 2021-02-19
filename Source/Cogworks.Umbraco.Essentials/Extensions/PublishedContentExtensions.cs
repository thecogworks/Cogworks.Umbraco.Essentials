using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace Cogworks.Umbraco.Essentials.Extensions
{
    public static class PublishedContentExtensions
    {
        public static TPublishedContent FirstOrDefault<TPublishedContent>(this IEnumerable<IPublishedContent> publishedContents)
            where TPublishedContent : class, IPublishedContent
            => publishedContents.OfType<TPublishedContent>().FirstOrDefault();

        public static TChild FirstOrDefaultChild<TChild>(this IPublishedContent publishedContent, string culture = null)
            where TChild : class, IPublishedContent
            => publishedContent?.FirstChild<TChild>(culture);

        public static bool HasValue(this IPublishedContent publishedContent)
            => publishedContent != null;

        public static string GetAbsoluteUrl(this IPublishedContent content)
            => content.HasValue()
                ? content.Url(mode: UrlMode.Absolute)
                : null;

        public static string GetImageAbsoluteUrl(this IPublishedContent image)
            => image.HasValue()
                ? image.GetCropUrl().ToAbsoluteUrl()
                : string.Empty;

        public static bool HasTemplate(this IPublishedContent content)
            => content.TemplateId.HasValue;
    }
}
using Umbraco.Core.Models;

namespace Cogworks.Umbraco.Essentials.Extensions
{
    public static class MediaExtensions
    {
        public static string GetStringValue(this IMedia media, string propertyAlias)
        {
            if (!propertyAlias.HasValue())
            {
                return string.Empty;
            }

            var value = media.GetValue<string>(propertyAlias);

            return value.HasValue()
                ? value
                : string.Empty;
        }
    }
}
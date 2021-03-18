using System.Collections.Generic;
using Cogworks.Essentials.Constants;
using Cogworks.Umbraco.Essentials.Models;
using Umbraco.Core.Models.PublishedContent;

namespace Cogworks.Umbraco.Essentials.Builders.Interfaces
{
    public interface IResponsiveImageBuilder
    {
        ResponsiveImage Build(
            IPublishedContent image,
            string cropPrefix,
            string altText,
            IReadOnlyDictionary<string, string> breakpoints = null,
            string imageClass = null,
            string containerClass = null,
            string cropSeparator = StringConstants.Separators.Hyphen,
            int? width = null,
            int? height = null,
            bool includeRetina = true,
            bool enableWebP = false,
            int? quality = null);

        IReadOnlyDictionary<string, string> BuildResponsiveImageSources(
            IPublishedContent image,
            string cropPrefix,
            IReadOnlyDictionary<string, string> breakpoints = null,
            string cropSeparator = StringConstants.Separators.Hyphen,
            int? width = null,
            int? height = null,
            bool includeRetina = true,
            bool enableWebP = false,
            int? quality = null);
    }
}
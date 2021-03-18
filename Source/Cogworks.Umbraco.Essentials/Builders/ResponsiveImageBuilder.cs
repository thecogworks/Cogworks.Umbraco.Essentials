using System.Collections.Generic;
using Cogworks.Essentials.Extensions;
using Cogworks.Umbraco.Essentials.Builders.Interfaces;
using Cogworks.Umbraco.Essentials.Constants;
using Cogworks.Umbraco.Essentials.Extensions;
using Cogworks.Umbraco.Essentials.Models;
using Umbraco.Core.Models.PublishedContent;
using StringConstants = Cogworks.Essentials.Constants.StringConstants;

namespace Cogworks.Umbraco.Essentials.Builders
{
    public class ResponsiveImageBuilder : IResponsiveImageBuilder
    {
        public ResponsiveImage Build(IPublishedContent image,
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
            int? quality = null)
        {
            var imageSources = BuildResponsiveImageSources(image, cropPrefix, breakpoints, cropSeparator, width, height, includeRetina, enableWebP, quality);

            if (!imageSources.HasAny())
            {
                return null;
            }

            var model = new ResponsiveImage
            {
                ImageSources = imageSources,
                ImageClass = imageClass,
                ContainerClass = containerClass,
                AltText = altText
            };

            return model;
        }

        public IReadOnlyDictionary<string, string> BuildResponsiveImageSources(IPublishedContent image,
            string cropPrefix,
            IReadOnlyDictionary<string, string> breakpoints = null,
            string cropSeparator = StringConstants.Separators.Hyphen,
            int? width = null,
            int? height = null,
            bool includeRetina = true,
            bool enableWebP = false,
            int? quality = null)
        {
            breakpoints ??= BreakPointConstants.DefaultBreakpoints;

            var imageSources = new Dictionary<string, string>();

            foreach (var breakPoint in breakpoints)
            {
                var cropAlias = $"{cropPrefix}{cropSeparator}{breakPoint.Key}";

                var imageSource = image.GetCropUrls(cropAlias, width, height, includeRetina, enableWebP, quality);

                if (imageSource.HasValue())
                {
                    imageSources.Add(imageSource, breakPoint.Value);
                }
            }

            return imageSources.HasAny()
                ? imageSources
                : null;
        }
    }
}
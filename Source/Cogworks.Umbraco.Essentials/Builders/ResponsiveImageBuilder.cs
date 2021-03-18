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
            string cropSeparator = StringConstants.Separators.Hyphen)
        {
            var imageSources = BuildResponsiveImageSources(image, cropPrefix, breakpoints, cropSeparator);

            if (!imageSources.HasAny())
            {
                return null;
            }

            var model = new ResponsiveImage
            {
                ImageSources = imageSources,
                ImageClass = imageClass,
                AltText = altText
            };

            return model;
        }

        public IReadOnlyDictionary<string, string> BuildResponsiveImageSources(IPublishedContent image,
            string cropPrefix,
            IReadOnlyDictionary<string, string> breakpoints = null,
            string cropSeparator = StringConstants.Separators.Hyphen)
        {
            breakpoints ??= BreakPointConstants.DefaultBreakpoints;

            var imageSources = new Dictionary<string, string>();

            foreach (var breakPoint in breakpoints)
            {
                var imageSource = image.GetCropUrls($"{cropPrefix}{cropSeparator}{breakPoint.Key}");

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
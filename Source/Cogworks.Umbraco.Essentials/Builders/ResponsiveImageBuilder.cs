using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Cogworks.Essentials.Extensions;
using Cogworks.Umbraco.Essentials.Builders.Interfaces;
using Cogworks.Umbraco.Essentials.Constants;
using Cogworks.Umbraco.Essentials.Extensions;
using Cogworks.Umbraco.Essentials.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using StringConstants = Cogworks.Essentials.Constants.StringConstants;

namespace Cogworks.Umbraco.Essentials.Builders
{
    public class ResponsiveImageBuilder : IResponsiveImageBuilder
    {
        public ResponsiveImage Build(IPublishedContent image,
            string cropPrefix,
            string altText = null,
            ImageBreakpoints breakpoints = null,
            string imageClass = null,
            string containerClass = null,
            string cropSeparator = StringConstants.Separators.Hyphen,
            int? width = null,
            int? height = null,
            bool includeRetina = true,
            bool enableWebP = false,
            int? quality = null,
            bool isLazy = true,
            bool enableDefaultDimensions = false)
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
                AltText = altText.HasValue() ? altText : image.Name,
                LazyLoadValue = isLazy ? "lazy" : "eager"
            };

            return enableDefaultDimensions
                ? AddDefaultDimensions(model, width, height)
                : model;
        }

        public IReadOnlyList<ImageSource> BuildResponsiveImageSources(IPublishedContent image,
            string cropPrefix,
            ImageBreakpoints breakpoints = null,
            string cropSeparator = StringConstants.Separators.Hyphen,
            int? width = null,
            int? height = null,
            bool includeRetina = true,
            bool enableWebP = false,
            int? quality = null,
            string fileExtension = null)
        {
            if (!breakpoints.HasAny())
            {
                breakpoints = new ImageBreakpoints(BreakPointConstants.DefaultBreakpoints);
            }

            var imageSources = new List<ImageSource>();

            foreach (var breakPoint in breakpoints)
            {
                var cropAlias = $"{cropPrefix}{cropSeparator}{breakPoint.Key}";

                if (enableWebP)
                {
                    var imageSourceWithWebP = image.GetCropUrls(cropAlias, width, height, includeRetina, true, quality);
                    if (imageSourceWithWebP.HasValue())
                    {
                        imageSources.Add(new ImageSource(imageSourceWithWebP, breakPoint.Value, $"image/{ImageCropConstants.WebP}"));
                    }
                }
                else
                {
                    var imageSource = image.GetCropUrls(cropAlias, width, height, includeRetina);

                    if (imageSource.HasValue())
                    {
                        var imageExtension = fileExtension.HasValue()
                            ? fileExtension
                            : Path.GetExtension(image.Url())?.TrimStart(StringConstants.Separators.Dot.ToChar());

                        imageSources.Add(new ImageSource(imageSource, breakPoint.Value, $"image/{imageExtension}"));
                    }
                }
            }

            return imageSources.HasAny()
                ? imageSources
                : null;
        }

        private static ResponsiveImage AddDefaultDimensions(ResponsiveImage image, int? width = null, int? height = null)
        {
            var cropUrl = (height == null || height < 1) || (width == null || width < 1)
                ? HttpUtility.ParseQueryString(image.ImageSources.FirstOrDefault().Source)
                : null;

            image.Height = height != null && height > 0
                ? (int)height
                : ImageCropperExtensions.GetValueFromCrops(cropUrl, ImageCropConstants.Height);

            image.Width = width != null && width > 0
                ? (int)width
                : ImageCropperExtensions.GetValueFromCrops(cropUrl, ImageCropConstants.Width);

            return image;
        }
    }
}
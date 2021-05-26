﻿using System.Collections.Generic;
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
            string altText = null,
            IReadOnlyDictionary<string, string> breakpoints = null,
            string imageClass = null,
            string containerClass = null,
            string cropSeparator = StringConstants.Separators.Hyphen,
            int? width = null,
            int? height = null,
            bool includeRetina = true,
            bool enableWebP = false,
            int? quality = null);

        IReadOnlyList<ImageSource> BuildResponsiveImageSources(
            IPublishedContent image,
            string cropPrefix,
            IReadOnlyDictionary<string, string> breakpoints = null,
            string cropSeparator = StringConstants.Separators.Hyphen,
            int? width = null,
            int? height = null,
            bool includeRetina = true,
            bool enableWebP = false,
            int? quality = null,
            string fileExtension = null);
    }
}
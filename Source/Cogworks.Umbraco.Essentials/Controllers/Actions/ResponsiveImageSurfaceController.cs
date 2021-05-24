using System.Collections.Generic;
using System.Web.Mvc;
using Cogworks.Essentials.Constants;
using Cogworks.Essentials.Extensions;
using Cogworks.Umbraco.Essentials.Builders.Interfaces;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Mvc;

namespace Cogworks.Umbraco.Essentials.Controllers.Actions
{
    public class ResponsiveImageSurfaceController : SurfaceController
    {
        private readonly IResponsiveImageBuilder _responsiveImageBuilder;

        public ResponsiveImageSurfaceController(IResponsiveImageBuilder responsiveImageBuilder) => _responsiveImageBuilder = responsiveImageBuilder;

        [ChildActionOnly]
        public virtual ActionResult ResponsiveImage(IPublishedContent image, string cropPrefix, string altText = null, IReadOnlyDictionary<string, string> breakpoints = null,
            string imageClass = null, string containerClass = null, string cropSeparator = StringConstants.Separators.Hyphen,
            int? width = null, int? height = null, bool includeRetina = true, bool enableWebP = false, int? quality = null)
        {
            if (!image.HasValue())
            {
                return new EmptyResult();
            }

            var model = _responsiveImageBuilder.Build(image, cropPrefix, altText, breakpoints,
                imageClass, containerClass, cropSeparator, width, height, includeRetina, enableWebP, quality);

            if (model == null)
            {
                return new EmptyResult();
            }

            return PartialView("~/Views/Partials/Shared/_ResponsiveImage.cshtml", model);
        }
    }
}
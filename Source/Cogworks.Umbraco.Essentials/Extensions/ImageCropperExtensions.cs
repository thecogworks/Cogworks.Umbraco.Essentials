using System.Web;
using Cogworks.Essentials.Extensions;
using Cogworks.Umbraco.Essentials.Constants;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace Cogworks.Umbraco.Essentials.Extensions
{
    public static class ImageCropperExtensions
    {
        public static string GetCropUrls(this IPublishedContent image, string cropAlias, int? width = null, int? height = null, bool includeRetina = true)
        {
            var imageUrl = image.GetCropUrl($"{cropAlias}");

            if (!imageUrl.HasValue())
            {
                return string.Empty;
            }

            if (width.HasValue || height.HasValue)
            {
                imageUrl = UpdateCropDimensions(imageUrl, width, height);
            }

            if (!includeRetina)
            {
                return imageUrl;
            }

            var retinaUrl = GetRetinaCropUrl(imageUrl);

            return retinaUrl.HasValue()
                ? $"{imageUrl} 1x, {retinaUrl} 2x"
                : imageUrl;
        }

        private static string UpdateCropDimensions(string imageCrop, int? width, int? height)
        {
            var query = HttpUtility.ParseQueryString(imageCrop);

            if (width.HasValue)
            {
                query[ImageCropConstants.Width] = width.ToString();
            }

            if (height.HasValue)
            {
                query[ImageCropConstants.Height] = height.ToString();
            }

            return HttpUtility.UrlDecode(query.ToString());
        }

        private static string GetRetinaCropUrl(string imageCrop)
        {
            var query = HttpUtility.ParseQueryString(imageCrop);

            if (!int.TryParse(query.Get(ImageCropConstants.Width), out var originalWidth) || originalWidth < 1)
            {
                return null;
            }

            if (!int.TryParse(query.Get(ImageCropConstants.Height), out var originalHeight) || originalHeight < 1)
            {
                return null;
            }

            var width = originalWidth * 2;
            var height = originalHeight * 2;

            query[ImageCropConstants.Width] = width.ToString();
            query[ImageCropConstants.Height] = height.ToString();

            return HttpUtility.UrlDecode(query.ToString());
        }
    }
}
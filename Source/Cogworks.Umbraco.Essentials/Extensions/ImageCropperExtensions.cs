using System.Web;
using Cogworks.Essentials.Extensions;
using Cogworks.Umbraco.Essentials.Constants;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace Cogworks.Umbraco.Essentials.Extensions
{
    public static class ImageCropperExtensions
    {
        public static string GetCropUrls(this IPublishedContent image, string cropAlias = null, int? width = null, int? height = null,
            bool includeRetina = true, bool enableWebP = false, int? quality = null, bool returnOnlyRetina = false)
        {
            var imageUrl = GetCropUrlStandard(image, cropAlias, width, height, enableWebP, quality);

            if (!includeRetina)
            {
                return imageUrl;
            }

            var retinaUrl = GetCropUrlRetina(image, cropAlias, width, height, enableWebP, quality);

            if (retinaUrl.HasValue())
            {
                return returnOnlyRetina ? retinaUrl : $"{imageUrl} 1x, {retinaUrl} 2x";
            }

            return imageUrl;
        }

        public static string GetCropUrlStandard(this IPublishedContent image, string cropAlias = null, int? width = null, int? height = null,
            bool enableWebP = false, int? quality = null)
        {
            var imageUrl = cropAlias.HasValue() ? image.GetCropUrl($"{cropAlias}") : image.GetCropUrl();

            if (!imageUrl.HasValue())
            {
                return string.Empty;
            }

            if (width.HasValue || height.HasValue || enableWebP)
            {
                imageUrl = UpdateCropSettings(imageUrl, width, height, enableWebP, quality);
            }

            return imageUrl;
        }

        public static string GetCropUrlRetina(this IPublishedContent image, string cropAlias = null, int? width = null, int? height = null,
            bool enableWebP = false, int? quality = null)
        {
            var imageUrl = GetCropUrlStandard(image, cropAlias, width, height, enableWebP, quality);

            if (!imageUrl.HasValue())
            {
                return string.Empty;
            }

            var retinaUrl = GetRetinaCropUrl(imageUrl);

            return retinaUrl.HasValue() ? retinaUrl : imageUrl;
        }

        private static string UpdateCropSettings(string imageCrop, int? width, int? height, bool enableWebP, int? quality = null)
        {
            var query = HttpUtility.ParseQueryString(imageCrop);

            if (width.HasValue)
            {
                query.Remove(ImageCropConstants.Width);
                query[ImageCropConstants.Width] = width.ToString();
            }

            if (height.HasValue)
            {
                query.Remove(ImageCropConstants.Height);
                query[ImageCropConstants.Height] = height.ToString();
            }

            if (enableWebP)
            {
                query.Remove(ImageCropConstants.Format);
                query[ImageCropConstants.Format] = ImageCropConstants.WebP;

                quality ??= 80;

                query.Remove(ImageCropConstants.Quality);
                query[ImageCropConstants.Quality] = quality.ToString();
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
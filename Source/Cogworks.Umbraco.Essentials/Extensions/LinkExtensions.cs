using Cogworks.Essentials.Extensions;
using Umbraco.Web.Models;

namespace Cogworks.Umbraco.Essentials.Extensions
{
    public static class LinkExtensions
    {
        public static bool HasValue(this Link link) => link != null && link.Url.HasValue();
    }
}
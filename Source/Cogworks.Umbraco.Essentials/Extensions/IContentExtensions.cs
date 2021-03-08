using Cogworks.Essentials.Extensions;
using Umbraco.Core.Models;

namespace Cogworks.Umbraco.Essentials.Extensions
{
    public static class IContentExtensions
    {
        public static bool TryGetProperty<T>(this IContent model, string propertyName, out T property)
        {
            property = default;

            if (!propertyName.HasValue())
            {
                return false;
            }

            if (model.HasProperty(propertyName.ToLower()))
            {
                property = model.GetValue<T>(propertyName.ToLower());

                if (property.HasValue())
                {
                    return true;
                }
            }

            return false;
        }
    }
}
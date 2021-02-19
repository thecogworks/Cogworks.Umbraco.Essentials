using System;
using System.Collections.Generic;
using System.Linq;
using Cogworks.Umbraco.Essentials.Constants;
using Umbraco.Core.Models.PublishedContent;

namespace Cogworks.Umbraco.Essentials.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool HasAny<T>(this IEnumerable<T> items)
            => items != null && items.Any();

        public static T FirstOrDefaultOfType<T>(this IEnumerable<IPublishedContent> publishedContents)
            => publishedContents.OfType<T>().FirstOrDefault();

        public static string JoinIfNotNull<TInput, TResult>(this IEnumerable<TInput> items, Func<TInput, TResult> func,
            string separator = StringConstants.Separators.Space)
            => items.HasAny()
                ? string.Join(separator, items.Select(func))
                : string.Empty;
    }
}
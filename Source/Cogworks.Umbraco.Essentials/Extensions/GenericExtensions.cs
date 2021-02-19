namespace Cogworks.Umbraco.Essentials.Extensions
{
    public static class GenericExtensions
    {
        public static bool HasValue<T>(this T value)
            => !(value == null || value.Equals(default));
    }
}
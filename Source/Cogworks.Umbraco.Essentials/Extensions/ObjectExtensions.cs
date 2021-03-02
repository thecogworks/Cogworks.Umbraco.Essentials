﻿namespace Cogworks.Umbraco.Essentials.Extensions
{
    public static class ObjectExtensions
    {
        public static bool HasValue(this object value)
            => !(value == null || value.Equals(default));
    }
}
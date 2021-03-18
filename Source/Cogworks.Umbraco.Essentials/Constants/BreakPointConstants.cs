using System.Collections.Generic;

namespace Cogworks.Umbraco.Essentials.Constants
{
    public static class BreakPointConstants
    {
        public static readonly IReadOnlyDictionary<string, string> DefaultBreakpoints = new Dictionary<string, string>
        {
            { ExtraLarge, "1440px" },
            { Large, "1024px" },
            { Medium, "768px" },
            { ExtraSmall, "375px" }
        };

        public const string ExtraLarge = "XL";
        public const string Large = "LG";
        public const string Medium = "MD";
        public const string Small = "S";
        public const string ExtraSmall = "XS";
    }
}
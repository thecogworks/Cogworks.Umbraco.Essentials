using System;
using System.Linq;

namespace Cogworks.Umbraco.Essentials.Utilities
{
    public static class TestDetector
    {
        public static bool IsRunningFromXUnit => AppDomain.CurrentDomain
            .GetAssemblies()
            .Any(a => a.FullName.ToLowerInvariant().StartsWith("xunit.core"));
    }
}
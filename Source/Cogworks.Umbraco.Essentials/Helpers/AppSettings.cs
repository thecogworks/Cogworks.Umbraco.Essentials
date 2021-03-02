using System;
using System.ComponentModel;
using System.Configuration;
using Cogworks.Umbraco.Essentials.Extensions;

namespace Cogworks.Umbraco.Essentials.Helpers
{
    public static class AppSettings
    {
        public static T Get<T>(string key, T defaultValue = default, Action<Exception> errorCallback = null)
        {
            var value = defaultValue;

            var setting = ConfigurationManager.AppSettings[key];

            if (!setting.HasValue())
            {
                return value;
            }

            var converter = TypeDescriptor.GetConverter(typeof(T));

            try
            {
                value = (T)converter.ConvertFromInvariantString(setting);
            }
            catch (Exception ex)
            {
                errorCallback?.Invoke(ex);
            }

            return value;
        }
    }
}
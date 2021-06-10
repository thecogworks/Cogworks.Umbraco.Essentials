using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Cogworks.Umbraco.Essentials.Models
{
    public class ImageBreakpoints : IEnumerable<KeyValuePair<string, string>>
    {
        private readonly IDictionary<string, string> _breakpoints;

        public ImageBreakpoints() => _breakpoints = new Dictionary<string, string>();

        public ImageBreakpoints(IEnumerable<KeyValuePair<string, string>> breakpoints)
            => _breakpoints = breakpoints.ToDictionary(x => x.Key, x => x.Value)
                              ?? throw new NullReferenceException($"Breakpoints {nameof(breakpoints)} are not assigned.");

        public IEnumerable<string> CropAliases => _breakpoints.Keys;

        public IEnumerable<string> CropValues => _breakpoints.Values;

        public int Count => _breakpoints.Count;

        public string this[string key]
            => TryGetCropValue(key, out var value)
                ? value
                : default;

        public bool ContainsCrop(string cropAlias)
            => _breakpoints.ContainsKey(cropAlias);

        public bool TryGetCropValue(string cropAlias, out string value)
            => _breakpoints.TryGetValue(cropAlias, out value);

        IEnumerator IEnumerable.GetEnumerator()
            => _breakpoints.GetEnumerator();

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
            => _breakpoints.GetEnumerator();

        public IEnumerable<KeyValuePair<string, string>> GetBreakpoints()
            => _breakpoints.ToArray();
    }
}
namespace Cogworks.Umbraco.Essentials.Models
{
    public class ImageSource
    {
        public string Source { get; }

        public string Breakpoint { get; }

        public string Type { get; }

        public ImageSource(string source, string breakpoint, string type)
        {
            Source = source;
            Breakpoint = breakpoint;
            Type = type;
        }
    }
}
using System.Collections.Generic;

namespace Cogworks.Umbraco.Essentials.Models
{
    public class ResponsiveImage
    {
        public IReadOnlyList<ImageSource> ImageSources { get; set; }

        public string ImageClass { get; set; }

        public string ContainerClass { get; set; }

        public string AltText { get; set; }

        public string LazyLoadValue { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }
    }
}
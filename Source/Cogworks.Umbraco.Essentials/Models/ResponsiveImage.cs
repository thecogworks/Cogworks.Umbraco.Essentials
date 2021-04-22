using System.Collections.Generic;

namespace Cogworks.Umbraco.Essentials.Models
{
    public class ResponsiveImage
    {
        public IReadOnlyList<ImageSource> ImageSources { get; set; }

        public string ImageClass { get; set; }

        public string ContainerClass { get; set; }

        public string AltText { get; set; }
    }
}
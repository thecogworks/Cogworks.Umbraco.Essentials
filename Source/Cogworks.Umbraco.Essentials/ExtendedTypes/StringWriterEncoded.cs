using System.IO;
using System.Text;

namespace Cogworks.Umbraco.Essentials.ExtendedTypes
{
    public class StringWriterEncoded : StringWriter
    {
        public override Encoding Encoding { get; }

        public StringWriterEncoded(Encoding encodingType)
            => Encoding = encodingType;
    }
}
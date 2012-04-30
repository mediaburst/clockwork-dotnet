using System.IO;
using System.Text;

namespace Clockwork
{
    /// <summary>
    /// utf-8 version of the .NET StringBuilder class
    /// </summary>
    /// <remarks>
    /// Inherits from StringWriter and overrides the character set so we 
    /// can produce the utf-8 encoded XML Clockwork requires
    /// </remarks>
    /// <see cref="System.IO.StringWriter"/>
    class UTF8StringWriter : StringWriter
    {
        public override Encoding Encoding { get { return Encoding.UTF8; } }
    }
}

using System.Collections.Generic;
using System.Xml.Serialization;

namespace Clockwork.XML
{
    /// <summary>
    /// XML Serialisation class
    /// for Message tag in API Send call
    /// </summary>
    [XmlRoot]
    public class Message
    {
        public Message()
        {
            SMS = new List<SMS>();
        }

        public string Key { get; set; }
        [XmlElement("SMS")]
        public List<SMS> SMS { get; set; }
    }
}

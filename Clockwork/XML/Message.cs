using System.Collections.Generic;
using System.Runtime.Serialization;
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

        [XmlElement("Key")]
        public string Key { get; set; }

        [XmlElement("SMS")]
        public List<SMS> SMS { get; set; }
    }
}

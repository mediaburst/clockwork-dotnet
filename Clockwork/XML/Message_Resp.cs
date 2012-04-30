using System.Collections.Generic;
using System.Xml.Serialization;

namespace Clockwork.XML
{
    /// <summary>
    /// XML Serialisation of SMS_Resp tag 
    /// from message sending response
    /// </summary>
    public class Message_Resp
    {
        public Message_Resp()
        {
            SMS_Resp = new List<SMS_Resp>();
        }

        public int? ErrNo { get; set; }
        public string ErrDesc { get; set; }

        [XmlElement("SMS_Resp")]
        public List<SMS_Resp> SMS_Resp { get; set; } 
    }
}

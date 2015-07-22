using System;
using System.Xml;
using System.Xml.Serialization;

namespace Clockwork.XML
{
    /// <summary>
    /// XML Serialization of SMS tag
    /// in Send API call
    /// </summary>
    public class SMS
    {
        /*
         * The ParameterSpecified functions prevent parameters being serialized to XML when they're null
         * allowing the API customer defaults to work correctly
         */

        public string To { get; set; }
        public string Content { get; set; }
        public string From { get; set; }
        public string ClientID { get; set; }
        public int WrapperID { get; set; }

        public bool? Long { get; set; }
        [XmlIgnore]
        public bool LongSpecified => Long != null;

        public bool? Truncate { get; set; }
        [XmlIgnore]
        public bool TruncateSpecified => Truncate != null;

        // Ignore this as we wan't to serialize the integer not the enum
        [XmlIgnore]
        public InvalidCharacterAction? InvalidCharAction { get; set; }

        // Integer representation of the Invalid Character Action enum
        [XmlElement("InvalidCharAction")]
        public int InvalidCharActionXml 
        {
            get { return (int)InvalidCharAction; }
            set { InvalidCharAction = (InvalidCharacterAction)Enum.ToObject(typeof(InvalidCharacterAction), value); }
        }
        [XmlIgnore]
        public bool InvalidCharActionXmlSpecified
        {
            get { return InvalidCharAction != null && InvalidCharAction != InvalidCharacterAction.AccountDefault; }
        }
    }
}

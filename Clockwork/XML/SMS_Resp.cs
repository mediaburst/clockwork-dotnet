
namespace Clockwork.XML
{
    /// <summary>
    /// XML Serialization class for SMS_Resp tag 
    /// in response from send message API call
    /// </summary>
    public class SMS_Resp
    {
        public string To { get; set; }
        public string MessageID { get; set; }
        public int? ErrNo { get; set; }
        public string ErrDesc { get; set; }
        public string ClientID { get; set; }
    }
}

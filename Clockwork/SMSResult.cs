
namespace Clockwork
{
    public class SMSResult
    {
        public string ID { get; set; }
        public string To { get; set; }
        public string ClientID { get; set; }
        public bool Success { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}

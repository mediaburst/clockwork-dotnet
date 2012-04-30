namespace Clockwork.XML
{
    /// <summary>
    /// XML Serialisation of Credit_Resp tag
    /// in credit check response
    /// </summary>
    public class Credit_Resp
    {
        public long Credit { get; set; }
        public int? ErrNo { get; set; }
        public string ErrDesc { get; set; }
    }
}

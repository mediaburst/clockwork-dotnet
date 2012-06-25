
namespace Clockwork
{
    /// <summary>
    /// A single text message
    /// </summary>
    public class SMS
    {
        /// <summary>
        /// Phone number the message is for
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// Message text
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// (Optional) From address displayed on the user's phone
        /// </summary>
        /// <remarks>
        /// If left blank your account default will be used
        /// </remarks>
        public string From { get; set; }        
        /// <summary>
        /// (Optional) Your identifier for this message
        /// For example this could be your database record ID.
        /// </summary>
        public string ClientID { get; set; }
        /// <summary>
        /// (Optional) Send message longer than 160 characters
        /// </summary>
        /// <remarks>
        /// If left blank your account default will be used
        /// </remarks>        
        public bool? Long { get; set; }
        /// <summary>
        /// (Optional) Trim the message text if it's too long
        /// </summary>
        /// <remarks>
        /// If left blank your account default will be used
        /// </remarks>        
        public bool? Truncate { get; set; }
        /// <summary>
        /// What to do if there's an invalid character in your message text
        /// Valid characters are defined in the GSM 03.38 character set
        /// </summary>
        /// <remarks>
        /// Set this to AccountDefault to use your accounts default setting
        /// </remarks>        
        public InvalidCharacterAction? InvalidCharacterAction { get; set; }

        /// <summary>
        /// Create a single text message
        /// </summary>
        public SMS()
        {
            // Required fields
            To = "";
            Message = "";

            // Optional fields
            // Leaving these set to null will tell the API to use account defaults
            From = null;
            ClientID = null;
            Long = null;
            Truncate = null;
            InvalidCharacterAction = null;
        }
    }
}

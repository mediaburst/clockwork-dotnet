
namespace Clockwork
{
    /// <summary>
    /// Result of sending an SMS
    /// </summary>
    public class SMSResult
    {
        /// <summary>
        /// Original SMS object this SMSResult is for
        /// </summary>
        public SMS SMS { get; set; }
        /// <summary>
        /// Clockwork Message ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// Success status flag
        /// This will be true if your message has been sent
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Error code (If the message wasn't sent)
        /// </summary>
        /// <remarks>
        /// This will be set to 0 if there wasn't an error
        /// </remarks>
        public int ErrorCode { get; set; }
        /// <summary>
        /// Human readable description of the error
        /// </summary>
        /// <remarks>
        /// If there wasn't an error this will be an empty string
        /// </remarks>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Set some sensible defaults for an SMS Result object
        /// </summary>
        public SMSResult()
        {
            SMS = null;
            ID = null;
            Success = false;
            ErrorCode = 0;
            ErrorMessage = "";
        }
    }
}
